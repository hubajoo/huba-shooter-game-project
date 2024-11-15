using System.Collections.Generic;
using DungeonCrawl.Maps;
using DungeonCrawl.GameObjects;
using SadConsole;
using SadRogue.Primitives;

namespace DungeonCrawl.Mechanics;

public class Wave
{
  public List<GameObject> Monsters = new List<GameObject>();
  private Point _spawn = new Point(0, 0);
  private ScreenObjectManager _screenObjectManager;
  private Map _map;
  public Wave(Map map, ScreenObjectManager screenObjectManager, int difficulty)
  {
    _map = map;
    _screenObjectManager = screenObjectManager;
    Point randomPosition = OpenPortal(map, difficulty, _screenObjectManager);
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
        gCount = 10;
        dCount = 10;
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

      GameObject monster1 = new Orc(position, screenObjectManager);
      Monsters.Add(monster1);
    }
    for (int i = 0; i < gCount; i++)
    {

      GameObject monster1 = new Goblin(position, _screenObjectManager);
      Monsters.Add(monster1);
    }
    for (int i = 0; i < dCount; i++)
    {

      GameObject monster1 = new Dragon(position, _screenObjectManager);
      Monsters.Add(monster1);
    }

  }

  private Point OpenPortal(Map map, int difficulty, ScreenObjectManager screenObjectManager)
  {
    Point location = new Point(0, 0);

    if (difficulty != 0)
    {
      location = new Point(Game.Instance.Random.Next(0, _map.Width),
          Game.Instance.Random.Next(0, _map.Height));
    }
    Monsters.Add(new Portal(location, _screenObjectManager));
    return location;
  }
}