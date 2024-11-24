using NUnit.Framework;
using Moq;
using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using DungeonCrawl.Ui;

/*
namespace Tests
{
  public class RootScreenTests
  {
    private Mock<IMap> _mockMap;
    private RootScreen _rootScreen;

    [SetUp]
    public void Setup()
    {
      _mockMap = new Mock<IMap>();
      _mockMap.Setup(m => m.SurfaceObject).Returns(new ScreenObject());
      _rootScreen = new RootScreen(_mockMap.Object);
    }
    
        [Test]
        public void Constructor_InitializesWithMap()
        {
          Assert.That(_rootScreen.Children.Contains(_mockMap.Object.SurfaceObject), Is.True);
        }

        [Test]
        public void ProcessKeyboard_EscapeKey_ReturnsTrue()
        {
          var keyboard = new Keyboard();
          keyboard.SetKeyState(Keys.Escape, true);

          bool result = _rootScreen.ProcessKeyboard(keyboard);

          Assert.That(result, Is.True);
        }

        [Test]
        public void ProcessKeyboard_OtherKey_ReturnsFalse()
        {
          var keyboard = new Keyboard();
          keyboard.SetKeyState(Keys.A, true);

          bool result = _rootScreen.ProcessKeyboard(keyboard);

          Assert.That(result, Is.False);
        }
        
  }
}

*/