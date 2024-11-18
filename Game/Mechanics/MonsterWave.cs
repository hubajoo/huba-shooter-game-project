using System.Collections.Generic;
using DungeonCrawl.Maps;
using DungeonCrawl.GameObjects;
using SadConsole;
using SadRogue.Primitives;

namespace DungeonCrawl.Mechanics;

public class MonsterWave
{
  public List<GameObject> Monsters = new List<GameObject>();
  private ScreenObjectManager _screenObjectManager;
  private Map _map;
  public MonsterWave(Map map, ScreenObjectManager screenObjectManager, int difficulty = 0)
  {
    _map = map;
    _screenObjectManager = screenObjectManager;
    Point randomPosition = OpenPortal(difficulty);
    randomPosition += DirectionGeneration.AggressiveDirection(randomPosition,
        map.UserControlledObject.Position);
    AddMonsters(randomPosition, difficulty, _screenObjectManager);
  }

  private void AddMonsters(Point position, int difficulty, ScreenObjectManager screenObjectManager)
  {
    int oCount = 0;
    int gCount = 0;
    int dCount = 0;

    switch (difficulty)
    {
      case 0:
        oCount = 1;
        gCount = 0;
        dCount = 0;
        break;
      case 1:
        oCount = 3;
        break;
      case 2:
        gCount = 1;
        break;
      case 3:
        gCount = 3;
        break;
      case 4:
        gCount = 6;
        break;
      case 5:
        dCount = 1;
        break;
      case 6:
        dCount = 3;
        break;
      default:
        int max = difficulty;
        gCount = Game.Instance.Random.Next(0, difficulty);
        max -= gCount;
        dCount = Game.Instance.Random.Next(0, difficulty);
        max -= dCount;
        oCount = Game.Instance.Random.Next(0, difficulty);
        break;
    }

    for (int i = 0; i < oCount; i++)
    {

      GameObject monster1 = new Orc(position, screenObjectManager, _map);
      Monsters.Add(monster1);
    }
    for (int i = 0; i < gCount; i++)
    {

      GameObject monster1 = new Goblin(position, _screenObjectManager, _map);
      Monsters.Add(monster1);
    }
    for (int i = 0; i < dCount; i++)
    {

      GameObject monster1 = new Dragon(position, _screenObjectManager, _map);
      Monsters.Add(monster1);
    }

    foreach (IGameObject monster in Monsters)
    {
      _map.AddMapObject(monster);
    }

  }

  private Point OpenPortal(int difficulty)
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
}