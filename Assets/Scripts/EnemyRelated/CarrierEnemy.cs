using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierEnemy : Enemy
{
    public GameObject onDeathSpawningEnemyPrefab;
    public int numberOfSpawn;
    public float spawnDelay;

    private void Start()
    {
        health = maxHealth;
    }

    protected override void Cast()
    {
    }

    protected override void Death()
    {
        int currentIndex = GetComponent<EnemyPathing>().currentPathIndex;
        if (currentIndex != 0)
            currentIndex--;
        enemySpawner.enemiesList.Remove(this);
        enemySpawner.StartCoroutine(enemySpawner.SpawnCarrierSubEnemy(onDeathSpawningEnemyPrefab, transform.position, currentIndex, numberOfSpawn ,spawnDelay));
        Destroy(gameObject);
    }

}
