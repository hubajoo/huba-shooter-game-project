using DungeonCrawl.GameObjects;
using DungeonCrawl.GameObjects.ObjectInterfaces;
using DungeonCrawl.GameObjects.Monsters;
using DungeonCrawl.Maps;
using DungeonCrawl.Mechanics;


using Tests.Wrappers;

using Moq;
using NUnit.Framework;
using SadConsole;
using SadRogue.Primitives;

namespace Tests.GameObjectTests.MonsterTests
{
  [TestFixture]
  public class MonsterTests
  {
    private Mock<IScreenObjectManager> _mockScreenObjectManager;
    private Mock<IMap> _mockMap;
    private Mock<IDirectionChoiche> _mockDirectionChoice;
    private Monster _monster;

    private ColoredGlyph _appearance = new ColoredGlyph(Color.Red, Color.Transparent, 'M');
    private Point _position = new Point(1, 1);
    private int _health = 10;
    private int _damage = 2;


    [SetUp]
    public void SetUp()
    {
      _mockScreenObjectManager = new Mock<IScreenObjectManager>();
      _mockMap = new Mock<IMap>();
      _mockDirectionChoice = new Mock<IDirectionChoiche>();

      _monster = new TestMonster(_appearance, _position, _mockScreenObjectManager.Object,
      _health, _damage, _mockMap.Object, _mockDirectionChoice.Object);
    }

    [Test]
    public void Monster_InitialHealth_IsCorrect()
    {
      Assert.AreEqual(_health, _monster.Health);
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
      var player = new Player("TestPlayer", new Point(0, 0), _mockScreenObjectManager.Object, _mockMap.Object);

      _mockMap.Setup(m => m.UserControlledObject).Returns(player);

      _monster.TakeDamage(10);
      _mockMap.Verify(m => m.RemoveMapObject(_monster), Times.Once);
      _mockMap.Verify(m => m.DropLoot(_position), Times.Once);
    }


    [Test]
    public void Monster_TakeDamage_HealthReachesZero_PlayerKillCountIncreases()
    {
      var player = new Player("TestPlayer", new Point(0, 0), _mockScreenObjectManager.Object, _mockMap.Object);

      _mockMap.Setup(m => m.UserControlledObject).Returns(player);

      var monster = new TestMonster(_appearance, _position, _mockScreenObjectManager.Object,
         _health, _damage, _mockMap.Object, _mockDirectionChoice.Object);

      monster.TakeDamage(10);
      _mockMap.Verify(m => m.RemoveMapObject(monster), Times.Once);
      _mockMap.Verify(m => m.DropLoot(_position), Times.Once);
      Assert.AreEqual(1, player.Kills);

    }


    [Test]
    public void Monster_Touched_TakesDamage_WhenDamagedByProjectile()
    {
      int mHealth = 20;
      int pDamage = 10;

      Monster monster = new TestMonster(_appearance, _position, _mockScreenObjectManager.Object,
        mHealth, _damage, _mockMap.Object, _mockDirectionChoice.Object);

      IGameObject projectile = new Projectile(new Point(1, 1), Direction.Right, _mockScreenObjectManager.Object,
      pDamage, 20, Color.Red, _mockMap.Object);

      monster.Touched(projectile);
      Assert.AreEqual(mHealth - pDamage, monster.Health);
    }

    //Update method calls AiMove and AiAttack
    [Test]
    public void Monster_Update_CallsAiMove()
    {
      TestMonster monster = new TestMonster(_appearance, _position, _mockScreenObjectManager.Object,
        _health, _damage, _mockMap.Object, _mockDirectionChoice.Object);

      Assert.That(monster.AiMoveCalled, Is.False);
      Assert.That(monster.AiMoveCalled, Is.False);

      monster.Update();
      Assert.That(monster.AiMoveCalled, Is.True);
      Assert.That(monster.AiMoveCalled, Is.True);
    }


    [Test]
    public void Map_ProgressTime_UpdatesMonsters()
    {
      var screenSurface = new Mock<IScreenSurface>();
      screenSurface.Setup(m => m.Surface.Width).Returns(10);
      screenSurface.Setup(m => m.Surface.Height).Returns(10);

      var map = new Map(screenSurface.Object, _mockScreenObjectManager.Object);

      TestMonster monster = new TestMonster(_appearance, _position, _mockScreenObjectManager.Object,
   _health, _damage, map, _mockDirectionChoice.Object);

      map.AddMapObject(monster);
      map.ProgressTime();

      Assert.That(monster.Updated, Is.True);
    }
  }
}
