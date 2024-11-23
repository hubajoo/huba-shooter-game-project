namespace DungeonCrawl.LeaderBoard;

/// <summary>
/// Record <c>LeaderBoardEntry</c> represents a leaderboard entry.
/// </summary>
/// <param name="Name"></param>
/// <param name="Score"></param>
public record LeaderBoardEntry(string Name, int Score);