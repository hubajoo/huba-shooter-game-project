namespace DungeonCrawl.Mechanics;

using DungeonCrawl.GameObjects;
using DungeonCrawl.Maps;
using SadConsole;
using SadRogue.Primitives;

public class Movement : IMovementLogic
{

  private Map _map;

  private ScreenObjectManager _screenObjectManager;

  public Movement(Map map, ScreenObjectManager screenObjectManager)
  {
    _map = map;
    _screenObjectManager = screenObjectManager;
  }


  public bool Move(GameObject gameObject, Map map, Point newPosition)
  {
    // Check new position is valid
    if (!map.SurfaceObject.Surface.IsValidCell(newPosition.X, newPosition.Y)) return false;

    // Check if other object is there
    if (map.TryGetMapObject(newPosition, out GameObject foundObject))
    {
      // We touched the other object, but they won't allow us to move into the space
      if (!foundObject.Touched(gameObject, map))
      {
        return false;
      }
    }

    // Restore the old cell
    GameObject cellContent;
    if (map.TryGetMapObject(gameObject.Position, out cellContent, gameObject))
    {
      map.SurfaceObject.Surface[gameObject.Position].CopyAppearanceFrom(cellContent.Appearance);
      /*
       map.SurfaceObject.Surface[Position].CopyAppearanceFrom(
           new ColoredGlyph(Color.Transparent, Color.Transparent, 0));
      // */
    }
    else
    {
      map.SurfaceObject.Surface[gameObject.Position].CopyAppearanceFrom(
          new ColoredGlyph(Color.White, Color.Transparent, 0));
    }

    // Store the map cell of the new position
    map.SurfaceObject.Surface[newPosition].CopyAppearanceTo(gameObject._mapAppearance);

    gameObject.Position = newPosition;
    _screenObjectManager.DrawScreenObject(gameObject, newPosition);
    return true;
  }

}