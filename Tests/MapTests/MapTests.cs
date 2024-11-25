using DungeonCrawl.GameObjects.ObjectInterfaces;
using DungeonCrawl.GameObjects;
using DungeonCrawl.Mechanics.SpawnLogic;
using DungeonCrawl.GameObjects.Items;
using DungeonCrawl.Maps;
using SadRogue.Primitives;
using SadConsole;
using NUnit.Framework;
using Moq;



namespace Tests.MapTests;
public class MapTests
{
  private Mock<IScreenObjectManager> _screenObjectManager;
  private Mock<IScreenSurface> _screenSurface;
  private Mock<IGameObject> _gameObject;
  private Player _player; // Mock<Player> doesn't work because Player implements GameObject class
  private Mock<ISpawnOrchestrator> _spawnOrchestrator;

  [SetUp]
  public void Setup()
  {
    _screenObjectManager = new Mock<IScreenObjectManager>();

    _screenSurface = new Mock<IScreenSurface>();
    _screenSurface.Setup(m => m.Surface.Width).Returns(10);
    _screenSurface.Setup(m => m.Surface.Height).Returns(10);

    var appearance = new ColoredGlyph(Color.White, Color.Black, 'X');

    _gameObject = new Mock<IGameObject>();
    _gameObject.Setup(m => m.Appearance).Returns(appearance);
    _gameObject.Setup(m => m.GetAppearance()).Returns(appearance);
    _gameObject.Setup(m => m.Position).Returns(new Point(1, 1));
    _gameObject.Setup(m => m.GetPosition()).Returns(new Point(1, 1));

    _spawnOrchestrator = new Mock<ISpawnOrchestrator>();



  }

  [Test]
  public void Constructor_SetsProperties()
  {
    var map = new Map(_screenSurface.Object, _screenObjectManager.Object);

    Assert.That(map.Width, Is.EqualTo(_screenSurface.Object.Surface.Width));
    Assert.That(map.Height, Is.EqualTo(_screenSurface.Object.Surface.Height));
    Assert.That(map.SurfaceObject, Is.EqualTo(_screenSurface.Object));
    Assert.That(map.UserControlledObject, Is.Null);
    Assert.That(map.GetGameObject(new Point(1, 1)), Is.Null);
  }


  [Test]
  public void AddUserControlledObject_SetsUserControlledObject()
  {
    var map = new Map(_screenSurface.Object, _screenObjectManager.Object);

    var player = new Player("Player", new Point(1, 1), _screenObjectManager.Object, map);
    map.AddUserControlledObject(player);

    Assert.That(map.UserControlledObject, Is.EqualTo(player));
  }


  [Test]
  public void AddMapObject_AddsObjectToMap()
  {
    var map = new Map(_screenSurface.Object, _screenObjectManager.Object);
    var player = new Player("Player", new Point(2, 2), _screenObjectManager.Object, map);
    map.AddUserControlledObject(player);


    map.AddMapObject(_gameObject.Object);

    Assert.That(map.GetGameObject(new Point(1, 1)), Is.EqualTo(_gameObject.Object));
  }

  [Test]
  public void SetSpawnLogic_SetsSpawnLogic()
  {
    var map = new Map(_screenSurface.Object, _screenObjectManager.Object);
    var player = new Player("Player", new Point(1, 1), _screenObjectManager.Object, map);
    map.AddUserControlledObject(player);


    _spawnOrchestrator.Setup(m => m.NoEnemiesLeft());
    map.SetSpawnLogic(_spawnOrchestrator.Object);
    map.ProgressTime();

    /// Verify that the NoEnemiesLeft method is called once, because the ProgressTime method calls it
    /// when there are no enemies left.
    _spawnOrchestrator.Verify(m => m.NoEnemiesLeft(), Times.Once);
  }

  [Test]
  public void TryGetMapObject_ReturnsTrueIfExists()
  {
    var map = new Map(_screenSurface.Object, _screenObjectManager.Object);
    var player = new Player("Player", new Point(2, 2), _screenObjectManager.Object, map);
    map.AddUserControlledObject(player);


    map.AddMapObject(_gameObject.Object);

    bool result = map.TryGetMapObject(new Point(1, 1), out IGameObject? testGameObject);

    Assert.That(result, Is.True);
    Assert.That(testGameObject, Is.EqualTo(_gameObject.Object));

    // TryGetMapObject with a third parameter excludes the object passed as the third parameter
    result = map.TryGetMapObject(new Point(1, 1), out testGameObject, _gameObject.Object);

    Assert.That(result, Is.False);
    Assert.That(testGameObject, Is.Null);


    // Add another object to the map with the same position
    var secondGameObject = new Mock<IGameObject>();
    secondGameObject.Setup(m => m.Position).Returns(new Point(1, 1));
    secondGameObject.Setup(m => m.GetPosition()).Returns(new Point(1, 1));
    map.AddMapObject(secondGameObject.Object);


    result = map.TryGetMapObject(new Point(1, 1), out IGameObject? secondTestGameObject, _gameObject.Object);

    Assert.That(result, Is.True);
    Assert.That(secondTestGameObject, Is.EqualTo(secondGameObject.Object));
  }


  [Test]
  public void TryGetMapObject_ReturnsFalseIfNotExists()
  {
    var map = new Map(_screenSurface.Object, _screenObjectManager.Object);
    var player = new Player("Player", new Point(2, 2), _screenObjectManager.Object, map);
    map.AddUserControlledObject(player);


    bool result = map.TryGetMapObject(new Point(1, 1), out IGameObject? testGameObject);

    Assert.That(result, Is.False);
    Assert.That(testGameObject, Is.Null);
  }


  [Test]
  public void RemoveMapObject_RemovesObjectFromMap()
  {
    var map = new Map(_screenSurface.Object, _screenObjectManager.Object);
    var player = new Player("Player", new Point(2, 2), _screenObjectManager.Object, map);
    map.AddUserControlledObject(player);


    map.AddMapObject(_gameObject.Object);
    map.RemoveMapObject(_gameObject.Object);

    IGameObject? gameObject;

    bool result = map.TryGetMapObject(new Point(1, 1), out gameObject);

    Assert.That(result, Is.False);
    Assert.That(gameObject, Is.Null);
  }

  [Test]
  public void ProgressTime_ProgressesTime()
  {
    var map = new Map(_screenSurface.Object, _screenObjectManager.Object);
    var player = new Player("Player", new Point(1, 1), _screenObjectManager.Object, map);
    map.AddUserControlledObject(player);

    // With no enemies left, the spawn orchestrator should be called
    map.SetSpawnLogic(_spawnOrchestrator.Object);
    map.ProgressTime();
    _spawnOrchestrator.Verify(m => m.NoEnemiesLeft(), Times.Once);

    // With an object in the map, the object should be updated
    map.AddMapObject(_gameObject.Object);
    map.ProgressTime();
    _gameObject.Verify(m => m.Update(), Times.Once);
  }

  [Test]
  public void DropLoot_DropsLoot()
  {
    var map = new Map(_screenSurface.Object, _screenObjectManager.Object);
    var player = new Player("Player", new Point(3, 3), _screenObjectManager.Object, map);
    map.AddUserControlledObject(player);

    map.DropLoot(new Point(1, 1));

    bool result = map.TryGetMapObject(new Point(1, 1), out IGameObject? gameObject);

    Assert.That(result, Is.True);
    Assert.That(gameObject is Item, Is.True);
  }
}




