using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy
{

    private void Start()
    {
        health = maxHealth;
    }

    protected override void Cast()
    {
    }

    protected override void Death()
    {
        enemySpawner.enemiesList.Remove(this);
        DestroyObject(gameObject);
    }


}
