public class SpawnScript
{
  private int[][] _spawnScript;
  public SpawnScript(int[][] spawnScript, int difficulty = 0)
  {
    _spawnScript = spawnScript;
  }
  public SpawnScript()
  {
    _spawnScript = new int[][]{
      new int[]{1, 10, 5},
      new int[]{3, 0, 0},
      new int[]{0, 1, 0},
      new int[]{0, 3, 0},
      new int[]{0, 6, 0},
      new int[]{0, 0, 1},
      new int[]{0, 0, 3},
    };
  }

  public int[][] GetScript()
  {
    return _spawnScript;
  }
}