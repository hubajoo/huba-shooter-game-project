using DungeonCrawl.GameObjects;
using DungeonCrawl.GameObjects.Items;
using DungeonCrawl.Maps;
using Moq;
using NUnit.Framework;
using SadRogue.Primitives;

namespace Tests.GameObjectTests.ItemTests
{

  public class PotionTests
  {
    private Mock<IScreenObjectManager> _mockScreenObjectManager;
    private Mock<IMap> _mockMap;
    private Player _player;
    private Potion _potion;

    [SetUp]
    public void SetUp()
    {
      _mockScreenObjectManager = new Mock<IScreenObjectManager>();
      _mockMap = new Mock<IMap>();
      _player = new Player("TestPlayer", new Point(0, 0), _mockScreenObjectManager.Object, _mockMap.Object);
      _potion = new Potion(new Point(1, 1), _mockScreenObjectManager.Object, _mockMap.Object);
    }

    [Test]
    public void Potion_InitialHealingAmount_IsCorrect()
    {
      Assert.AreEqual(25, _potion.Healing);
    }

    [Test]
    public void Potion_Touched_IncreasesPlayerHealth()
    {
      _player.Health = 50;
      _potion.Touched(_player);
      Assert.AreEqual(75, _player.Health);
    }

    [Test]
    public void Potion_Touched_RemovesPotionFromMap()
    {
      _potion.Touched(_player);
      _mockMap.Verify(m => m.RemoveMapObject(_potion), Times.Once);
    }
  }
}