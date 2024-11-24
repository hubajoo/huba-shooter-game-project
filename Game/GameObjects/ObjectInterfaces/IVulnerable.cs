namespace DungeonCrawl.GameObjects.ObjectInterfaces;

/// <summary>
/// Interface <c>IVulnerable</c> models the vulnerability of a game object.
/// </summary>
public interface IVulnerable
{
  /// <summary> 
  /// Takes damage. 
  /// </summary>
  public void TakeDamage(int damage);
}