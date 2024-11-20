public interface ILeaderBoardHandler
{
  public string[] GetArray();
  public void AddToLeaderboard(string name, int score);
  public void ReadLeaderBoard();
  public void ClearLeaderBoard();
  public void SortLeaderBoard();
  public void FetchLeaderboard(string url, string name);
}