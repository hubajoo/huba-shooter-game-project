using DungeonCrawl.GameObjects;
using DungeonCrawl.GameObjects.Items;
using DungeonCrawl.Maps;
using Moq;
using NUnit.Framework;
using SadRogue.Primitives;

namespace Tests.GameObjectTests.ItemTests
{
  [TestFixture]
  public class RangeBonusTests
  {
    private Mock<IScreenObjectManager> _mockScreenObjectManager;
    private Mock<IMap> _mockMap;
    private RangeBonus _rangeBonus;
    private Player _player;

    [SetUp]
    public void SetUp()
    {
      _mockScreenObjectManager = new Mock<IScreenObjectManager>();
      _mockMap = new Mock<IMap>();
      _rangeBonus = new RangeBonus(new Point(0, 0), _mockScreenObjectManager.Object, _mockMap.Object);
      _player = new Player("TestPlayer", new Point(1, 1), _mockScreenObjectManager.Object, _mockMap.Object);
    }

    [Test]
    public void RangeBonus_Touched_IncreasesPlayerRange()
    {
      _player.Range = 5;
      _rangeBonus.Touched(_player);
      Assert.AreEqual(8, _player.Range);
    }

    [Test]
    public void RangeBonus_Touched_RemovesSelfFromMap()
    {
      _rangeBonus.Touched(_player);
      _mockMap.Verify(m => m.RemoveMapObject(_rangeBonus), Times.Once);
    }
  }
}