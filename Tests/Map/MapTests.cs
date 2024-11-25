using DungeonCrawl.GameObjects.ObjectInterfaces;
using DungeonCrawl.GameObjects;
using DungeonCrawl.Mechanics.SpawnLogic;
using DungeonCrawl.GameObjects.Items;
using SadRogue.Primitives;
using SadConsole;
using NUnit.Framework;
using Moq;
using DungeonCrawl.Maps;


namespace Tests.MapTests;
public class MapTests
{
  private Mock<IScreenObjectManager> _screenObjectManager;
  private Mock<IScreenSurface> _screenSurface;
  private Map _map;
  private Mock<IGameObject> _gameObject;
  private Mock<Player> _player;
  private Mock<ISpawnOrchestrator> _spawnOrchestrator;



  [SetUp]
  public void Setup()
  {
    _screenObjectManager = new Mock<IScreenObjectManager>();
    _screenSurface = new Mock<IScreenSurface>();

    var appearance = new ColoredGlyph(Color.White, Color.Black, 'X');

    _gameObject = new Mock<IGameObject>();
    _gameObject.Setup(m => m.Appearance).Returns(appearance);
    _gameObject.Setup(m => m.GetAppearance()).Returns(appearance);
    _gameObject.Setup(m => m.Position).Returns(new Point(1, 1));

    Map map = new Map(_screenSurface.Object, _screenObjectManager.Object);

    _player = new Mock<Player>();
    _spawnOrchestrator = new Mock<ISpawnOrchestrator>();
  }


  [Test]
  public void AddUserControlledObject_SetsUserControlledObject()
  {
    _map.AddUserControlledObject(_player.Object);

    Assert.That(_map.UserControlledObject, Is.EqualTo(_player.Object));
  }


  [Test]
  public void AddMapObject_AddsObjectToMap()
  {
    _map.AddMapObject(_gameObject.Object);

    Assert.That(_map.GetGameObject(new Point(1, 1)), Is.EqualTo(_gameObject.Object));
  }

  [Test]
  public void SetSpawnLogic_SetsSpawnLogic()
  {
    _spawnOrchestrator.Setup(m => m.NoEnemiesLeft());
    _map.SetSpawnLogic(_spawnOrchestrator.Object);
    _map.ProgressTime();

    /// Verify that the NoEnemiesLeft method is called once, because the ProgressTime method calls it
    /// when there are no enemies left.
    _spawnOrchestrator.Verify(m => m.NoEnemiesLeft(), Times.Once);
  }

  [Test]
  public void TryGetMapObject_ReturnsTrueIfExists()
  {
    _map.AddMapObject(_gameObject.Object);

    bool result = _map.TryGetMapObject(new Point(1, 1), out IGameObject testGameObject);

    Assert.That(result, Is.True);
    Assert.That(testGameObject, Is.EqualTo(_gameObject.Object));
  }


  [Test]
  public void TryGetMapObject_ReturnsFalseIfNotExists()
  {
    bool result = _map.TryGetMapObject(new Point(1, 1), out IGameObject testGameObject);

    Assert.That(result, Is.False);
    Assert.That(testGameObject, Is.Null);
  }


  [Test]
  public void RemoveMapObject_RemovesObjectFromMap()
  {
    _map.AddMapObject(_gameObject.Object);
    _map.RemoveMapObject(_gameObject.Object);

    bool result = _map.TryGetMapObject(new Point(1, 1), out IGameObject gameObject);

    Assert.That(result, Is.False);
    Assert.That(gameObject, Is.Null);
  }

  [Test]
  public void ProgressTime_ProgressesTime()
  {
    // With no enemies left, the spawn orchestrator should be called
    _map.SetSpawnLogic(_spawnOrchestrator.Object);
    _map.ProgressTime();
    _spawnOrchestrator.Verify(m => m.NoEnemiesLeft(), Times.Once);

    // With an object in the map, the object should be updated
    _map.AddMapObject(_gameObject.Object);
    _map.ProgressTime();
    _gameObject.Verify(m => m.Update(), Times.Once);
  }

  [Test]
  public void DropLoot_DropsLoot()
  {
    _map.DropLoot(new Point(1, 1));

    bool result = _map.TryGetMapObject(new Point(1, 1), out IGameObject gameObject);

    Assert.That(result, Is.True);
    Assert.That(gameObject, Is.TypeOf<Item>());
  }
}




