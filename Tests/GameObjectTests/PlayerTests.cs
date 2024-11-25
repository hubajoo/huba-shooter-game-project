using DungeonCrawl.GameObjects;
using DungeonCrawl.GameObjects.ObjectInterfaces;
using DungeonCrawl.GameObjects.Items;
using DungeonCrawl.Maps;
using Moq;
using NUnit.Framework;
using SadRogue.Primitives;

namespace DungeonCrawl.Tests
{
  [TestFixture]
  public class PlayerTests
  {
    private Mock<IScreenObjectManager> _mockScreenObjectManager;
    private Mock<IMap> _mockMap;
    private Player _player;

    [SetUp]
    public void SetUp()
    {
      _mockScreenObjectManager = new Mock<IScreenObjectManager>();
      _mockMap = new Mock<IMap>();
      _player = new Player("TestPlayer", new Point(0, 0), _mockScreenObjectManager.Object, _mockMap.Object);
    }

    [Test]
    public void Player_InitialHealth_IsCorrect()
    {
      Assert.AreEqual(100, _player.Health);
    }

    [Test]
    public void Player_TakeDamage_DecreasesHealth()
    {
      _player.TakeDamage(20);
      Assert.AreEqual(80, _player.Health);
    }

    [Test]
    public void Player_TakeDamage_HealthReachesZero_PlayerRemovedFromMap()
    {
      _player.TakeDamage(100);
      _mockMap.Verify(m => m.RemoveMapObject(_player), Times.Once);
    }

    [Test]
    public void Player_Touched_IncreasesRange_WhenRangeBonusTouched()
    {
      var rangeBonus = new RangeBonus(new Point(1, 1), _mockScreenObjectManager.Object, _mockMap.Object);
      _player.Range = 5;
      rangeBonus.Touched(_player);
      Assert.AreEqual(8, _player.Range);
    }

    [Test]
    public void Player_Shoot_CreatesProjectile()
    {
      _player.Direction = Direction.Right;
      var result = _player.CreateProjectile(_player.Position, _player.Direction, Color.Orange, _player.Damage, _player.Range);
      Assert.IsTrue(result);
      _mockMap.Verify(m => m.AddMapObject(It.IsAny<Projectile>()), Times.Once);
    }

    [Test]
    public void Player_Killed_IncreasesKillCount()
    {
      var mockVictim = new Mock<IGameObject>();
      _player.Killed(mockVictim.Object);
      Assert.AreEqual(1, _player.Kills);
    }

    [Test]
    public void Player_ChangeDirection_UpdatesDirection()
    {
      _player.ChangeDirection(Direction.Up);
      Assert.AreEqual(Direction.Up, _player.Direction);
    }

    [Test]
    public void Player_Touched_TakesDamage_WhenDamagedByProjectile()
    {
      var projectile = new Projectile(new Point(1, 1), Direction.Right, _mockScreenObjectManager.Object, 5, 10, Color.Red, _mockMap.Object);
      _player.Touched(projectile);
      Assert.AreEqual(90, _player.Health);
    }

    [Test]
    public void Player_EndGame_RemovesPlayerAndEndsGame()
    {
      _player.EndGame();
      _mockMap.Verify(m => m.RemoveMapObject(_player), Times.Once);
      _mockScreenObjectManager.Verify(m => m.End(), Times.Once);
    }
  }
}