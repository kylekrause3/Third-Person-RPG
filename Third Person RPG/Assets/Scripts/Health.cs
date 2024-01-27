using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float regen;
    public float maxHealth = 100;
    private float health;
    private HealthBar healthBar;
    [HideInInspector] public Defense defense;

    private float timeSinceLastDmg;

    private void Start()
    {
        healthBar = this.transform.GetComponentInChildren<HealthBar>();
        defense = this.GetComponent<Defense>();
        health = maxHealth;

        healthBar.SetMaxHealth(health);
    }

    private void Update()
    {

        if (Time.time - timeSinceLastDmg > 5f && health < maxHealth)
        {
            heal(regen * Time.deltaTime);
        }
    }

    public void takeDamage(float x)
    {
        health -= x;
        healthBar.SetHealth(health);

        if(health < 0)
            health = 0;

        timeSinceLastDmg = Time.time;
    }

    public void attack(float x) { 
        if(defense != null)
        {
            if (defense.getUpdate() == false)
            {
                defense.startDefense();
            }
        }
        takeDamage(x); 
    }

    public void heal(float x)
    {
        health += x;

        if (health > maxHealth)
            health = maxHealth;

        healthBar.SetHealth(health);
    }

    public float getHealth()
    {
        return health;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }

    public void setHealth(float x)
    {
        float diff = x - health;
        heal(diff);
    }

    public void setToMaxHealth()
    {
        setHealth(maxHealth);
    }

}
