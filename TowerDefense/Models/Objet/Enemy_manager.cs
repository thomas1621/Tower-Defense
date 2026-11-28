using System.Collections.Generic;

namespace TowerDefense.Models.Objet;

public class EnemyManager
{
    private List<Enemy> enemies;

    public EnemyManager()
    {
        enemies = new List<Enemy>();
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void Update(double deltaTime)
    {
        foreach (var enemy in enemies)
        {
            enemy.Update(deltaTime);
        }
    }
}
