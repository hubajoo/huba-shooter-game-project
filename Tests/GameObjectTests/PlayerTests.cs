using DungeonCrawl.GameObjects;
using DungeonCrawl.GameObjects.ObjectInterfaces;
using DungeonCrawl.GameObjects.Items;
using DungeonCrawl.Maps;
using DungeonCrawl.UI;
using Moq;
using NUnit.Framework;
using SadRogue.Primitives;
using SadConsole;

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
    public void Player_TakeDamage_Decrease_Health()
    {
      _player.TakeDamage(20);
      Assert.AreEqual(80, _player.Health);
    }

    [Test]
    public void Dead_Player_Removed_From_Map()
    {
      var player = new Player("TestPlayer", new Point(0, 0), _mockScreenObjectManager.Object, _mockMap.Object);
       player.TakeDamage(100);
      _mockMap.Verify(m => m.RemoveMapObject(player), Times.Once);
    }

    [Test]
    public void Player_Range_Increases_On_RangeBonus_Pickup()
    {
      var rangeBonus = new RangeBonus(new Point(1, 1), _mockScreenObjectManager.Object, _mockMap.Object);
      _player.Range = 5;
      rangeBonus.Touched(_player);
      Assert.AreEqual(8, _player.Range);
    }

    [Test]
    public void Player_Shoot_CreatesProjectile()
    {
      /// Shoot method creates a projectile
      /// It depends on the CellSurface IsValidCell method
      /// so it's testing requires a lot of rewriting

    }

    [Test]
    public void Killed_Method_IncreasesKillCount()
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
    public void Player_Takes_Damage_From_Projectiles()
    {
      var projectile = new Projectile(new Point(1, 1), Direction.Right, _mockScreenObjectManager.Object, 10, 10, Color.Red, _mockMap.Object);
      _player.Touched(projectile);
      Assert.AreEqual(90, _player.Health);
    }

    [Test]
    public void Player_EndGame_Removes_Player_And_EndsGame()
    {
      _player.EndGame();
      _mockMap.Verify(m => m.RemoveMapObject(_player), Times.Once);
      _mockScreenObjectManager.Verify(m => m.End(), Times.Once);
    }
  }
}