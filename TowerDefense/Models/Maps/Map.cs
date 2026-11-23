using System;

namespace TowerDefense.Models.Maps;

public class Map
{
    public int Width { get; }
    public int Height { get; }

    private Tile[,] tiles;

    public Map(int width, int height)
    {
        Width = width;
        Height = height;
        tiles = new Tile[width, height];
    }

    public void SetTile(Tile tile)
    {
        tiles[tile.X, tile.Y] = tile;
    }

    public Tile GetTile(int x, int y)
    {
        return tiles[x, y];
    }
}