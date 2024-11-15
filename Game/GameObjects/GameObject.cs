using DungeonCrawl.Maps;
using SadConsole;
using SadRogue.Primitives;


namespace DungeonCrawl.Gameobjects;

/// <summary>
/// Class <c>GameObject</c> models any objects in the game.
/// </summary>
public abstract class GameObject : IGameObject
{
  public Point Position { get; private set; }
  public Direction Direction;
  public int Damage { get; protected set; } = 0;
  public int Range { get; set; }
  public void RestoreMap(Map map) => _mapAppearance.CopyAppearanceTo(map.SurfaceObject.Surface[Position]);
  public ColoredGlyph Appearance { get; set; }
  protected ColoredGlyph OriginalAppearance { get; set; }
  private ColoredGlyph _mapAppearance = new ColoredGlyph();

  /// <summary>
  /// Constructor.
  /// </summary>
  /// <param name="appearance"></param>
  /// <param name="position"></param>
  /// <param name="hostingSurface"></param>
  protected GameObject(ColoredGlyph appearance, Point position, ScreenObjectManager screenObjectManager)
  {
    Appearance = appearance;
    OriginalAppearance = appearance;
    Position = position;
    // Store the map cell
    //hostingSurface.Surface[position].CopyAppearanceTo(_mapAppearance);
    _mapAppearance = screenObjectManager.GetScreenObject(position);


    // Draw the object
    screenObjectManager.DrawScreenObject(this);
    //DrawGameObject(hostingSurface);
  }

  /// <summary>
  /// Moves the object to the given position on the map.
  /// </summary>
  /// <param name="newPosition"></param>
  /// <param name="map"></param>
  /// <returns></returns>
  public bool Move(Point newPosition, Map map)
  {
    // Check new position is valid
    if (!map.SurfaceObject.Surface.IsValidCell(newPosition.X, newPosition.Y)) return false;

    // Check if other object is there
    if (map.TryGetMapObject(newPosition, out GameObject foundObject))
    {
      // We touched the other object, but they won't allow us to move into the space
      if (!foundObject.Touched(this, map))
      {
        return false;
      }
    }

    // Restore the old cell
    GameObject cellContent;
    if (map.TryGetMapObject(Position, out cellContent, this))
    {
      map.SurfaceObject.Surface[Position].CopyAppearanceFrom(cellContent.Appearance);
      /*
       map.SurfaceObject.Surface[Position].CopyAppearanceFrom(
           new ColoredGlyph(Color.Transparent, Color.Transparent, 0));
      // */
    }
    else
    {
      map.SurfaceObject.Surface[Position].CopyAppearanceFrom(
          new ColoredGlyph(Color.White, Color.Transparent, 0));
    }

    // Store the map cell of the new position
    map.SurfaceObject.Surface[newPosition].CopyAppearanceTo(_mapAppearance);

    Position = newPosition;
    DrawGameObject(map.SurfaceObject);
    return true;
  }

  /// <summary>
  /// Defines what should happen if another object touches the current one.
  /// </summary>
  /// <param name="source"></param>
  /// <param name="map"></param>
  /// <returns></returns>
  protected virtual bool Touched(GameObject source, Map map)
  {
    source.Touching(this);
    return false;
  }

  public virtual void Update(Map map)
  {

  }
  public virtual bool TakeDamage(Map map, GameObject source, int damage)
  {
    return false;
  }
  public virtual void Touching(GameObject source)
  {
  }

  /// <summary>
  /// Draws the object on the screen.
  /// </summary>
  /// <param name="screenSurface"></param>
  protected void DrawGameObject(IScreenSurface screenSurface)
  {
    Appearance.CopyAppearanceTo(screenSurface.Surface[Position]);
    screenSurface.IsDirty = true;
  }
}