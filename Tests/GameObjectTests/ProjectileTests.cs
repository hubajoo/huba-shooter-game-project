using DungeonCrawl.GameObjects;
using DungeonCrawl.GameObjects.ObjectInterfaces;
using DungeonCrawl.Mechanics;
using DungeonCrawl.Maps;
using Moq;
using NUnit.Framework;
using SadRogue.Primitives;
using SadConsole;

namespace Tests.GameObjectTests
{
  [TestFixture]
  public class ProjectileTests
  {
    private Mock<IScreenObjectManager> _mockScreenObjectManager;
    private Mock<IMap> _mockMap;
    private Mock<IMovementLogic> _mockMovementLogic;
    private Projectile _projectile;
    private Point _position = new Point(1, 1);
    private int _damage = 5;
    private int _maxDistance = 10;
    private Direction _direction = Direction.Right;
    private Color _color = Color.Red;

    [SetUp]
    public void SetUp()
    {
      _mockScreenObjectManager = new Mock<IScreenObjectManager>();
      _mockMap = new Mock<IMap>();
      _mockMovementLogic = new Mock<IMovementLogic>();
      _projectile = new Projectile(_position, _direction, _mockScreenObjectManager.Object, _maxDistance, _damage, _color, _mockMap.Object);

      _projectile.SetMovementLogic(_mockMovementLogic.Object);
    }

    [Test]
    public void Constructor_SetsProperties()
    {
      Assert.AreEqual(_position, _projectile.Position);
      Assert.AreEqual(_direction, _projectile.Direction);
      Assert.AreEqual(_damage, _projectile.Damage);
      Assert.AreEqual(_color, _projectile.Appearance.Foreground);
    }

    [Test]
    public void GetDamage_ReturnsCorrectDamage()
    {
      Assert.AreEqual(5, _projectile.GetDamage());
    }

    [Test]
    public void Touched_ChangesAppearanceAndStopsProjectile()
    {
      var initialAppearance = _projectile.Appearance;
      _projectile.Touched(new Mock<IGameObject>().Object);
      Assert.AreNotEqual(initialAppearance, _projectile.Appearance);
      Assert.AreEqual(Direction.None, _projectile.Direction);
    }

    [Test]
    public void Fly_RemovesProjectileWhenMaxDistanceReached()
    {
      // _mockMovementLogic.Setup(m => m.Move(It.IsAny<IGameObject>(), It.IsAny<IMap>(), It.IsAny<Point>())).Returns(true);
      for (int i = 0; i < 11; i++)
      {
        _projectile.Fly();
      }
      _mockMap.Verify(m => m.RemoveMapObject(_projectile), Times.Once);
    }

    [Test]
    public void Update_CallsFly()
    {
      var mockProjectile = new Mock<Projectile>(new Point(1, 1), Direction.Right, _mockScreenObjectManager.Object, 5, 10, Color.Red, _mockMap.Object) { CallBase = true };
      mockProjectile.Object.SetMovementLogic(_mockMovementLogic.Object);
      mockProjectile.Object.Update();
      mockProjectile.Verify(p => p.Fly(), Times.Once);
    }
  }
}