using DungeonCrawl.Maps;
using DungeonCrawl.Mechanics;
using SadConsole;
using SadRogue.Primitives;

namespace DungeonCrawl.GameObjects;

/// <summary>
/// The interface <I>IGameObject</I> models any objects in the game.
/// </summary>
public interface IGameObject
{
  /// <summary>
  /// Moves the object to the given position on the map.
  /// </summary>
  /// <param name="newPosition"></param>
  /// <param name="map"></param>
  /// <returns></returns>


  public bool Touched(IGameObject source, Map map);

  public void Touching(IGameObject source);

  public void Update(Map map);

}