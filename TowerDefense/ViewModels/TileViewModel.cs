using System;
using TowerDefense.Models.Maps;

namespace TowerDefense.ViewModels;

public class TileViewModel : ViewModelBase
{

    private Tile tile;


    public TileViewModel(Tile tile)
    {
        this.tile = tile;
    }


    public int X
    {
        get
        {
            return this.tile.X;
        }
    }

    public int Y
    {
        get
        {
            return this.tile.Y;
        }
    }


    public string Color
    {
        get
        {
            if (tile is StartTile)
            {
                return "Blue";
            }
            if (tile is EndTile)
            {
                return "Red";
            }
            if (tile is PathTile)
            {
                return "Brown";
            }
            if (tile is BuildableTile)
            {
                return "Gray";
            }
            if (tile is GrassTile)
            {
                return "Green";
            }

            return "Black";
        }
    }
}