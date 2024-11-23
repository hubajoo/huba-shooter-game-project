using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using DungeonCrawl.GameSetup;

namespace DungeonCrawl.LeaderBoard;

/// <summary>
/// Class <c>LeaderBoardHandler</c> handles the leaderboard.
/// </summary>
public class LeaderBoardHandler : ILeaderBoardHandler
{
  private List<string> LeaderBoard;

  public string path = "Data/leaderBoard.txt";

  private string _url;
  public LeaderBoardHandler(IGameSettings settings)
  {
    _url = settings.ServerUrl;
    LeaderBoard = new List<string>();
  }

  public static async Task<LeaderBoardHandler> CreateAsync(IGameSettings settings)
  {
    var handler = new LeaderBoardHandler(settings);
    await handler.FetchLeaderboard();
    handler.ReadLeaderBoard();
    handler.SortLeaderBoard();
    return handler;
  }

  /// <summary>
  /// Method <c>FetchLeaderboard</c> fetches the leaderboard from the server.
  /// </summary>
  /// <param name="url"></param>
  /// <param name="name"></param>
  public async Task FetchLeaderboard()
  {
    try
    {
      string request = $"{_url}/api/leaderboard";
      Console.WriteLine($"Fetching leaderboard from: {request}");

      using (HttpClient client = new HttpClient())
      {
        HttpResponseMessage response = await client.GetAsync(request);
        if (response.IsSuccessStatusCode)
        {
          string content = await response.Content.ReadAsStringAsync();
          Console.WriteLine(content);
          ClearLeaderBoard();
          var leaderboardEntries = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LeaderBoardEntry>>(content);
          foreach (var entry in leaderboardEntries)
          {
            string line = $"{entry.Name}:{entry.Score}";
            LeaderBoard.Add(line);
            using StreamWriter sw = File.AppendText(path);
            sw.WriteLine($"\n{line}");
          }
          ReadLeaderBoard();
        }
        else
        {
          Console.WriteLine($"Error: {response.StatusCode}");
        }
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"An error occurred: {ex.Message}");
    }
  }

  /// <summary>
  /// Method <c>AddToLeaderboard</c> adds a player to the leaderboard.
  /// </summary>
  /// <param name="name"></param>
  /// <param name="score"></param>
  public void AddToLeaderboard(string name, int score)
  {
    try
    {
      LeaderBoard.Add($"{name}:{score}");
      using StreamWriter sw = File.AppendText(path);
      sw.WriteLine($"\n{name}:{score}");
      Console.WriteLine($"{name}:{score}");
      SortLeaderBoard();
      AddToLeaderboardServer(name, score);
    }
    catch (Exception e)
    {
      Console.WriteLine(e.Message);
    }

  }

  /// <summary>
  /// Method <c>AddToLeaderboardServer</c> adds a player to the leaderboard on the server.
  /// </summary>
  /// <param name="name"></param>
  /// <param name="score"></param>
  private async void AddToLeaderboardServer(string name, int score)
  {
    try
    {
      string requestUrl = $"{_url}/api/leaderboard";
      Console.WriteLine($"Sending PUT request to: {requestUrl}");

      var leaderboardEntry = new LeaderBoardEntry(name, score);
      string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(leaderboardEntry);
      var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
      Console.WriteLine(jsonContent);
      using (HttpClient client = new HttpClient())
      {
        HttpResponseMessage response = await client.PutAsync(requestUrl, content);
        if (response.IsSuccessStatusCode)
        {
          Console.WriteLine("Successfully added to leaderboard.");
        }
        else
        {
          Console.WriteLine($"Error: {response.StatusCode}");
        }
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"An error occurred: {ex.Message}");
    }
  }

  /// <summary>
  /// Method <c>ReadLeaderBoard</c> reads the leaderboard from a file.
  /// </summary>
  public List<string> ReadLeaderBoard()
  {
    try
    {
      LeaderBoard.Clear();
      string[] lines = File.ReadAllLines(path); // Reads all lines from file
      foreach (string line in lines) // Loops through lines
      {
        string[] parts = line.Split(':'); // Splits line by colon
        if (parts.Length != 2) // If not two parts
        {
          continue; // Skip to next iteration
        }
        LeaderBoard.Add(line); // Adds key-value pair to dictionary
      }
      SortLeaderBoard();
      return LeaderBoard;
    }
    catch (FileNotFoundException) // If file not found
    {
      File.Create(path);
      return LeaderBoard;
    }
    catch (Exception e) // If any other exception
    {
      File.Delete(path);
      File.Create(path);
      Console.WriteLine(e.Message);
      return LeaderBoard;
    }
  }

  /// <summary>
  /// Method <c>ClearLeaderBoard</c> clears the leaderboard.
  /// </summary>
  public void ClearLeaderBoard()
  {
    File.WriteAllText(path, string.Empty);
  }

  /// <summary>
  /// Method <c>SortLeaderBoard</c> sorts the leaderboard.
  /// </summary>
  public void SortLeaderBoard()
  {
    LeaderBoard.Sort((a, b) => int.Parse(b.Split(':')[1]).CompareTo(int.Parse(a.Split(':')[1])));
  }

  /// <summary>
  /// Method <c>GetArray</c> returns the leaderboard as an array.
  /// </summary>
  /// <returns></returns>
  public string[] GetArray()
  {
    return LeaderBoard.ToArray();
  }

}