using System;
using System.Collections.ObjectModel;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using TowerDefense.Models.Maps;

namespace TowerDefense.ViewModels;

public partial class MainViewModel : ViewModelBase
{

    [ObservableProperty]
    public partial string Greeting { get; set; } = "Welcome to Avalonia!";

    public ObservableCollection<TileViewModel> Tiles { get; }


    public int MapWidth { get; }

    public MainViewModel()
    {
        string filePath;
        filePath = Path.Combine(AppContext.BaseDirectory, "Models", "Maps", "map1.txt");


        MapLoader loader;
        loader = new MapLoader();

        Map map;
        map = loader.Load(filePath);


        MapWidth = map.Width;


        Tiles = new ObservableCollection<TileViewModel>();


        for (int y = 0; y < map.Height; y = y + 1)
        {
            for (int x = 0; x < map.Width; x = x + 1)
            {
                Tile tile;
                tile = map.GetTile(x, y);

                TileViewModel tileViewModel;
                tileViewModel = new TileViewModel(tile);

                Tiles.Add(tileViewModel);
            }
        }
    }
}