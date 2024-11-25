using DungeonCrawl.GameObjects;
using SadRogue.Primitives;
using SadConsole;
using DungeonCrawl.Mechanics;


namespace Tests.Wrappers;

/// <summary>
/// Class <c>TestGameObject</c> implements a pure object for testing purposes.
/// </summary>
public class TestGameObject : GameObject
{

  /// <summary>
  /// Constructor for TestGameObject.
  /// </summary>
  /// <param name="apperance"></param>
  /// <param name="position"></param>
  /// <param name="screenObjectManager"></param>
  /// <param name="map"></param>
  /// <param name="movementLogic"></param>
  public TestGameObject(ColoredGlyph apperance, Point position, IScreenObjectManager screenObjectManager, IMap map, IMovementLogic? movementLogic)
      : base(apperance, position, screenObjectManager, map, movementLogic)
  {
  }

}
