using System;
using TowerDefense.Models.Maps;
using TowerDefense.Models.Objet;

namespace TowerDefense.ViewModels;

public class EnemyViewModel : ViewModelBase
{
    // Taille d'une case en pixels (la même que pour les cases de la carte)
    private const double TileSize = 40;

    // Diamètre du rond qui représente l'ennemi
    private const double Size = 20;

    // L'ennemi du Model que ce ViewModel représente
    private Enemy enemy;

    public EnemyViewModel(Enemy enemy)
    {
        this.enemy = enemy;
    }

    // La taille du rond, pour la Vue
    public double Diameter
    {
        get
        {
            return Size;
        }
    }

    // Position horizontale du coin haut-gauche du rond, en pixels
    public double X
    {
        get
        {
            Position position;
            position = this.enemy.Position;

            double centerInPixels;
            centerInPixels = (position.X + 0.5) * TileSize;

            return centerInPixels - (Size / 2);
        }
    }

    // Position verticale du coin haut-gauche du rond, en pixels
    public double Y
    {
        get
        {
            Position position;
            position = this.enemy.Position;

            double centerInPixels;
            centerInPixels = (position.Y + 0.5) * TileSize;

            return centerInPixels - (Size / 2);
        }
    }

    // La couleur selon le type d'ennemi
    public string Color
    {
        get
        {
            if (enemy is FastEnemy)
            {
                return "Yellow";
            }

            if (enemy is TankEnemy)
            {
                return "Purple";
            }

            if (enemy is FlyingEnemy)
            {
                return "White";
            }

            return "Black";
        }
    }

    // À appeler à chaque image : prévient la Vue que la position a changé
    public void Refresh()
    {
        OnPropertyChanged(nameof(X));
        OnPropertyChanged(nameof(Y));
    }
}