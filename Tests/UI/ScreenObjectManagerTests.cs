using NUnit.Framework;
using System;
using Moq;
using DungeonCrawl.Maps;
using DungeonCrawl.GameObjects.ObjectInterfaces;
using DungeonCrawl.UI;
using SadConsole;
using SadRogue.Primitives;
using DungeonCrawl.Mechanics.SpawnLogic;
using DungeonCrawl.LeaderBoard;


namespace Tests.ScreenObjectManagerTests
{
  public class ScreenObjectManagerTests
  {
    private IScreenObjectManager _screenObjectManager;
    private ColoredGlyph _appearance;
    private Mock<IGameObject> _gameObject;
    private Mock<IScreenSurface> _screenSurface;
    private Mock<IScreenObject> _screenObject;
    private Mock<IMap> _map;

    [SetUp]
    public void Setup()
    {
      _appearance = new ColoredGlyph(Color.White, Color.Black, 'X');

      _gameObject = new Mock<IGameObject>();
      _gameObject.Setup(m => m.Appearance).Returns(_appearance);
      _gameObject.Setup(m => m.GetAppearance()).Returns(_appearance);
      _gameObject.Setup(m => m.Position).Returns(new Point(1, 1));

      _screenSurface = new Mock<IScreenSurface>();
      _screenObjectManager = new ScreenObjectManager(_screenSurface.Object);

      _screenObject = new Mock<IScreenObject>();
      _map = new Mock<IMap>();
    }



    [Test]
    public void AddScreenObject_AddsObject()
    {
      _screenObjectManager.AddScreenObject(_screenObject.Object);

      Assert.That(_screenObjectManager.Screen.Children.Contains(_screenObject.Object), Is.True);
    }



    [Test]
    public void RemoveScreenObject_RemovesObject()
    {
      _screenObjectManager.AddScreenObject(_screenObject.Object);
      _screenObjectManager.RemoveScreenObject(_screenObject.Object);

      Assert.That(_screenObjectManager.Screen.Children.Contains(_screenObject.Object), Is.False);
    }

    [Test]
    public void RefreshCell_RefreshesCell()
    {
      var obj = _gameObject.Object;

      // Setup the mock to return the object at the given position
      _map.Setup(m => m.TryGetMapObject(new Point(1, 1), out obj)).Returns(true);

      // Define alternative appearance
      ColoredGlyph altAppearance = new ColoredGlyph(Color.Black, Color.White, 'Y');

      // Setup the mock to return the alternative appearance instead of the default appearance
      _screenSurface.Setup(s => s.Surface[new Point(1, 1)]).Returns(altAppearance);

      // Call the method
      _screenObjectManager.RefreshCell(_map.Object, new Point(1, 1));

      // Verify screen surface and map were accessed
      _screenSurface.Verify(s => s.Surface[new Point(1, 1)], Times.Once);
      _map.Verify(m => m.TryGetMapObject(new Point(1, 1), out obj), Times.Once);

      // Verify the alt appearance was overwritten
      Assert.That(altAppearance.Foreground, Is.EqualTo(_appearance.Foreground));
    }

    [Test]
    public void GetScreenObject_ReturnsObject()
    {
      _screenSurface.Setup(s => s.Surface[new Point(1, 1)]).Returns(_appearance);

      var obj = _gameObject.Object;

      _map.Setup(m => m.TryGetMapObject(new Point(1, 1), out obj)).Returns(true);

      var result = _screenObjectManager.GetScreenObject(new Point(1, 1));

      Assert.That(result, Is.EqualTo(_appearance));
    }

    [Test]
    public void DrawScreenObject_DrawsObject()
    {
      var obj = _gameObject.Object;

      // Define alternative appearance
      ColoredGlyph altAppearance = new ColoredGlyph(Color.Black, Color.White, 'Y');

      // Setup the mock to return the alternative appearance instead of the default appearance
      _screenSurface.Setup(s => s.Surface[new Point(1, 1)]).Returns(altAppearance);

      // Call the method
      _screenObjectManager.DrawScreenObject(_gameObject.Object, new Point(1, 1));

      // Verify screen surface and map were accessed
      _screenSurface.Verify(s => s.Surface[new Point(1, 1)], Times.Once);

      // Verify the appearance was updated
      Assert.That(_screenSurface.Object.Surface[new Point(1, 1)], Is.EqualTo(altAppearance));
    }

    [Test]
    public void ClearScreen_ClearsScreen()
    {
      _screenObjectManager.ClearScreen();

      Assert.That(_screenObjectManager.Screen.Children.Count, Is.EqualTo(0));
    }
  }
}

