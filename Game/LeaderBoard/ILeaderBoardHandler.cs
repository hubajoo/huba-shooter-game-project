using System.Collections.Generic;
using System.Threading.Tasks;

namespace DungeonCrawl.LeaderBoard;

/// <summary>
/// Interface <c>ILeaderBoardHandler</c> provides methods for handling the leaderboard.
/// </summary>
public interface ILeaderBoardHandler
{
  public string[] GetArray();
  public void AddToLeaderboard(string name, int score);
  public List<string> ReadLeaderBoard();
  public void ClearLeaderBoard();
  public void SortLeaderBoard();
  public Task FetchLeaderboard();
}