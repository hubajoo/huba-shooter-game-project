using System.Data;
using DungeonCrawl.GameObjects;
/// <summary>
/// Interface <c>IDamaging</c> defines the damage logic for a game object.
/// </summary>
public interface IDamaging
{
  /// <summary> Gets the damage. </summary>
  public int GetDamage();
  /// <summary>
  /// Calls contact logic for the game object.
  /// </summary>
  public void Touching();
}