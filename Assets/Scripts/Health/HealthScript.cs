using System;

public class HealthScript
{
    public event EventHandler OnHealthChanged;
    public event EventHandler OnDeath;

    private float health;
    private float healthMax;
    public HealthScript(float currentHealth, float currentMaxHealth)
    {
        this.health = currentHealth;
        healthMax = currentMaxHealth;
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetHealthPercent()
    {
        return health / healthMax;
    }

    public void Damage(float damageAmount)
    {
        health -= damageAmount;
        if (health < 0)
        {
            health = 0;
            OnDeath?.Invoke(this, EventArgs.Empty);


        }
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }
    public void Heal(float healAmount)
    {
        if (health > 0)
        {
            health += healAmount;
        }
        if (health > healthMax) 
        {
            health = healthMax;
        }

        if (OnHealthChanged != null)
        {
            OnHealthChanged(this, EventArgs.Empty);
        }
    }
}
