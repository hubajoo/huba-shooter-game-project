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
  private List<string> _leaderBoard;

  private string _path;

  private string _url;

  /// <summary>
  /// Constructor for LeaderBoardHandler
  /// </summary>
  /// <param name="settings"></param>
  /// <param name="path"></param>
  public LeaderBoardHandler(IGameSettings settings, string path) //= "Data/leaderboard.txt")
  {
    _url = settings.ServerUrl;
    _leaderBoard = new List<string>();
    _path = path;
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

          ClearLeaderBoard();

          var leaderboardEntries = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LeaderBoardEntry>>(content);
          foreach (var entry in leaderboardEntries)
          {
            string line = $"{entry.Name}:{entry.Score}";
            _leaderBoard.Add(line);
            using StreamWriter sw = File.AppendText(_path);
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
      throw new Exception($"An error occurred: {ex.Message}");
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
      _leaderBoard.Add($"{name}:{score}");
      using StreamWriter sw = File.AppendText(_path);
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
      _leaderBoard.Clear();
      string[] lines = File.ReadAllLines(_path); // Reads all lines from file
      foreach (string line in lines) // Loops through lines
      {
        string[] parts = line.Split(':'); // Splits line by colon
        if (parts.Length != 2) // If not two parts
        {
          continue; // Skip to next iteration
        }
        _leaderBoard.Add(line); // Adds key-value pair to dictionary
      }
      SortLeaderBoard();
      return _leaderBoard;
    }
    catch (FileNotFoundException) // If file not found
    {
      File.Create(_path);
      return _leaderBoard;
    }
    catch (Exception e) // If any other exception
    {
      File.Delete(_path);
      File.Create(_path);
      Console.WriteLine(e.Message);
      return _leaderBoard;
    }
  }

  /// <summary>
  /// Method <c>ClearLeaderBoard</c> clears the leaderboard.
  /// </summary>
  public void ClearLeaderBoard()
  {
    File.WriteAllText(_path, string.Empty);
    _leaderBoard.Clear();
  }

  /// <summary>
  /// Method <c>SortLeaderBoard</c> sorts the leaderboard.
  /// </summary>
  public void SortLeaderBoard()
  {
    _leaderBoard.Sort((a, b) => int.Parse(b.Split(':')[1]).CompareTo(int.Parse(a.Split(':')[1])));
  }

  /// <summary>
  /// Method <c>GetArray</c> returns the leaderboard as an array.
  /// </summary>
  /// <returns></returns>
  public string[] GetArray()
  {
    return _leaderBoard.ToArray();
  }

}