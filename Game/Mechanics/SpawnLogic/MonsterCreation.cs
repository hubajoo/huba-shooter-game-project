using System;
using DungeonCrawl.GameObjects;
using DungeonCrawl.Mechanics;
using SadRogue.Primitives;

public static class MonsterCreation
{
  public static Func<Point, IScreenObjectManager, IMap, IGameObject>[] CreateMonsterTypes()
  {
    Func<Point, IScreenObjectManager, IMap, IGameObject> Orc = (Point a, IScreenObjectManager b, IMap c) => new Orc(a, b, c, new AggressiveDirection());
    Func<Point, IScreenObjectManager, IMap, IGameObject> Goblin = (Point a, IScreenObjectManager b, IMap c) => new Goblin(a, b, c, new AggressiveDirection());
    Func<Point, IScreenObjectManager, IMap, IGameObject> Dragon = (Point a, IScreenObjectManager b, IMap c) => new Dragon(a, b, c, new AggressiveDirection());
    return new Func<Point, IScreenObjectManager, IMap, IGameObject>[] { Orc, Goblin, Dragon };
  }
}