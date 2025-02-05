using System;
using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;

namespace DungeonCrawl.Ui;


/// <summary>
/// Class <c>RootScreen</c> provides parent/child, components, and position.
/// </summary>

public class RootScreen : ScreenObject
{
  private IMap _map;

  /// <summary>
  /// Constructor.
  /// </summary>
  public RootScreen(IMap map)
  {
    _map = map;
    Children.Add(_map.SurfaceObject);
  }


  /// <summary>
  /// Processes keyboard inputs.
  /// </summary>
  /// <param name="keyboard"></param>
  /// <returns></returns>

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
      _map.UserControlledObject.Shoot();
      handled = true;
    }
    else if (keyboard.IsKeyPressed(Keys.Escape))
    {
      _map.UserControlledObject.EndGame();
      handled = true;
    }
    return handled;
  }

  public override void Update(TimeSpan delta)
  {
    _map.ProgressTime();
    foreach (var child in Children)
    {
      child.Update(delta);
    }
  }
}