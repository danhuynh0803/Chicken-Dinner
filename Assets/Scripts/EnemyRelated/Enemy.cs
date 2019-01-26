using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour {

    [Header("Health Settings")]
    public int maxHealth;
    protected int health;
    public Image healthBarImage;

    public EnemySpawner enemySpawner;
    
    public int damage;
    protected abstract void Cast();
    protected abstract void Death();
    //public abstract void TakeDamage(int damage);
    public bool isByAutarca;

    private void OnMouseDown()
    {
        Camera.main.GetComponent<CameraMovement>().LockOnTarget(transform);
    }

    // This is the same as damaging the player
    // Base is used just to differentiate player structures later
    public void DamagePlayer()
    {
        Debug.Log("Damage Player");        
    }

    // TODO: create a framework for which objects can be destroyed by enemies
    public void DamageObject(GameObject gameObject)
    {

    }

    public virtual void TakeDamage(int damage)
    {
        health = (int)Mathf.Clamp(health - damage, 0f, maxHealth);
        healthBarImage.fillAmount = (float)health / maxHealth;
        if (health <= 0)
        {
            Death();

        }
    }
}
