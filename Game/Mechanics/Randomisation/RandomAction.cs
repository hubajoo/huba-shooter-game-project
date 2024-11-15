using System;
using SadRogue.Primitives;

namespace DungeonCrawl.Mechanics;

public class RandomAction
{
  public static int RandomWait(int max)
  {
    Random rnd = new Random();
    int randomDelay = rnd.Next(max);
    return randomDelay;
  }

  public static bool Bool()
  {
    Random rnd = new Random();
    int randomOrAggro = rnd.Next(0, 2);
    return randomOrAggro == 1;
  }
  public static bool weightedBool(int max)
  {
    Random rnd = new Random();
    int randomOrAggro = rnd.Next(0, max);
    return randomOrAggro != 0;
  }
}