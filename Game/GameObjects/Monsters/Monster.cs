using DungeonCrawl.GameObjects.ObjectInterfaces;
using DungeonCrawl.Mechanics;
using DungeonCrawl.Mechanics.Randomisation;
using SadConsole;
using SadRogue.Primitives;


namespace DungeonCrawl.GameObjects.Monsters;
/// <summary>
/// Class <c>Monster</c> models a hostile object in the game.
/// </summary>
public class Monster : GameObject, IDamaging, IMoving, IVulnerable
{
  public int Health { get; set; } //Health of the monster
  public int DamageNumber { get; set; } //Damage of the monster

  protected int FixActionDelay { get;  set; } //Delay between actions
  protected int RandomActionDelayMax { get; set; } = 100; //Random delay between actions
  protected int InactiveTime { get; set; } //Time since last action

  protected readonly IDirectionChoiche DirectionChoice; //Direction choice for the monster

  /// <summary>
  /// Constructor for <c>Monster</c> initializes a new instance of the class.
  /// </summary>
  /// <param name="appearance"></param>
  /// <param name="position"></param>
  /// <param name="screenObjectManager"></param>
  /// <param name="health"></param>
  /// <param name="damage"></param>
  /// <param name="map"></param>
  /// <param name="directionChoice"></param>
  protected Monster(ColoredGlyph appearance, Point position, IScreenObjectManager screenObjectManager, int health, int damage, IMap map, IDirectionChoiche directionChoice = null)
      : base(appearance, position, screenObjectManager, map)
  {
    FixActionDelay = 0;
    Health = health;
    DamageNumber = damage;
    DirectionChoice = directionChoice ?? new RandomDirection();
  }

  /// <summary>
  /// Method <c>TakeDamage</c> reduces the health of the monster by a given amount.
  /// </summary>
  /// <param name="damage"></param>
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
    if (source is IDamaging)
    {
      var s = source as IDamaging;
      source.Touching(this);
      TakeDamage(s.GetDamage());
    }
    source.Touching(this);
    return false;
  }

  /// <summary>
  /// Method <c>Update</c> updates the monster's behaviour.
  /// </summary>
  public override void Update()
  {
    AiMove(_map); //Triggers the movement behaviour
    AiAttack(_map); //Triggers the attack behaviour
  }

  /// <summary>
  ///  Method <c>AIMove</c> moves the monster in a random direction with a delay.
  /// </summary>
  /// <param name="map"></param>
  protected virtual void AiMove(IMap map) //Default movement is random with a delay
  {
    if (InactiveTime >= FixActionDelay) //If the monster is not inactive
    {
      var direction = DirectionChoice.GetDirection(Position, map.UserControlledObject.Position); // Get the direction of the player
      Move(Position + direction, map); //Move in the direction
      InactiveTime = 0; //Reset the inactive time
      InactiveTime -= RandomAction.RandomWait(RandomActionDelayMax); //Add a random wait
    }
    else
    {
      InactiveTime++; //Wait
    }
  }

  /// <summary>
  /// Method <c>AIAttack</c> triggers the attack behaviour of the monster.
  /// </summary>
  /// <param name="map"></param>
  protected virtual void AiAttack(IMap map) //No default attack
  {

  }
}