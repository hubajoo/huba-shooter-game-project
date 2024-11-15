using System;
using DungeonCrawl.Maps;
using DungeonCrawl.UI;
using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using Game = SadConsole.Game;
using System.Collections.Generic;

namespace DungeonCrawl.Ui;


/// <summary>
/// Class <c>RootScreen</c> provides parent/child, components, and position.
/// </summary>

public class RootScreen : ScreenObject
{
  private Map _map;
  private PlayerStatsConsole _playerStatsConsole;

  private ScreenObject _leaderBoard;

  /// <summary>
  /// Constructor.
  /// </summary>
  public RootScreen(Map map, PlayerStatsConsole playerStatsConsole, LeaderBoard leaderBoard)
  {
    _map = map;
    _playerStatsConsole = playerStatsConsole;
    _leaderBoard = leaderBoard;
    AddScreenObject(_map.SurfaceObject);
    AddScreenObject(_playerStatsConsole);
  }



  /// <summary>
  /// Processes keyboard inputs.
  /// </summary>
  /// <param name="keyboard"></param>
  /// <returns></returns>
  public override void Update(TimeSpan delta)
  {
    _map.ProgressTime();
  }


  public override bool ProcessKeyboard(Keyboard keyboard)
  {
    bool handled = false;

    if (keyboard.IsKeyPressed(Keys.Up))
    {
      _map.UserControlledObject.Move(_map.UserControlledObject.Position + Direction.Up, _map);
      _map.UserControlledObject.ChangeDirection(Direction.Up);
      handled = true;
    }
    else if (keyboard.IsKeyPressed(Keys.Down))
    {
      _map.UserControlledObject.Move(_map.UserControlledObject.Position + Direction.Down, _map);
      _map.UserControlledObject.ChangeDirection(Direction.Down);
      handled = true;
    }

    if (keyboard.IsKeyPressed(Keys.Left))
    {
      _map.UserControlledObject.Move(_map.UserControlledObject.Position + Direction.Left, _map);
      _map.UserControlledObject.ChangeDirection(Direction.Left);
      handled = true;
    }
    else if (keyboard.IsKeyPressed(Keys.Right))
    {
      _map.UserControlledObject.Move(_map.UserControlledObject.Position + Direction.Right, _map);
      _map.UserControlledObject.ChangeDirection(Direction.Right);
      handled = true;
    }
    else if (keyboard.IsKeyPressed(Keys.Space))
    {
      _map.UserControlledObject.Shoot(_map);
      handled = true;
    }
    //Alternative controls
    /* 
    if (keyboard.IsKeyPressed(Keys.W)
        ||keyboard.IsKeyPressed(Keys.S)
        ||keyboard.IsKeyPressed(Keys.A)
        ||keyboard.IsKeyPressed(Keys.D)
        )
    {
        int x = 0;
        int y = 0;

        if (keyboard.IsKeyPressed(Keys.W))
        {
            y = -1;
        }
        if (keyboard.IsKeyPressed(Keys.S))
        {
            y = 1;
        }

        if (keyboard.IsKeyPressed(Keys.A))
        {
            x = -1;
        }

        if (keyboard.IsKeyPressed(Keys.D))
        {
            x = 1;
        }

        // _map.UserControlledObject.ChangeDirection(Direction.GetDirection(0,0, 10, 0));
        _map.UserControlledObject.ChangeDirection(Direction.GetDirection(x, y));
    }
    */
    _playerStatsConsole.PrintStats();

    return handled;
  }

  /// <summary>
  /// ScreenObject lifetime managementt.
  /// </summary>
  public void RemoveScreenObject(ScreenObject screenObject)
  {
    Children.Remove(screenObject);
  }
  public void AddScreenObject(ScreenObject screenObject)
  {
    Children.Add(screenObject);
  }
  public void ClearScreen()
  {
    Children.Remove(_map.SurfaceObject);
    Children.Remove(_playerStatsConsole);
  }

}