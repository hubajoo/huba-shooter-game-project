using SadConsole;
using SadRogue.Primitives;
using DungeonCrawl.GameObjects.ObjectInterfaces;

namespace DungeonCrawl.GameObjects;

/// <summary>
/// Projectile class is a GameObject that can move in a direction and damage other objects.
/// </summary>
public class Projectile : GameObject, IDamaging
{

  // _flownDistance is the distance the projectile has flown.
  private int _flownDistance = 0;

  // _maxDistance is the maximum distance the projectile can fly.
  private int _maxDistance;

  /// <summary>
  /// Projectile constructor.
  /// </summary>
  /// <param name="position"></param>
  /// <param name="direction"></param>
  /// <param name="screenObjectManager"></param>
  /// <param name="damage"></param>
  /// <param name="maxDistance"></param>
  /// <param name="color"></param>
  /// <param name="map"></param>
  /// <param name="glyph"></param>
  /// <returns></returns>
  public Projectile(Point position, Direction direction, IScreenObjectManager screenObjectManager, int damage, int maxDistance, Color color, IMap map, int glyph = 4)
      : base(new ColoredGlyph(color, Color.Transparent, glyph), position, screenObjectManager, map)
  {
    _maxDistance = maxDistance;
    Damage = damage;
    Direction = direction;
  }

  /// <summary>
  /// Touched method changes the stops and explodes the projectile when it touches another object.
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public override bool Touched(IGameObject source)
  {
#nullable enable
    Appearance = new ColoredGlyph(OriginalAppearance.Foreground, Appearance.Background, 15); // Change the appearance of the projectile - it explodes
    ScreenObjectManager.RefreshCell(_map, Position); // Refresh the cell on the map
    _maxDistance = 1; // Set the maximum distance to 1 - the projectile will be removed after the update
    Direction = Direction.None; // Stop the projectile
    return true;
  }


  /// <summary>
  /// Update method updates the projectile.
  /// </summary>
  public override void Update()
  {
    Fly();
  }

  /// <summary>
  /// Fly method moves the projectile in the direction it is facing.
  /// </summary>
  public void Fly()
  {
    // If the projectile has not flown the maximum distance
    if (_flownDistance < _maxDistance)
    {
      // Move the projectile in the direction it is facing, if it can't move, call the Touched method.
      _flownDistance++; // Increase the flown distance

      // Move the projectile in the direction it is facing
      if (!Move(Position + Direction, _map)) Touched(null); // If the projectile can't move, call the Touched method

      // Increase the flown distance if the projectile is moving up or down
      // Tiles are rectangular, visually the projectile moves 2 times faster up and down
      if (Direction == Direction.Up || Direction == Direction.Down) _flownDistance += 2;
    }
    else
    {
      RemoveSelf(); // Remove the projectile from the map
    }

  }

  /// <summary>
  /// Touching method changes the appearance of the projectile when it touches another object.
  /// </summary>
  public override void Touching(IGameObject source)
  {
    Appearance = new ColoredGlyph(OriginalAppearance.Foreground, Appearance.Background, 15);
    ScreenObjectManager.RefreshCell(_map, Position);
    Direction = Direction.None;
  }

  /// <summary>
  /// GetDamage method returns the damage of the projectile.
  /// </summary>
  public int GetDamage()
  {
    return Damage;
  }
}