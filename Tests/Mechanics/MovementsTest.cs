using DungeonCrawl.GameObjects;
using DungeonCrawl.GameObjects.ObjectInterfaces;
using DungeonCrawl.Maps;
using DungeonCrawl.Mechanics;
using Moq;
using NUnit.Framework;
using SadRogue.Primitives;
using SadConsole;

namespace Tests.Mechanics
{
  [TestFixture]
  public class MovementsTests
  {
    private Mock<IMap> _mockMap;
    private Mock<IScreenObjectManager> _mockScreenObjectManager;
    private Movements _movements;
    private Mock<IGameObject> _mockGameObject;
    private Point _initialPosition;
    private Point _newPosition;

    [SetUp]
    public void SetUp()
    {
      _mockMap = new Mock<IMap>();
      _mockScreenObjectManager = new Mock<IScreenObjectManager>();
      _movements = new Movements(_mockMap.Object, _mockScreenObjectManager.Object);
      _mockGameObject = new Mock<IGameObject>();
      _initialPosition = new Point(5, 5);
      _newPosition = new Point(6, 5);

      _mockGameObject.Setup(g => g.Position).Returns(_initialPosition);
    }
    /*

    [Test]
    public void Move_ValidMove_UpdatesPosition()
    {
      _mockMap.Setup(m => m.SurfaceObject.Surface.IsValidCell(_newPosition.X, _newPosition.Y)).Returns(true);
      _mockMap.Setup(m => m.TryGetMapObject(_newPosition, out It.Ref<IGameObject>.IsAny)).Returns(false);

      bool result = _movements.Move(_mockGameObject.Object, _mockMap.Object, _newPosition);

      Assert.IsTrue(result);
      _mockGameObject.VerifySet(g => g.Position = _newPosition);
      _mockScreenObjectManager.Verify(m => m.RefreshCell(_mockMap.Object, _newPosition), Times.Once);
      _mockScreenObjectManager.Verify(m => m.RefreshCell(_mockMap.Object, _initialPosition), Times.Once);
    }

    [Test]
    public void Move_InvalidMove_ReturnsFalse()
    {
      _mockMap.Setup(m => m.SurfaceObject.Surface.IsValidCell(_newPosition.X, _newPosition.Y)).Returns(false);

      bool result = _movements.Move(_mockGameObject.Object, _mockMap.Object, _newPosition);

      Assert.IsFalse(result);
      _mockGameObject.VerifySet(g => g.Position = It.IsAny<Point>(), Times.Never);
      _mockScreenObjectManager.Verify(m => m.RefreshCell(_mockMap.Object, _newPosition), Times.Never);
      _mockScreenObjectManager.Verify(m => m.RefreshCell(_mockMap.Object, _initialPosition), Times.Never);
    }

    [Test]
    public void Move_TouchesOtherObject_ReturnsFalse()
    {
      var mockOtherGameObject = new Mock<IGameObject>();
      _mockMap.Setup(m => m.SurfaceObject.Surface.IsValidCell(_newPosition.X, _newPosition.Y)).Returns(true);
      _mockMap.Setup(m => m.TryGetMapObject(_newPosition, out It.Ref<IGameObject>.IsAny)).Returns(true).Callback((Point p, out IGameObject g) => g = mockOtherGameObject.Object);
      mockOtherGameObject.Setup(o => o.Touched(_mockGameObject.Object)).Returns(false);

      bool result = _movements.Move(_mockGameObject.Object, _mockMap.Object, _newPosition);

      Assert.IsFalse(result);
      _mockGameObject.VerifySet(g => g.Position = It.IsAny<Point>(), Times.Never);
      _mockScreenObjectManager.Verify(m => m.RefreshCell(_mockMap.Object, _newPosition), Times.Never);
      _mockScreenObjectManager.Verify(m => m.RefreshCell(_mockMap.Object, _initialPosition), Times.Never);
    }

    [Test]
    public void Move_TouchesOtherObject_ReturnsTrue()
    {
      var mockOtherGameObject = new Mock<IGameObject>();
      _mockMap.Setup(m => m.SurfaceObject.Surface.IsValidCell(_newPosition.X, _newPosition.Y)).Returns(true);
      _mockMap.Setup(m => m.TryGetMapObject(_newPosition, out It.Ref<IGameObject>.IsAny)).Returns(true).Callback((Point p, out IGameObject g) => g = mockOtherGameObject.Object);
      mockOtherGameObject.Setup(o => o.Touched(_mockGameObject.Object)).Returns(true);

      bool result = _movements.Move(_mockGameObject.Object, _mockMap.Object, _newPosition);

      Assert.IsTrue(result);
      _mockGameObject.VerifySet(g => g.Position = _newPosition);
      _mockScreenObjectManager.Verify(m => m.RefreshCell(_mockMap.Object, _newPosition), Times.Once);
      _mockScreenObjectManager.Verify(m => m.RefreshCell(_mockMap.Object, _initialPosition), Times.Once);
    }
    */
  }
}