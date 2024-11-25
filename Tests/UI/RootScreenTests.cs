using NUnit.Framework;
using Moq;
using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using DungeonCrawl.Ui;

namespace Tests.RootScreenTests
{
  public class RootScreenTests
  {
    private Mock<IMap> _mockMap;
    private Mock<IScreenSurface> _screenSurface;
    private RootScreen _rootScreen;


    [SetUp]
    public void Setup()
    {

      _mockMap = new Mock<IMap>();
      _screenSurface = new Mock<IScreenSurface>();
      _mockMap.Setup(m => m.SurfaceObject).Returns(_screenSurface.Object);
      _rootScreen = new RootScreen(_mockMap.Object);

    }

    [Test]
    public void Constructor_InitializesWithMap()
    {
      Assert.That(_rootScreen.Children.Contains(_mockMap.Object.SurfaceObject), Is.True);
    }


    [Test]
    public void Update_UpdatesMap()
    {
      var time = new TimeSpan(0, 0, 0, 0, 100);
      _rootScreen.Update(time);
      _mockMap.Verify(m => m.ProgressTime(), Times.Once);
    }

    [Test]
    public void Update_UpdatesChildren()
    {
      var time = new TimeSpan(0, 0, 0, 0, 100);
      _rootScreen.Update(time);
      _screenSurface.Verify(s => s.Update(time), Times.Once);
    }

    /// Keyboard inputs are not tested here, because the whole keyboard logic is prevritten SadConsole code.
  }
}

