using SadRogue.Primitives;

namespace DungeonCrawl.GameObjects.ObjectInterfaces;


public interface IShooting
{
  public void Shoot();

  public bool CreateProjectile(Point origin, Direction direction, Color color, int damage = 1, int maxDistance = 1, int glyph = 4);
}