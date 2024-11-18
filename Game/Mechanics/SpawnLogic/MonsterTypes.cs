using System;
using System.Collections.Generic;
using DungeonCrawl.GameObjects;
using DungeonCrawl.Maps;
using SadRogue.Primitives;

public class MonsterTypes
{
  public List<Func<Point, ScreenObjectManager, Map, IGameObject>> Types { get; private set; } = new List<Func<Point, ScreenObjectManager, Map, IGameObject>>();
  public MonsterTypes(params Func<Point, ScreenObjectManager, Map, IGameObject>[] monsterTypes)
  {
    foreach (var func in monsterTypes)
    {
      Types.Add(func);
    }
  }
  public List<Func<Point, ScreenObjectManager, Map, IGameObject>> GetTypes()
  {
    return Types;
  }
}