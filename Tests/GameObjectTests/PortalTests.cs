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

public class PortalTests
{
  private Mock<IScreenObjectManager> _screenObjectManager;
  private Mock<IMap> _map;
  private Mock<IMovementLogic> _movementLogic;
  private ColoredGlyph _appearance;
  private Point _position;
  private Portal _gameObject;


  [SetUp]
  public void Setup()
  {
    _appearance = new ColoredGlyph(Color.Gray, Color.Red, 0);
    _position = new Point(1, 1);

    _screenObjectManager = new Mock<IScreenObjectManager>();
    _map = new Mock<IMap>();
    _movementLogic = new Mock<IMovementLogic>();

    _gameObject = new Portal(_position, _screenObjectManager.Object, _map.Object);
  }

  [Test]
  public void Constructor_SetsProperties()
  {
    var constructorTestGameobject = new Portal(_position, _screenObjectManager.Object, _map.Object);

    var portalDefaultAppearance = new ColoredGlyph(Color.Gray, Color.Red, 0);

    Assert.That(constructorTestGameobject.Appearance.Foreground, Is.EqualTo(portalDefaultAppearance.Foreground));
    Assert.That(constructorTestGameobject.Appearance.Background, Is.EqualTo(portalDefaultAppearance.Background));
    Assert.That(constructorTestGameobject.Appearance.Glyph, Is.EqualTo(portalDefaultAppearance.Glyph));
    Assert.That(constructorTestGameobject.Position, Is.EqualTo(_position));
  }

  [Test]
  public void Blocks_Movement()
  {
    var portal = new Portal(_position, _screenObjectManager.Object, _map.Object);
    var source = new Mock<IGameObject>();

    bool result = portal.Touched(source.Object);
    Assert.That(result, Is.False);
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
  public void Touched_Calls_TouchingOnSource()
  {
    var source = new Mock<IGameObject>();
    _gameObject.Touched(source.Object);

    source.Verify(m => m.Touching(_gameObject), Times.Once);
  }
}

