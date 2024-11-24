using NUnit.Framework;
using SadConsole;
using SadRogue;
using SadRogue.Primitives;
using DungeonCrawl.UI;
using DungeonCrawl.GameObjects;
using Moq;

namespace Tests.ScreenObjectManagerTests
{
  public class ScreenObjectManagerTests
  {
    private ScreenSurface _screenSurface;
    private IScreenObjectManager _screenObjectManager;
    private ColoredGlyph _appearance;
    private Mock<IGameObject> _gameObject;

    [SetUp]
    public void Setup()
    {
      //_screenSurface = new ScreenSurface(10, 10);
      _screenObjectManager = new ScreenObjectManager(_screenSurface);
      _gameObject = new Mock<IGameObject>();
      _appearance = new ColoredGlyph(Color.White, Color.Black, 'X');
      _gameObject.Setup(m => m.Appearance).Returns(_appearance);
      _gameObject.Setup(m => m.Position).Returns(new SadRogue.Primitives.Point(1, 1));
    }

    [TearDown]
    public void TearDown()
    {
      //_screenSurface.Dispose();
    }


    [Test]
    public void DrawGameObject_DrawsGameObject()
    {
      // _screenObjectManager.DrawScreenObject(_gameObject);
      // Assert.That(_screenSurface.Surface[1, 1], Is.EqualTo(_appearance));
      Assert.Fail("Forced failure for testing purposes.");
    }

  }}
    /*
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
            */
