#nullable enable
using DungeonCrawl.GameObjects.ObjectInterfaces;
using DungeonCrawl.Maps;
using DungeonCrawl.Mechanics;
using SadConsole;
using SadRogue.Primitives;


namespace DungeonCrawl.GameObjects;

/// <summary>
/// Class <c>GameObject</c> models any objects in the game.
/// </summary>
public abstract class GameObject : IGameObject
{
  public virtual Point Position { get; set; }
  public virtual Direction Direction { get; set; }
  public virtual int Damage { get; protected set; }
  public virtual int Range { get; set; }

  public virtual ColoredGlyph Appearance { get; set; }
  protected virtual ColoredGlyph OriginalAppearance { get; set; }
  public ColoredGlyph MapAppearance = new ColoredGlyph();

  protected IScreenObjectManager ScreenObjectManager;

  private IMovementLogic _movement;

  protected IMap _map;


  /// <summary>
  /// Constructor for GameObject.
  /// </summary>
  /// <param name="appearance"></param>
  /// <param name="position"></param>
  /// <param name="screenObjectManager"></param>
  /// <param name="map"></param>
  /// <param name="movementLogic"></param>
  protected GameObject(ColoredGlyph appearance, Point position, IScreenObjectManager screenObjectManager, IMap map, IMovementLogic? movementLogic = null)
  {
    Appearance = appearance;
    OriginalAppearance = appearance;
    Position = position;
    ScreenObjectManager = screenObjectManager;
    _map = map;
    _movement = movementLogic ?? new Movements(map, screenObjectManager);

    // Store the map cell
    MapAppearance = screenObjectManager.GetScreenObject(position);


    // Draw the object
    screenObjectManager.DrawScreenObject(this, position);
  }

  /*public void RestoreMap(Map map)
  {

    //_mapAppearance.CopyAppearanceTo(map.SurfaceObject.Surface[Position]);
  }*/

  /// <summary>
  /// Moves the object to the given position on the map.
  /// </summary>
  /// <param name="newPosition"></param>
  /// <param name="map"></param>
  /// <returns></returns>
  public virtual bool Move(Point newPosition, IMap map)
  {
    return _movement.Move(this, map, newPosition);
  }

  /// <summary>
  /// Defines what should happen if another object touches the current one.
  /// </summary>
  /// <param name="source"></param>
  /// <param name="map"></param>
  /// <returns></returns>
  public virtual bool Touched(IGameObject source)
  {
    source.Touching(this);
    return false;
  }
  /// <summary>
  /// Defines what should happen if the current object touches another object.
  /// </summary>
  /// <param name="source"></param>
  public virtual void Touching(IGameObject source)
  {
  }
  /// <summary>
  /// Updates the object.
  /// </summary>
  public virtual void Update()
  {

  }
  /// <summary>
  /// Removes the object from the map.
  /// </summary>
  public virtual void RemoveSelf()
  {
    _map.RemoveMapObject(this);
    ScreenObjectManager.RefreshCell(_map, Position);
  }
  /// <summary>
  /// Gets the position of the object.
  /// </summary>
  /// <returns>Point</returns>
  public virtual Point GetPosition()
  {
    return Position;
  }
  /// <summary>
  /// Gets the appearance of the object.
  /// </summary>
  /// <returns></returns>
  public virtual ColoredGlyph GetAppearance()
  {
    return Appearance;
  }

  /// <summary>
  /// Method <c>TakeDamage</c> reduces the health of the monster by a given amount.
  /// </summary>
  /// <param name="map"></param>
  /// <param name="source"></param>
  /// <param name="damage"></param>
  /// <returns></returns>
  public virtual bool TakeDamage(Map map, IGameObject source, int damage)
  {
    return false;
  }

  /// <summary>
  /// Method <c>SetMovementLogic</c> sets the movement logic for the object.
  /// </summary>
  /// <param name="movementLogic"></param>
  public void SetMovementLogic(IMovementLogic movementLogic)
  {
    _movement = movementLogic;
  }
}