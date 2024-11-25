using SadConsole;

namespace Tests.Wrappers;
public class CellSurfaceWrapper : ICellSurfaceWrapper
{
  private readonly ICellSurface _cellSurface;

  public CellSurfaceWrapper(ICellSurface cellSurface)
  {
    _cellSurface = cellSurface;
  }

  public bool IsValidCell(int x, int y)
  {
    return _cellSurface.IsValidCell(x, y);
  }
}