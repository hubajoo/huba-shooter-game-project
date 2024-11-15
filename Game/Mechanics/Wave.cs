using System.Collections.Generic;
using DungeonCrawl.Maps;
using DungeonCrawl.Tiles;
using SadConsole;
using SadRogue.Primitives;

namespace DungeonCrawl.Mechanics;

public class Wave
{
    public List<GameObject> Monsters = new List<GameObject>();
    private Point spawn = new Point(0,0);
    private ScreenSurface MapSurface;
    public Wave(Map map, ScreenSurface _mapSurface, int difficulty)
    {
        MapSurface = _mapSurface;
        Point randomPosition =  OpenPortal(map, difficulty, _mapSurface);
        randomPosition += Movements.AggressiveDirection(randomPosition,
            map.UserControlledObject.Position);
        AddMonsters(randomPosition, difficulty, _mapSurface);
    }

    private void AddMonsters(Point position, int difficulty,ScreenSurface  _mapSurface)
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

            GameObject monster1 = new Orc(position, _mapSurface);
            Monsters.Add(monster1);
        }
        for (int i = 0; i < gCount; i++)
        {

            GameObject monster1 = new Goblin(position, _mapSurface);
            Monsters.Add(monster1);
        }
        for (int i = 0; i < dCount; i++)
        {

            GameObject monster1 = new Dragon(position, _mapSurface);
            Monsters.Add(monster1);
        }
       
    }

    private Point OpenPortal(Map map, int difficulty, ScreenSurface _mapSurface)
    { 
        Point location = new Point(0,0);
      if (difficulty != 0)
      {
          location = new Point(Game.Instance.Random.Next(0, _mapSurface.Surface.Width),
              Game.Instance.Random.Next(0, _mapSurface.Surface.Height));
      }
      Monsters.Add(new Portal(location, MapSurface));
      return location;
    }
}