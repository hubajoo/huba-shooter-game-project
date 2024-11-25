using DungeonCrawl.GameObjects.ObjectInterfaces;
using DungeonCrawl.GameObjects;
using DungeonCrawl.UI;
using SadConsole;
using SadRogue.Primitives;
using DungeonCrawl.Mechanics;
using NUnit.Framework;
using Moq;
/
namespace Tests.GameObjectTests;

public class GameObjectTests
{
  private GameObject _gameObject;
  private ColoredGlyph _appearance;
  private Point _position;
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
    _gameObject = new Mock<GameObject>(_appearance, _position, _screenObjectManager.Object, _map.Object, _movementLogic.Object).Object;
  }

  [Test]
  public void Constructor_SetsProperties()
  {
    /*
        var constructorTestGameobject = new GameObject(_appearance, _position, _screenObjectManager.Object,
         _map.Object, _movementLogic.Object);

        Assert.That(constructorTestGameobject.Appearance, Is.EqualTo(_appearance));
        Assert.That(constructorTestGameobject.Position, Is.EqualTo(_position));
        Assert.That(constructorTestGameobject.ScreenObjectManager, Is.EqualTo(_screenObjectManager.Object));
        Assert.That(constructorTestGameobject._map, Is.EqualTo(_map.Object));
        Assert.That(constructorTestGameobject._movement, Is.EqualTo(_movementLogic.Object));
        */
/* }

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
}*/