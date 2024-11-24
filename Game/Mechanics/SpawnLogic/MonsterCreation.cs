using System;
using DungeonCrawl.GameObjects.ObjectInterfaces;
using DungeonCrawl.GameObjects.Monsters;
using SadRogue.Primitives;

namespace DungeonCrawl.Mechanics.SpawnLogic;
public static class MonsterCreation
{
  public static Func<Point, IScreenObjectManager, IMap, IGameObject>[] CreateMonsterTypes()
  {
    Func<Point, IScreenObjectManager, IMap, IGameObject> orc = ( a,  b, c) => new Orc(a, b, c, new AggressiveDirection());
    Func<Point, IScreenObjectManager, IMap, IGameObject> goblin = ( a,  b, c) => new Goblin(a, b, c, new AggressiveDirection());
    Func<Point, IScreenObjectManager, IMap, IGameObject> dragon = ( a,  b, c) => new Dragon(a, b, c, new AggressiveDirection());
    return new Func<Point, IScreenObjectManager, IMap, IGameObject>[] { orc, goblin, dragon };
  }
}