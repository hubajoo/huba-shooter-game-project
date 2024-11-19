using SadRogue.Primitives;

public interface IDirectionChoiche
{
  public Direction GetDirection(Point position, Point targetPosition);
  public Direction GetDirection();
}