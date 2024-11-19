using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using SadConsole.UI.Controls;

public class LeaderBoardHandler : ILeaderBoardHandler
{
  private Dictionary<string, int> LeaderBoard { get; set; }

  public string path = "Data/leaderBoard.txt";
  public LeaderBoardHandler(string url, string username)
  {
    LeaderBoard = new Dictionary<string, int>();
    FetchLeaderboard(url, username);
    ReadLeaderBoard();
    SortLeaderBoard();
  }

  public async void FetchLeaderboard(string url, string name)
  {
    try
    {
      using var client = new HttpClient();
      using var s = await client.GetStreamAsync($"{url}/api/leaderboard?name={name}");
      using var fs = new System.IO.FileStream(path, FileMode.OpenOrCreate);
      await s.CopyToAsync(fs);
    }
    catch (Exception e)
    {
      Console.WriteLine(e.Message);
    }
  }

  public void AddToLeaderboard(string name, int score)
  {
    using StreamWriter sw = File.AppendText(path);
    sw.WriteLine($"{name}:{score}");
    SortLeaderBoard();
  }

  public void ReadLeaderBoard()
  {
    try
    {
      string[] lines = File.ReadAllLines(path); // Reads all lines from file
      foreach (string line in lines) // Loops through lines
      {
        string[] parts = line.Split(':'); // Splits line by colon
        LeaderBoard.Add(parts[0], Int32.Parse(parts[1])); // Adds key-value pair to dictionary
      }
    }
    catch (FileNotFoundException) // If file not found
    {
      File.Create(path);
    }
    catch (Exception e) // If any other exception
    {
      File.Delete(path);
      File.Create(path);
      Console.WriteLine(e.Message);
    }
  }

  public void ClearLeaderBoard()
  {
    File.WriteAllText(path, string.Empty);
  }
  public void SortLeaderBoard()
  {
    var sortedDict = LeaderBoard.AsQueryable();
    sortedDict = (from entry in sortedDict orderby entry.Value descending select entry).Take(10);
    LeaderBoard = sortedDict.ToDictionary(x => x.Key, x => x.Value);
  }

  public string[] GetArray()
  {
    string[] array = new string[LeaderBoard.Count];
    int i = 0;
    foreach (KeyValuePair<string, int> kvp in LeaderBoard)
    {
      array[i] = $"{kvp.Key}: {kvp.Value}";
      i++;
    }
    return array;
  }

}