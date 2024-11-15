using System;
using DungeonCrawl.Tiles;
using SadConsole;
using SadRogue.Primitives;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DungeonCrawl.Mechanics;
using DungeonCrawl.UI;
using SadConsole.UI;

namespace DungeonCrawl.Maps;

/// <summary>
/// Class <c>Map</c> models a map for the game.
/// </summary>
public class Map
{
  public IReadOnlyList<GameObject> GameObjects => _mapObjects.AsReadOnly();
  public ScreenSurface SurfaceObject => _mapSurface;
  public Player UserControlledObject { get; private set; }
  private List<GameObject> _mapObjects;
  private ScreenSurface _mapSurface;
  private int _difficulty;
  /// <summary>
  /// Constructor.
  /// </summary>
  /// <param name="mapWidth"></param>
  /// <param name="mapHeight"></param>
  public Map(ScreenSurface mapSurface, Player player)
  {
    _mapObjects = new List<GameObject>();
    _mapSurface = mapSurface;
    UserControlledObject = player;

    CreateMonsterWave();
  }

  /// <summary>
  /// Try to find a map object at that position.
  /// </summary>
  /// <param name="position"></param>
  /// <param name="gameObject"></param>
  /// <returns></returns>
  public bool TryGetMapObject(Point position, out GameObject gameObject)
  {
    foreach (var otherGameObject in _mapObjects)
    {
      if (otherGameObject.Position == position)
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
  public bool TryGetMapObject(Point position, out GameObject gameObject, GameObject excluded)
  {
    foreach (var otherGameObject in _mapObjects)
    {
      if (otherGameObject.Position == position && otherGameObject != excluded)
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
  /// <param name="mapObject"></param>
  public void RemoveMapObject(GameObject mapObject)
  {
    if (_mapObjects.Contains(mapObject))
    {
      _mapObjects.Remove(mapObject);
      mapObject.RestoreMap(this);
    }
  }

  /// <summary>
  /// Creates a monster on the map.
  /// </summary>
  private void CreateMonster()
  {
    for (int i = 0; i < 1000; i++)
    {
      Point randomPosition1 = new Point(Game.Instance.Random.Next(0, _mapSurface.Surface.Width),
          Game.Instance.Random.Next(0, _mapSurface.Surface.Height));

      bool foundObject1 = _mapObjects.Any(obj => obj.Position == randomPosition1);
      if (foundObject1) continue;

      GameObject monster1 = new Goblin(randomPosition1, _mapSurface);
      _mapObjects.Add(monster1);

      Point randomPosition2 = new Point(Game.Instance.Random.Next(0, _mapSurface.Surface.Width),
          Game.Instance.Random.Next(0, _mapSurface.Surface.Height));

      bool foundObject2 = _mapObjects.Any(obj => obj.Position == randomPosition2);
      if (foundObject2) continue;

      GameObject monster2 = new Orc(randomPosition2, _mapSurface);
      _mapObjects.Add(monster2);

      Point randomPosition3 = new Point(Game.Instance.Random.Next(0, _mapSurface.Surface.Width),
          Game.Instance.Random.Next(0, _mapSurface.Surface.Height));

      bool foundObject3 = _mapObjects.Any(obj => obj.Position == randomPosition3);
      if (foundObject3) continue;

      GameObject monster3 = new Dragon(randomPosition3, _mapSurface);
      _mapObjects.Add(monster3);
      break;
    }
  }

  public void CreateMonsterWave()
  {
    Wave wave = new Wave(this, _mapSurface, _difficulty);
    foreach (var monster in wave.Monsters)
    {
      _mapObjects.Add(monster);
    }

    _difficulty++;

  }


  public bool CreateProjectile(Point attackerPosition, Direction direction, Color color, int damage = 1, int maxDistance = 1, int glyph = 4)
  {
    Point spawnPosition = attackerPosition + direction;
    if (!this.SurfaceObject.Surface.IsValidCell(spawnPosition.X, spawnPosition.Y)) return false;
    if (this.TryGetMapObject(spawnPosition, out GameObject foundObject))
    {
      return false;
    }
    Projectile hitbox = new Projectile(
        spawnPosition, direction, _mapSurface, damage, maxDistance, color, glyph);
    _mapObjects.Add(hitbox);
    return true;
  }

  public void ProgressTime()
  {
    for (int i = 0; i < _mapObjects.Count; i++)
    {
      _mapObjects[i].Update(this);
    }

    if (_mapObjects.Count(obj => obj is Monster) < 1)
    {
      CreateMonsterWave();
    }
    //DrawGameObject(map.SurfaceObject)
  }

  public void DropLoot(Point position)
  {
    Random rnd = new Random();
    int item = rnd.Next(0, 2);
    //position += Movements.GetRandomDirection();
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

  }
}