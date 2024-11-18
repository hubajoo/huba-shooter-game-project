﻿using DungeonCrawl.Maps;
using DungeonCrawl.Mechanics;
using SadConsole;
using SadRogue.Primitives;


namespace DungeonCrawl.GameObjects;

/// <summary>
/// Class <c>GameObject</c> models any objects in the game.
/// </summary>
public abstract class GameObject : IGameObject
{
  public Point Position { get; set; }
  public Direction Direction;
  public int Damage { get; protected set; } = 0;
  public int Range { get; set; }

  public ColoredGlyph Appearance { get; set; }
  protected ColoredGlyph OriginalAppearance { get; set; }
  public ColoredGlyph _mapAppearance = new ColoredGlyph();

  private ScreenObjectManager _screenObjectManager;

  protected Map _map;


  /// <summary>
  /// Constructor.
  /// </summary>
  /// <param name="appearance"></param>
  /// <param name="position"></param>
  /// <param name="hostingSurface"></param>
  protected GameObject(ColoredGlyph appearance, Point position, ScreenObjectManager screenObjectManager, Map map)
  {
    Appearance = appearance;
    OriginalAppearance = appearance;
    Position = position;
    _screenObjectManager = screenObjectManager;
    _map = map;


    // Store the map cell
    _mapAppearance = screenObjectManager.GetScreenObject(position);


    // Draw the object
    screenObjectManager.DrawScreenObject(this, position);
  }

  public void RestoreMap(Map map)
  {

    //_mapAppearance.CopyAppearanceTo(map.SurfaceObject.Surface[Position]);
  }

  /// <summary>
  /// Moves the object to the given position on the map.
  /// </summary>
  /// <param name="newPosition"></param>
  /// <param name="map"></param>
  /// <returns></returns>
  public bool Move(Point newPosition, Map map)
  {
    var m = new Movement(map, _screenObjectManager);
    return m.Move(this, map, newPosition);
  }

  /// <summary>
  /// Defines what should happen if another object touches the current one.
  /// </summary>
  /// <param name="source"></param>
  /// <param name="map"></param>
  /// <returns></returns>
  public virtual bool Touched(IGameObject source)
  {
    source.Touching(this);
    return false;
  }
  /// <summary>
  /// Defines what should happen if the current object touches another object.
  /// </summary>
  /// <param name="source"></param>
  public virtual void Touching(IGameObject source)
  {
  }
  /// <summary>
  /// Updates the object.
  /// </summary>
  public virtual void Update()
  {

  }
  /// <summary>
  /// Removes the object from the map.
  /// </summary>
  public virtual void RemoveSelf()
  {
    _map.RemoveMapObject(this);
  }
  /// <summary>
  /// Gets the position of the object.
  /// </summary>
  /// <returns>Point</returns>
  public Point GetPosition()
  {
    return Position;
  }
  /// <summary>
  /// Gets the appearance of the object.
  /// </summary>
  /// <returns></returns>
  public ColoredGlyph GetAppearance()
  {
    return Appearance;
  }
  /// <summary>
  /// Method <c>TakeDamage</c> reduces the health of the monster by a given amount.
  /// </summary>
  /// <param name="map"></param>
  /// <param name="source"></param>
  /// <param name="damage"></param>
  /// <returns></returns>

  public virtual bool TakeDamage(Map map, IGameObject source, int damage)
  {
    return false;
  }


  /// <summary>
  /// Draws the object on the screen.
  /// </summary>
  /// <param name="screenSurface"></param>
  /*
  protected void DrawGameObject(IScreenSurface screenSurface)
  {
    Appearance.CopyAppearanceTo(screenSurface.Surface[Position]);
    screenSurface.IsDirty = true;
  }  */

}