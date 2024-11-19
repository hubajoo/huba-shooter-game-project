using DungeonCrawl.Maps;
using DungeonCrawl.Mechanics;
using SadConsole;
using SadRogue.Primitives;


namespace DungeonCrawl.GameObjects;

/// <summary>
/// Class <c>Monster</c> models a hostile object in the game.
/// </summary>
public class Monster : GameObject, IDamaging, IMoving, IVulnerable
{
  public int Health { get; set; } //Health of the monster
  public int DamageNumber { get; set; } //Damage of the monster

  public int FixActionDelay { get; set; } //Delay between actions
  public int RandomActionDelayMax { get; set; } = 100; //Random delay between actions
  public int InactiveTime { get; set; } = 0; //Time since last action

  protected IDirectionChoiche _directionChoice; //Direction choice for the monster
  /// <summary>
  /// Constructor.
  /// </summary>
  /// <param name="appearance"></param>
  /// <param name="position"></param>
  /// <param name="hostingSurface"></param>
  protected Monster(ColoredGlyph appearance, Point position, IScreenObjectManager screenObjectManager, int health, int damage, IMap map, IDirectionChoiche directionChoice = null)
      : base(appearance, position, screenObjectManager, map)
  {
    FixActionDelay = 0;
    Health = health;
    DamageNumber = damage;
    _directionChoice = directionChoice ?? new RandomDirection();
  }
  /// <summary>
  /// Method <c>TakeDamage</c> reduces the health of the monster by a given amount.
  /// </summary>
  /// <param name="map"></param>
  /// <param name="damage"></param>
  /// <returns></returns>
  public void TakeDamage(int damage = 1)
  {
    Health -= damage;
    if (Health <= 0)
    {
      RemoveSelf(); //Removes the monster from the map
      _map.UserControlledObject.Killed(this); //Accredits the kill to the player
      _map.DropLoot(Position); //Drops loot
    }
  }
  /// <summary>
  /// Method <c>GetDamage</c> returns the damage of the monster.
  /// </summary>
  /// <returns></returns>
  public int GetDamage()
  {
    return DamageNumber;
  }


  /// <summary>
  /// Method <c>Touched</c> checks if the monster has been touched, triggers TakeDamage if when relevant.
  /// </summary>
  /// <param name="source"></param>
  /// <returns></returns>
  public override bool Touched(IGameObject source)
  {
    if (source is IDamaging damaging && !(source is Monster))
    {
      damaging.Touching(this);
      TakeDamage(damaging.GetDamage());
    }
    source.Touching(this);
    return false;
  }


  /// <summary>
  /// Method <c>Update</c> updates the monster's behaviour.
  /// </summary>
  public override void Update()
  {
    AIMove(_map); //Triggers the movement behaviour
    AIAttack(_map); //Triggers the attack behaviour
  }
  /// <summary>
  ///  Method <c>AIMove</c> moves the monster in a random direction with a delay.
  /// </summary>
  /// <param name="map"></param>
  protected virtual void AIMove(IMap map) //Default movement is random with a delay
  {
    if (InactiveTime >= FixActionDelay)
    {
      var direction = _directionChoice.GetDirection(Position, map.UserControlledObject.Position);
      this.Move(this.Position + direction, map);
      InactiveTime = 0;
      InactiveTime -= RandomAction.RandomWait(RandomActionDelayMax);
    }
    else
    {
      InactiveTime++;
    }
  }
  /// <summary>
  /// Method <c>AIAttack</c> triggers the attack behaviour of the monster.
  /// </summary>
  /// <param name="map"></param>
  protected virtual void AIAttack(IMap map) //No default attack
  {

  }
}