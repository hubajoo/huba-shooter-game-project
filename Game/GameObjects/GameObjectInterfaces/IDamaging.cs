using System.Data;
using DungeonCrawl.GameObjects;

public interface IDamaging
{
  public int GetDamage();
  public void Touching();
}