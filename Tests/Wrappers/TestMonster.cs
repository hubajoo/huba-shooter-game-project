using SadRogue.Primitives;
using SadConsole;
using DungeonCrawl.GameObjects.ObjectInterfaces;
using DungeonCrawl.Maps;
using DungeonCrawl.GameObjects.Monsters;
using DungeonCrawl.Mechanics;


namespace Tests.Wrappers;

/// <summary>
/// Class <c>TestGameObject</c> implements a pure object for testing purposes.
/// </summary>
public class TestMonster : Monster
{

  public bool AiAttackCalled { get; private set; } = false;
  public bool AiMoveCalled { get; private set; } = false;
  public bool Updated { get; private set; } = false;

  /// <summary>
  /// Constructor for TestGameObject.
  /// </summary>
  /// <param name="apperance"></param>
  /// <param name="position"></param>
  /// <param name="screenObjectManager"></param>
  /// <param name="map"></param>
  /// <param name="movementLogic"></param>
  public TestMonster(ColoredGlyph appearance, Point position, IScreenObjectManager screenObjectManager, int health, int damage, IMap map, IDirectionChoiche directionChoice)
      : base(appearance, position, screenObjectManager, health, damage, map, directionChoice)
  {
  }

  protected override void AiAttack(IMap map)
  {
    AiAttackCalled = true;
  }

  protected override void AiMove(IMap map)
  {
    AiMoveCalled = true;
  }

  public override void Update()
  {
    base.Update();
    Updated = true;
  }

}
