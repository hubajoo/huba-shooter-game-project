using DungeonCrawl.GameObjects;
using DungeonCrawl.GameObjects.Monsters;
using DungeonCrawl.GameObjects.ObjectInterfaces;
using DungeonCrawl.Mechanics.SpawnLogic;
using Moq;
using NUnit.Framework;
using SadRogue.Primitives;

namespace Tests.Mechanics.SpawnLogic
{
  [TestFixture]
  public class MonsterCreationTests
  {
    private Mock<IScreenObjectManager> _mockScreenObjectManager;
    private Mock<IMap> _mockMap;
    private Point _position;

    [SetUp]
    public void SetUp()
    {
      _mockScreenObjectManager = new Mock<IScreenObjectManager>();
      _mockMap = new Mock<IMap>();
      _position = new Point(1, 1);
    }

    [Test]
    public void CreateMonsterTypes_ReturnsCorrectMonsterTypes()
    {
      var monsterTypes = MonsterCreation.CreateMonsterTypes();

      Assert.AreEqual(3, monsterTypes.Length);

      var orc = monsterTypes[0](_position, _mockScreenObjectManager.Object, _mockMap.Object);
      var goblin = monsterTypes[1](_position, _mockScreenObjectManager.Object, _mockMap.Object);
      var dragon = monsterTypes[2](_position, _mockScreenObjectManager.Object, _mockMap.Object);

      Assert.IsInstanceOf<Orc>(orc);
      Assert.IsInstanceOf<Goblin>(goblin);
      Assert.IsInstanceOf<Dragon>(dragon);
    }

    [Test]
    public void CreateMonsterTypes_OrcHasCorrectProperties()
    {
      var monsterTypes = MonsterCreation.CreateMonsterTypes();
      var orc = monsterTypes[0](_position, _mockScreenObjectManager.Object, _mockMap.Object);

      Assert.IsInstanceOf<Orc>(orc);
      Assert.AreEqual(_position, orc.Position);
    }

    [Test]
    public void CreateMonsterTypes_GoblinHasCorrectProperties()
    {
      var monsterTypes = MonsterCreation.CreateMonsterTypes();
      var goblin = monsterTypes[1](_position, _mockScreenObjectManager.Object, _mockMap.Object);

      Assert.IsInstanceOf<Goblin>(goblin);
      Assert.AreEqual(_position, goblin.Position);
    }

    [Test]
    public void CreateMonsterTypes_DragonHasCorrectProperties()
    {
      var monsterTypes = MonsterCreation.CreateMonsterTypes();
      var dragon = monsterTypes[2](_position, _mockScreenObjectManager.Object, _mockMap.Object);

      Assert.IsInstanceOf<Dragon>(dragon);
      Assert.AreEqual(_position, dragon.Position);

    }
  }
}