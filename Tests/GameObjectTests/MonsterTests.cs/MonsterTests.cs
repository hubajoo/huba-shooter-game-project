/*
using DungeonCrawl.GameObjects;
using DungeonCrawl.GameObjects.Monsters;
using DungeonCrawl.Maps;
using Moq;
using NUnit.Framework;
using SadRogue.Primitives;

namespace Tests.GameObjectTests.MonsterTests
{
  [TestFixture]
  public class MonsterTests
  {
    private Mock<IScreenObjectManager> _mockScreenObjectManager;
    private Mock<IMap> _mockMap;
    private Monster _monster;

    [SetUp]
    public void SetUp()
    {
      _mockScreenObjectManager = new Mock<IScreenObjectManager>();
      _mockMap = new Mock<IMap>();
      _monster = new Monster(new ColoredGlyph(Color.Red, Color.Transparent, 'M'), new Point(0, 0), _mockScreenObjectManager.Object, 10, 2, _mockMap.Object);
    }

    [Test]
    public void Monster_InitialHealth_IsCorrect()
    {
      Assert.AreEqual(10, _monster.Health);
    }

    [Test]
    public void Monster_TakeDamage_DecreasesHealth()
    {
      _monster.TakeDamage(3);
      Assert.AreEqual(7, _monster.Health);
    }

    [Test]
    public void Monster_TakeDamage_HealthReachesZero_MonsterRemovedFromMap()
    {
      _monster.TakeDamage(10);
      _mockMap.Verify(m => m.RemoveMapObject(_monster), Times.Once);
    }

    [Test]
    public void Monster_TakeDamage_HealthReachesZero_PlayerKillCountIncreases()
    {
      var mockPlayer = new Mock<Player>("TestPlayer", new Point(0, 0), _mockScreenObjectManager.Object, _mockMap.Object);
      _mockMap.Setup(m => m.UserControlledObject).Returns(mockPlayer.Object);
      _monster.TakeDamage(10);
      mockPlayer.Verify(p => p.Killed(_monster), Times.Once);
    }

    [Test]
    public void Monster_Touched_TakesDamage_WhenDamagedByProjectile()
    {
      var mockProjectile = new Mock<IDamaging>();
      mockProjectile.Setup(p => p.GetDamage()).Returns(5);
      _monster.Touched(mockProjectile.Object);
      Assert.AreEqual(5, _monster.Health);
    }

    [Test]
    public void Monster_Update_CallsAiMoveAndAiAttack()
    {
      var mockMonster = new Mock<Monster>(new ColoredGlyph(Color.Red, Color.Transparent, 'M'), new Point(0, 0), _mockScreenObjectManager.Object, 10, 2, _mockMap.Object) { CallBase = true };
      mockMonster.Object.Update();
      mockMonster.Verify(m => m.AiMove(_mockMap.Object), Times.Once);
      mockMonster.Verify(m => m.AiAttack(_mockMap.Object), Times.Once);
    }

    [Test]
    public void Monster_AiMove_MovesInRandomDirection()
    {
      var initialPosition = _monster.Position;
      _monster.AiMove(_mockMap.Object);
      Assert.AreNotEqual(initialPosition, _monster.Position);
    }

    [Test]
    public void Monster_AiAttack_DoesNotThrowException()
    {
      Assert.DoesNotThrow(() => _monster.AiAttack(_mockMap.Object));
    }
  }
}
*/