using System;
using SadConsole;
using SadRogue.Primitives;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DungeonCrawl.Mechanics;
using DungeonCrawl.UI;
using SadConsole.UI;
using DungeonCrawl.GameObjects;

namespace DungeonCrawl.Maps;

/// <summary>
/// Class <c>Map</c> models a map for the game.
/// </summary>
public class Map
{
  public IReadOnlyList<IGameObject> GameObjects => _mapObjects.AsReadOnly();
  public ScreenSurface SurfaceObject => _mapSurface;
  public Player UserControlledObject { get; set; }
  private List<IGameObject> _mapObjects;
  private ScreenSurface _mapSurface;
  private int _difficulty;

  public int Width;
  public int Height;

  private ScreenObjectManager _screenObjectManager;
  /// <summary>
  /// Constructor.
  /// </summary>
  /// <param name="mapWidth"></param>
  /// <param name="mapHeight"></param>
  public Map(ScreenSurface mapSurface, ScreenObjectManager screenObjectManager)
  {
    _mapObjects = new List<IGameObject>();
    _mapSurface = mapSurface;
    Width = _mapSurface.Surface.Width;
    Height = _mapSurface.Surface.Height;
    _screenObjectManager = screenObjectManager;
  }


  public void AddUserControlledObject(Player player)
  {
    UserControlledObject = player;
    _mapObjects.Add(player);
  }

  public void AddMapObject(IGameObject gameObject)
  {
    _mapObjects.Add(gameObject);
  }

  /// <summary>
  /// Try to find a map object at that position.
  /// </summary>
  /// <param name="position"></param>
  /// <param name="gameObject"></param>
  /// <returns></returns>
  public IGameObject GetGameObject(Point position)
  {
    #nullable enable
    IGameObject? foundGameObject = null;
    foreach (var gameObject in _mapObjects)
    {
      if (gameObject.GetPosition() == position)
      {
        foundGameObject =  gameObject;
      }
    }

    return foundGameObject;
    /*
    if (UserControlledObject.Position == position)
    {
      gameObject = UserControlledObject;
      return true;
    }
    gameObject = null;
    return false;
    */
  }
  public bool TryGetMapObject(Point position, out IGameObject gameObject)
  {
    foreach (var otherGameObject in _mapObjects)
    {
      if (otherGameObject.GetPosition() == position)
      {
        gameObject = otherGameObject;
        return true;
      }
    }
    if (UserControlledObject.Position == position)
    {
      gameObject = UserControlledObject;
      return true;
    }
    gameObject = null;
    return false;
  }
  public bool TryGetMapObject(Point position, out IGameObject gameObject, IGameObject excluded)
  {
    foreach (var otherGameObject in _mapObjects)
    {
      if (otherGameObject.GetPosition() == position && otherGameObject != excluded)
      {
        gameObject = otherGameObject;
        return true;
      }
    }
    if (UserControlledObject.Position == position && excluded is not Player)
    {
      gameObject = UserControlledObject;
      return true;
    }
    gameObject = null;
    return false;
  }

  /// <summary>
  /// Removes an object from the map.
  /// </summary>
  /// <param name="mapObject">Object to be removed</param>
  public void RemoveMapObject(GameObject mapObject)
  {
    if (_mapObjects.Contains(mapObject))
    {
      _mapObjects.Remove(mapObject);

      //mapObject.RestoreMap(this);
    }
  }


  /// <summary>
  /// Opens a portal and unleashes enemies.
  /// </summary>
  public void CreateMonsterWave()
  {
    MonsterWave wave = new MonsterWave(this, _screenObjectManager, _difficulty);
    foreach (var monster in wave.Monsters)
    {
      _mapObjects.Add(monster);
    }

    _difficulty++;
  }


  /// <summary>
  /// Creartes a custom projectile going in the required direction.
  /// </summary>
  /// <param name="attackerPosition">Origin position</param>
  /// <param name="direction"Direction of flight</param>
  /// <param name="color" Projectile color</param>
  public bool CreateProjectile(Point origin, Direction direction, Color color, int damage = 1, int maxDistance = 1, int glyph = 4)
  {
    Point spawnPosition = origin + direction;
    if (!this.SurfaceObject.Surface.IsValidCell(spawnPosition.X, spawnPosition.Y)) return false;
    if (this.TryGetMapObject(spawnPosition, out IGameObject foundObject))
    {
      return false;
    }

    Projectile hitbox = new Projectile(
        spawnPosition, direction, _screenObjectManager, damage, maxDistance, color, this, glyph);
    _mapObjects.Add(hitbox);

    return true;
  }

  public void ProgressTime()
  {
    for (int i = 0; i < _mapObjects.Count; i++)
    {
      _mapObjects[i].Update();
    }

    if (_mapObjects.Count(obj => obj is Monster) < 1)
    {

    }
    //DrawGameObject(map.SurfaceObject)
  }

  public void DropLoot(Point position)
  {
    Random rnd = new Random();
    int item = rnd.Next(0, 2);
    //position += Movements.GetRandomDirection();
    /*
    GameObject loot = new Wall(position, _mapSurface);
    switch (item)
    {
      case 0:
        loot = new Potion(position, _mapSurface);
        break;
      case 1:
        loot = new RangeBonus(position, _mapSurface);
        break;
    }
    _mapObjects.Add(loot);
*/
  }
}