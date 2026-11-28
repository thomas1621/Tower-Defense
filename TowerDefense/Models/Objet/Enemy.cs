using System;
using TowerDefense.Models.Maps;

namespace TowerDefense.Models.Objet;

public abstract class Enemy
{
    private readonly EnemyPath _path;

    public double MaxHealth { get; }
    public double Speed { get; }
    public int BaseDamage { get; }
    public int Reward { get; }

    public double Health { get; private set; }
    public bool IsDead => Health <= 0;

    public double DistanceTravelled { get; private set; }
    public Position Position => _path.GetPositionAt(DistanceTravelled);
    public bool HasReachedEnd => DistanceTravelled >= _path.Length;

    protected Enemy(EnemyPath path, double maxHealth, double speed, int baseDamage, int reward)
    {
        _path = path;
        MaxHealth = maxHealth;
        Health = maxHealth;
        Speed = speed;
        BaseDamage = baseDamage;
        Reward = reward;
        DistanceTravelled = 0;
    }

    public void Update(double deltaTime)
    {
        if (IsDead || HasReachedEnd)
            return;

        DistanceTravelled += Speed * deltaTime;
    }

    public void TakeDamage(double damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage), "Les dégâts ne peuvent pas être négatifs.");
        if (IsDead)
            return;

        Health = Math.Max(0, Health - damage);
    }
}

public class FastEnemy : Enemy
{
    public FastEnemy(EnemyPath path) : base(path, maxHealth: 40, speed: 3.0, baseDamage: 1, reward: 5) { }
}

public class TankEnemy : Enemy
{
    public TankEnemy(EnemyPath path) : base(path, maxHealth: 200, speed: 0.8, baseDamage: 3, reward: 15) { }
}

public class FlyingEnemy : Enemy
{
    public FlyingEnemy(EnemyPath path) : base(path, maxHealth: 60, speed: 1.8, baseDamage: 2, reward: 10) { }
}