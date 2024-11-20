using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using SadConsole.UI.Controls;

public class LeaderBoardHandler : ILeaderBoardHandler
{
  private List<string> LeaderBoard { get; set; }

  public string path = "Data/leaderBoard.txt";
  public LeaderBoardHandler(string url, string username)
  {
    LeaderBoard = new List<string>();
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
    LeaderBoard.Add($"{name}:{score}");
    using StreamWriter sw = File.AppendText(path);
    sw.WriteLine($"{name}:{score}");
    Console.WriteLine($"{name}:{score}");
  }

  public void ReadLeaderBoard()
  {
    try
    {
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
    LeaderBoard.Sort((a, b) => int.Parse(b.Split(':')[1]).CompareTo(int.Parse(a.Split(':')[1])));
  }

  public string[] GetArray()
  {
    return LeaderBoard.ToArray();
  }

}