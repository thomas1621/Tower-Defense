namespace TowerDefense.Models.Map;

public abstract class Tile
{
    public int X { get; }
    public int Y { get; }
    public virtual bool IsWalkable => false;
    public virtual bool IsBuildable => false;
    protected Tile(int x, int y)
    {
        X = x;
        Y = y;
    }
    public class GrassTile : Tile
    {
        public GrassTile(int x, int y) : base(x, y) { }
    }
    public class PathTile : Tile
    {
        public PathTile(int x, int y) : base(x, y) { }
        public override bool IsWalkable => true;
    }
    public class BuildableTile : Tile
    {
        public bool IsOccupied { get; private set; }
        public BuildableTile(int x, int y) : base(x, y)
        {
            IsOccupied = false;
        }
        public override bool IsBuildable => !IsOccupied;
        public void Occupy()
        {
            IsOccupied = true;
        }
        public void Vacate()
        {
            IsOccupied = false;
        }
    }
    public class StartTile : PathTile
    {
        public StartTile(int x, int y) : base(x, y) { }
        public override bool IsWalkable => true;
    }
    public class EndTile : PathTile
    {
        public EndTile(int x, int y) : base(x, y) { }
        public override bool IsWalkable => true;
    }
}