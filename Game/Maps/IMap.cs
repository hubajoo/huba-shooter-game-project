using DungeonCrawl.GameObjects;
using SadConsole;
using SadRogue.Primitives;

public interface IMap
{
  public int Width { get; }
  public int Height { get; }
  public IScreenSurface SurfaceObject { get; }
  public Player UserControlledObject { get; }
  public void AddUserControlledObject(Player player);
  public void AddMapObject(IGameObject gameObject);
  public void SetSpawnLogic(ISpawnOrchestrator spawnLogic);
  public IGameObject GetGameObject(Point position);
  public bool TryGetMapObject(Point position, out IGameObject gameObject);
  public bool TryGetMapObject(Point position, out IGameObject gameObject, IGameObject excluded);
  public void RemoveMapObject(IGameObject gameObject);
  public void ProgressTime();
  public void DropLoot(Point position);

}