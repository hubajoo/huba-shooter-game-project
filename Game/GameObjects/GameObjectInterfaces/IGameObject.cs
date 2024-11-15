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
  public bool Touched(IGameObject source);

  public void Touching(IGameObject source);

  public void Update();

  public void RemoveSelf();

  public Point GetPosition();

  public ColoredGlyph GetAppearance();

}