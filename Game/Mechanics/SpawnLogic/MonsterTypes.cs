using System;
using System.Collections.Generic;
using DungeonCrawl.GameObjects;
using DungeonCrawl.Maps;
using SadRogue.Primitives;

public class MonsterTypes
{
  public List<Func<Point, IScreenObjectManager, IMap, IGameObject>> Types { get; private set; } = new List<Func<Point, IScreenObjectManager, IMap, IGameObject>>();
  public MonsterTypes(params Func<Point, IScreenObjectManager, IMap, IGameObject>[] monsterTypes)
  {
    foreach (var func in monsterTypes)
    {
      Types.Add(func);
    }
  }
  public List<Func<Point, IScreenObjectManager, IMap, IGameObject>> GetTypes()
  {
    return Types;
  }
}