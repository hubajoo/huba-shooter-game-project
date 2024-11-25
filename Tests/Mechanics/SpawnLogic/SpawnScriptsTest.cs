using DungeonCrawl.Mechanics.SpawnLogic;
using NUnit.Framework;

namespace Tests.Mechanics.SpawnLogic
{
  [TestFixture]
  public class SpawnScriptTests
  {
    [Test]
    public void Constructor_WithCustomScript_InitializesCorrectly()
    {
      int[][] customScript = new int[][]
      {
                new int[] { 1, 2, 3 },
                new int[] { 4, 5, 6 },
                new int[] { 7, 8, 9 }
      };

      var spawnScript = new SpawnScript(customScript);

      int[][] result = spawnScript.GetScript();

      Assert.AreEqual(customScript, result);
    }

    [Test]
    public void Constructor_WithoutParameters_InitializesDefaultScript()
    {
      var spawnScript = new SpawnScript();

      int[][] expectedScript = new int[][]
      {
                new int[] { 1, 0, 0 },
                new int[] { 3, 0, 0 },
                new int[] { 0, 1, 0 },
                new int[] { 0, 3, 0 },
                new int[] { 0, 6, 0 },
                new int[] { 0, 0, 1 },
                new int[] { 0, 0, 3 }
      };

      int[][] result = spawnScript.GetScript();

      Assert.AreEqual(expectedScript, result);
    }

    [Test]
    public void GetScript_ReturnsCorrectScript()
    {
      int[][] customScript = new int[][]
      {
                new int[] { 1, 2, 3 },
                new int[] { 4, 5, 6 },
                new int[] { 7, 8, 9 }
      };

      var spawnScript = new SpawnScript(customScript);

      int[][] result = spawnScript.GetScript();

      Assert.AreEqual(customScript, result);
    }
  }
}