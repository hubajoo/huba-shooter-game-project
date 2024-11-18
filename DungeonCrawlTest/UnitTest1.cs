using DungeonCrawl.GameObjects;
using DungeonCrawl.Maps;
using SadConsole;
using SadRogue.Primitives;

namespace DungeonCrawlTest;

public class Tests
{
    int mapWidth;
    int mapHeight;
    private ScreenSurface screenSurface;
    private ScreenObjectManager screenObjectManager;
    private Map map;
    private Player player;
    
    [SetUp]
    public void Setup()
    {
        mapWidth = 3;
        mapHeight = 3;
        
        // Creates screensurface
        screenSurface = new ScreenSurface(mapWidth, mapHeight);
        screenSurface.UseMouse = false;

        // Drawing logic setup
        screenObjectManager = new ScreenObjectManager(screenSurface);
        
        // Creates the map
        map = new Map(screenSurface, screenObjectManager);
        map.SurfaceObject.Position = new Point(0, 0);

        // Creates player
        player = new Player(screenSurface.Surface.Area.Center, screenObjectManager, map);
        map.AddUserControlledObject(player);
    }

    
    [Test]
    public void SucceesfullMoveTest()
    {
        player.Move(new Point(0, 0), map);
        Assert.Equals( player.Position , new Point(0,0));
    }
    
    [Test]
    public void screenSurfaceCreationTest()
    {
        var screenSurface = new ScreenSurface(mapWidth, mapHeight);
        Assert.Equals( screenSurface.);
    }
}