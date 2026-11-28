using System;
using System.Collections.ObjectModel;
using System.IO;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using TowerDefense.Models.Maps;
using TowerDefense.Models.Objet;

namespace TowerDefense.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private const double TileSize = 40;

    private const double FrameTime = 1.0 / 30;

    private EnemyManager enemyManager;
    private DispatcherTimer timer;

    public ObservableCollection<TileViewModel> Tiles { get; }
    public ObservableCollection<EnemyViewModel> Enemies { get; }

    public int MapWidth { get; }

    public double MapPixelWidth { get; }
    public double MapPixelHeight { get; }

    public MainViewModel()
    {
        // ----- 1. La carte -----
        string filePath;
        filePath = Path.Combine(AppContext.BaseDirectory, "Models", "Maps", "map1.txt");

        MapLoader loader;
        loader = new MapLoader();

        Map map;
        map = loader.Load(filePath);

        MapWidth = map.Width;
        MapPixelWidth = map.Width * TileSize;
        MapPixelHeight = map.Height * TileSize;

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

        // ----- 2. Le chemin des ennemis -----
        PathFinder pathFinder;
        pathFinder = new PathFinder();

        EnemyPath path;
        path = pathFinder.FindPath(map);

        // ----- 3. Les ennemis -----
        enemyManager = new EnemyManager();
        Enemies = new ObservableCollection<EnemyViewModel>();

        Enemy enemy;
        enemy = new TankEnemy(path);

        enemyManager.AddEnemy(enemy);

        EnemyViewModel enemyViewModel;
        enemyViewModel = new EnemyViewModel(enemy);

        Enemies.Add(enemyViewModel);

        // ----- 4. La boucle de jeu -----
        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(FrameTime);
        timer.Tick += OnTick;
        timer.Start();
    }


    private void OnTick(object? sender, EventArgs e)
    {

        enemyManager.Update(FrameTime);


        foreach (EnemyViewModel enemyViewModel in Enemies)
        {
            enemyViewModel.Refresh();
        }
    }
}