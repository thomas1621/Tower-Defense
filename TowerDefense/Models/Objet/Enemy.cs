using System;

namespace TowerDefense.Models.Objet;

public abstract class Enemy
{
    public double MaxHealth { get; }
    public double Speed { get; }
    public int BaseDamage { get; }
    public int Reward { get; }

    public double Health { get; private set; }
    public bool IsDead => Health <= 0;

    protected Enemy(double maxHealth, double speed, int baseDamage, int reward)
    {
        MaxHealth = maxHealth;
        Health = maxHealth;
        Speed = speed;
        BaseDamage = baseDamage;
        Reward = reward;
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
    public FastEnemy() : base(maxHealth: 40, speed: 3.0, baseDamage: 1, reward: 5) { }
}

public class TankEnemy : Enemy
{
    public TankEnemy() : base(maxHealth: 200, speed: 0.8, baseDamage: 3, reward: 15) { }
}

public class FlyingEnemy : Enemy
{
    public FlyingEnemy() : base(maxHealth: 60, speed: 1.8, baseDamage: 2, reward: 10) { }
}
