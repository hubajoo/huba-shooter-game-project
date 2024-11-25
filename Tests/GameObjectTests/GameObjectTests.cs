using Tests.Wrappers;
using DungeonCrawl.GameObjects.ObjectInterfaces;
using DungeonCrawl.GameObjects;
using DungeonCrawl.UI;
using DungeonCrawl.Mechanics;
using SadConsole;
using SadRogue.Primitives;
using DungeonCrawl.Mechanics;
using NUnit.Framework;
using Moq;

namespace Tests.GameObjectTests;

public class GameObjectTests
{
  private GameObject _gameObject;
  private ColoredGlyph _appearance;
  private Point _position;
  private Color _color;
  private Mock<IScreenObjectManager> _screenObjectManager;
  private Mock<IMap> _map;
  private Mock<IMovementLogic> _movementLogic;



  [SetUp]
  public void Setup()
  {
    _appearance = new ColoredGlyph(Color.White, Color.Black, 'X');
    _position = new Point(1, 1);

    _screenObjectManager = new Mock<IScreenObjectManager>();
    _map = new Mock<IMap>();

    _movementLogic = new Mock<IMovementLogic>();
    _movementLogic.Setup(m => m.Move(It.IsAny<IGameObject>(), It.IsAny<IMap>(), It.IsAny<Point>()));
    _gameObject = new TestGameObject(_appearance, _position, _screenObjectManager.Object, _map.Object, _movementLogic.Object);
  }

  [Test]
  public void Constructor_SetsProperties()
  {
    var constructorTestGameobject = new TestGameObject(_appearance, _position, _screenObjectManager.Object, _map.Object, _movementLogic.Object);
    Assert.That(constructorTestGameobject.Appearance, Is.EqualTo(_appearance));
    Assert.That(constructorTestGameobject.Position, Is.EqualTo(_position));
  }

  [Test]
  public void GetAppearance_ReturnsAppearance()
  {
    Assert.That(_gameObject.GetAppearance(), Is.EqualTo(_appearance));
  }

  [Test]
  public void GetPosition_ReturnsPosition()
  {
    Assert.That(_gameObject.Position, Is.EqualTo(new Point(1, 1)));
  }

  [Test]
  public void RemoveSelf_Calls_RemoveMapObject()
  {
    _gameObject.RemoveSelf();

    _map.Verify(m => m.RemoveMapObject(_gameObject), Times.Once);
  }

  [Test]
  public void Move_Calls_MoveOnMovementLogic()
  {
    var newPosition = new Point(2, 2);
    _gameObject.Move(newPosition, _map.Object);

    _movementLogic.Verify(m => m.Move(_gameObject, _map.Object, newPosition), Times.Once);
  }

  [Test]
  public void Touched_Calls_TouchingOnSource()
  {
    var source = new Mock<IGameObject>();
    _gameObject.Touched(source.Object);

    source.Verify(m => m.Touching(_gameObject), Times.Once);
  }

}

