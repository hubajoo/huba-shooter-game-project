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
  /// Checks if the game object has been touched.
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public bool Touched(IGameObject source);
  /// <summary>
  /// Calls contact logic for the touchhed game object.
  /// </summary>
  /// <param name="source"></param>
  public void Touching(IGameObject source);
}