﻿using System;
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
public class Map : IMap
{
  //public IReadOnlyList<IGameObject> GameObjects => _mapObjects.AsReadOnly();
  public IScreenSurface SurfaceObject => _mapSurface;
  public Player UserControlledObject { get; private set; }
  private List<IGameObject> _mapObjects;
  private IScreenSurface _mapSurface;

  private ISpawnOrchestrator _spawnLogic;
  private bool _spawnLogicSet = false;

  public int Width { get; private set; }
  public int Height { get; private set; }

  private IScreenObjectManager _screenObjectManager;
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

      //mapObject.RestoreMap(this);
    }
  }


  public void ProgressTime()
  {
    for (int i = 0; i < _mapObjects.Count; i++)
    {
      _mapObjects[i].Update();
    }

    if (_mapObjects.Count(obj => obj is Monster) < 1 && _spawnLogicSet)
    {
      _spawnLogic.NoEnemiesLeft();
    }
  }

  public void DropLoot(Point position)
  {
    Random rnd = new Random();
    int item = rnd.Next(0, 2);

    IGameObject? loot = null;
    switch (item)
    {
      case 0:
        loot = new Potion(position, _screenObjectManager, this);
        break;
      case 1:
        loot = new RangeBonus(position, _screenObjectManager, this);
        break;
    }
    _mapObjects.Add(loot);

  }

}