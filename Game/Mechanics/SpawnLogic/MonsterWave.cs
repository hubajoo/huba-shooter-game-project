using System.Collections.Generic;
using DungeonCrawl.Maps;
using DungeonCrawl.GameObjects;
using SadConsole;
using SadRogue.Primitives;

namespace DungeonCrawl.Mechanics;

/// <summary>
/// Class <c>MonsterWave</c> models a wave of monsters in the game.
/// </summary>
public class MonsterWave : ISpawnOrchestrator
{

  /// <summary>
  /// List of monsters in the wave.
  /// </summary>
  public List<IGameObject> Monsters = new List<IGameObject>();

  /// <summary>
  /// Screen object manager.
  /// </summary>
  private ScreenObjectManager _screenObjectManager;

  /// <summary>
  ///  Map object.
  /// </summary>
  private Map _map;

  /// <summary>
  /// Monster types.
  /// </summary>
  private MonsterTypes _monsterTypes;

  /// <summary>
  /// Spawn script.
  /// </summary>
  private int[][] _spawnScript;

  /// <summary>
  /// Difficulty of the wave, iterates the spawn script and defines the monster count at random waves.
  /// </summary>
  private int _difficulty;

  /// <summary>
  /// Constructor.
  /// </summary>
  /// <param name="map"></param>
  /// <param name="screenObjectManager"></param>
  /// <param name="monsterTypes"></param>
  /// <param name="spawnScript"></param>
  /// <param name="difficulty"></param>
  public MonsterWave(Map map, ScreenObjectManager screenObjectManager, MonsterTypes monsterTypes, SpawnScript spawnScript, int difficulty = 0)
  {
    _map = map;
    _screenObjectManager = screenObjectManager;
    _difficulty = difficulty;
    _monsterTypes = monsterTypes;
    _spawnScript = spawnScript.GetScript();
  }

  /// <summary>
  /// Method <c>NoEnemiesLeft</c> spawns a new wave of monsters when there are no enemies left.
  /// </summary>
  public void NoEnemiesLeft()
  {
    Point spawnPosition = openPortal(_difficulty);
    if (_difficulty < _spawnScript.Length)
    {
      scriptedSpawn(spawnPosition, _spawnScript[_difficulty]);
    }
    else
    {
      randomSpawn(spawnPosition);
    }

    _difficulty++;
  }

  /// <summary>
  /// Opens a portal for the monsters to spawn.
  /// </summary>
  /// <param name="difficulty"></param>
  /// <returns></returns>
  private Point openPortal(int difficulty)
  {
    Point location = new Point(0, 0);

    if (difficulty != 0)
    {
      location = new Point(Game.Instance.Random.Next(0, _map.Width),
          Game.Instance.Random.Next(0, _map.Height));
    }
    Monsters.Add(new Portal(location, _screenObjectManager, _map));
    return location;
  }

  /// <summary>
  /// Spawns a monster.
  /// </summary>
  /// <param name="code"></param>
  /// <param name="position"></param>
  private void spawn(int code, Point position)
  {
    var spawn = _monsterTypes.GetTypes()[code];
    Monsters.Add(spawn(position, _screenObjectManager, _map));
    addToMap();
    Monsters = new List<IGameObject>();
  }

  /// <summary>
  /// Spawns monsters according to a script.
  /// </summary>
  /// <param name="position"></param>
  /// <param name="waveCode"></param>
  private void scriptedSpawn(Point position, int[] waveCode)
  {
    for (int i = 0; i < waveCode.Length; i++)
    {
      for (int j = 0; j < waveCode[i]; j++)
      {
        spawn(i, position);
      }
    }
  }

  /// <summary>
  /// Spawns random monsters equal to the difficulty.
  /// </summary>
  /// <param name="position"></param>
  private void randomSpawn(Point position)
  {
    for (int i = 0; i < _difficulty; i++)
    {
      int monsterType = Game.Instance.Random.Next(0, _monsterTypes.GetTypes().Count);
      spawn(monsterType, position);
    }
  }

  /// <summary>
  /// Adds the monsters to the map.
  /// </summary>
  private void addToMap()
  {
    foreach (IGameObject monster in Monsters)
    {
      _map.AddMapObject(monster);
    }
  }
}