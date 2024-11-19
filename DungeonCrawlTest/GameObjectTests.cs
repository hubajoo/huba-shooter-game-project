using DungeonCrawl.GameObjects;
using DungeonCrawl.Maps;
using NUnit.Framework;
using Moq;
using SadConsole;
using SadRogue.Primitives;
using System.Collections.ObjectModel;

namespace DungeonCrawlTest;

[TestFixture]
public class GameObjectsTests
{
  private Mock<IScreenSurface> mockScreenSurface;
  private Mock<IScreenObjectManager> mockScreenObjectManager;
  private Map map;
  private Player player;

  [SetUp]
  public void Setup()
  {
    mockScreenSurface = new Mock<IScreenSurface>();
    // mockScreenSurface.Setup(s => s.UseMouse).Returns(false);
    //mockScreenSurface.SetupGet(s => s.Surface.Width).Returns(3);
    //mockScreenSurface.SetupGet(s => s.Surface.Height).Returns(3);
    //mockScreenSurface.SetupGet(s => s.Surface.Area.Center).Returns(new Point(1, 1));

    mockScreenObjectManager = new Mock<ScreenObjectManager>(mockScreenSurface.Object);

    map = new Map(mockScreenSurface.Object, mockScreenObjectManager.Object);
    map.SurfaceObject.Position = new Point(0, 0);

    player = new Player(mockScreenSurface.Object.Surface.Area.Center, mockScreenObjectManager.Object, map);
    map.AddUserControlledObject(player);
  }

  [Test]
  public void PlayerCreationTest()
  {
    System.Console.WriteLine(player.Position);
    Assert.AreEqual(player.Position, mockScreenSurface.Object.Surface.Area.Center);
  }
  /*
    [Test]
    public void MapInitializationTest()
    {
      Assert.AreEqual(map.SurfaceObject.Position, new Point(0, 0));
    }

    [Test]
    public void PlayerAddedToMapTest()
    {
      Assert.Contains(player, map.GameObjects as Collection<IGameObject>);
    }
    */
}