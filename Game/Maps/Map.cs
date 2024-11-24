using System;
using SadConsole;
using SadRogue.Primitives;
using System.Collections.Generic;
using System.Linq;
using DungeonCrawl.GameObjects;
using DungeonCrawl.GameObjects.Items;
using DungeonCrawl.GameObjects.Monsters;
using DungeonCrawl.Mechanics.SpawnLogic;
using DungeonCrawl.GameObjects.ObjectInterfaces;


namespace DungeonCrawl.Maps;

/// <summary>
/// Class <c>Map</c> models a map for the game.
/// </summary>
public class Map : IMap
{
  //public IReadOnlyList<IGameObject> GameObjects => _mapObjects.AsReadOnly();
  public IScreenSurface SurfaceObject => _mapSurface;
  public Player UserControlledObject { get; private set; }
  private readonly List<IGameObject> _mapObjects;
  private readonly IScreenSurface _mapSurface;
  private readonly IScreenObjectManager _screenObjectManager;
  private ISpawnOrchestrator _spawnLogic;

  private bool _spawnLogicSet = false;
  public int Width { get; private set; }
  public int Height { get; private set; }


  /// <summary>
  /// Constructor.
  /// </summary>
  /// <param name="mapWidth"></param>
  /// <param name="mapHeight"></param>
  public Map(IScreenSurface mapSurface, IScreenObjectManager screenObjectManager)
  {
    _mapObjects = new List<IGameObject>();
    _mapSurface = mapSurface;
    Width = _mapSurface.Surface.Width;
    Height = _mapSurface.Surface.Height;
    _screenObjectManager = screenObjectManager;
  }

  /// <summary>
  /// Adds a player to the map.
  /// </summary>
  /// <param name="player"></param>
  public void AddUserControlledObject(Player player)
  {
    UserControlledObject = player;
    _mapObjects.Add(player);
  }
  /// <summary>
  /// Adds a map object to the map.
  /// </summary>
  /// <param name="gameObject"></param>
  public void AddMapObject(IGameObject gameObject)
  {
    _mapObjects.Add(gameObject);
  }

  /// <summary>
  /// Sets the spawn logic for the map.
  /// </summary>
  /// <param name="spawnLogic"></param>
  public void SetSpawnLogic(ISpawnOrchestrator spawnLogic)
  {
    _spawnLogicSet = true;
    _spawnLogic = spawnLogic;
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
        foundGameObject = gameObject;
      }
    }

    return foundGameObject;
  }
  /// <summary>
  /// Try to find a map object at that position.
  /// </summary>
  /// <param name="position"></param>
  /// <param name="gameObject"></param>
  /// <returns></returns>
  public bool TryGetMapObject(Point position, out IGameObject? gameObject)
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

  /// <summary>
  /// Try to find a map object at that position.
  /// </summary>
  /// <param name="position"></param>
  /// <param name="gameObject"></param>
  /// <param name="excluded"></param>
  /// <returns></returns>
  public bool TryGetMapObject(Point position, out IGameObject? gameObject, IGameObject excluded)
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
  public void RemoveMapObject(IGameObject mapObject)
  {
    if (_mapObjects.Contains(mapObject))
    {
      _mapObjects.Remove(mapObject);
    }
  }


  /// <summary>
  /// Progress time in the map.
  /// </summary>
  public void ProgressTime()
  {

    // Update all objects
    for (int i = 0; i < _mapObjects.Count; i++)
    {
      _mapObjects[i].Update();
    }

    // If there are no monsters left, call the spawn logic
    if (_mapObjects.Count(obj => obj is Monster) < 1 && _spawnLogicSet)
    {
      _spawnLogic.NoEnemiesLeft();
    }
  }

  /// <summary>
  /// Drop loot at a position.
  /// </summary>
  /// <param name="position"></param>
  public void DropLoot(Point position)
  {
    // Randomly choose a loot item
    Random rnd = new Random();
    int item = rnd.Next(0, 2);

    IGameObject? loot = null;
    switch (item)
    {
      case 0:
        loot = new Potion(position, _screenObjectManager, this); // Create a potion
        break;
      case 1:
        loot = new RangeBonus(position, _screenObjectManager, this); // Create a range bonus
        break;
    }
    _mapObjects.Add(loot);
  }
}