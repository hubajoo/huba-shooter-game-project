using NUnit.Framework;
using SadConsole;
using DungeonCrawl.UI;
/*
namespace Tests
{
  public class ScreenObjectManagerTests
  {
    private IScreenObjectManager _screenObjectManager;
    private IScreenObject _screenObject;

    private ScreenSurface _screenSurface;

    [SetUp]
    public void Setup()
    {
      _screenSurface = new ScreenSurface(10, 10);
      _screenObjectManager = new ScreenObjectManager(_screenSurface);
      _screenObject = new ScreenObject();
    }
    
    [TearDown]
    public void TearDown()
    {
      _screenSurface.Dispose();
      _screenObjectManager = null;
      _screenObject = null;
    }

    /*
        [Test]
        public void AddScreenObject_AddsObject()
        {
          _screenObjectManager.AddScreenObject(_screenObject);

          Assert.That(_screenObjectManager.ContainsScreenObject(_screenObject), Is.True);
          Assert.That(_screenObjectManager.ScreenObjectCount, Is.EqualTo(1));
        }

        [Test]
        public void RemoveScreenObject_RemovesObject()
        {
          _screenObjectManager.AddScreenObject(_screenObject);
          _screenObjectManager.RemoveScreenObject(_screenObject);

          Assert.That(_screenObjectManager.ContainsScreenObject(_screenObject), Is.False);
          Assert.That(_screenObjectManager.ScreenObjectCount, Is.EqualTo(0));
        }

        [Test]
        public void ContainsScreenObject_ReturnsTrueIfExists()
        {
          _screenObjectManager.AddScreenObject(_screenObject);

          Assert.That(_screenObjectManager.ContainsScreenObject(_screenObject), Is.True);
        }

        [Test]
        public void ContainsScreenObject_ReturnsFalseIfNotExists()
        {
          Assert.That(_screenObjectManager.ContainsScreenObject(_screenObject), Is.False);
        }

        [Test]
        public void ScreenObjectCount_ReturnsCorrectCount()
        {
          Assert.That(_screenObjectManager.ScreenObjectCount, Is.EqualTo(0));

          _screenObjectManager.AddScreenObject(_screenObject);
          Assert.That(_screenObjectManager.ScreenObjectCount, Is.EqualTo(1));

          _screenObjectManager.RemoveScreenObject(_screenObject);
          Assert.That(_screenObjectManager.ScreenObjectCount, Is.EqualTo(0));
        }
        *//*
  }
}
*/