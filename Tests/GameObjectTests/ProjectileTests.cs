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
      _projectile = new Projectile(_position, _direction, _mockScreenObjectManager.Object, _damage, _maxDistance, _color, _mockMap.Object);

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
      var projectile = new Projectile(_position, _direction, _mockScreenObjectManager.Object, _damage, _maxDistance, _color, _mockMap.Object);
      var initialAppearance = projectile.Appearance;
      projectile.Touched(new Mock<IGameObject>().Object);
      Assert.AreNotEqual(initialAppearance, projectile.Appearance);
      Assert.AreEqual(Direction.None, projectile.Direction);
    }

    [Test]
    public void Update_RemovesProjectileWhenMaxDistanceReached()
    {
      var projectile = new Projectile(_position, Direction.Right, _mockScreenObjectManager.Object, _damage, 10, _color, _mockMap.Object);
      projectile.SetMovementLogic(_mockMovementLogic.Object);

      for (int i = 0; i < 10; i++)
      {
        _projectile.Fly();
      }
      _mockMap.Verify(m => m.RemoveMapObject(projectile), Times.Once);
    }

    [Test]
    public void Update_CallsFly()
    {
      var projectile = new Projectile(_position, _direction, _mockScreenObjectManager.Object, _damage, _maxDistance, _color, _mockMap.Object);

      projectile.SetMovementLogic(_mockMovementLogic.Object);

      projectile.Update();
      _mockMovementLogic.Verify(p => p.Move(projectile, It.IsAny<IMap>(), It.IsAny<Point>()), Times.Once);
    }
  }
}