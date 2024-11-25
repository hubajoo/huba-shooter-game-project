using System;
using System.Collections.Generic;
using DungeonCrawl.GameObjects.ObjectInterfaces;
using SadRogue.Primitives;

namespace DungeonCrawl.Mechanics.SpawnLogic;
public interface IMonsterTypes
{
  public List<Func<Point, IScreenObjectManager, IMap, IGameObject>> GetTypes();
}