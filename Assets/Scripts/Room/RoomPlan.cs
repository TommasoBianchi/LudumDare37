using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlan {

    public List<ResourceType> loot { get; private set; }

    private List<Burst> bursts;

    private List<Enemy> spawnedEnemies = new List<Enemy>();
    private float timer = -5; // 5 seconds starting delay

    private bool canSpawn = false;
    private bool hasSpawnedEverything = false;

    private Room room;

    public RoomPlan(List<Burst> bursts, List<ResourceType> loot, Room room)
    {
        this.bursts = bursts;
        bursts.Sort();
        this.loot = loot;
        this.room = room;

        if (bursts.Count == 0)
            hasSpawnedEverything = true;
    }

	public void StartPlan() {
        canSpawn = true;
	}

	public void UpdatePlan() {
        if (!canSpawn)
            return;

        if (bursts.Count >= 1 && timer > bursts[bursts.Count - 1].time)
        {
            // Spawn burst
            Burst burst = bursts[bursts.Count - 1];
            for (int i = 0; i < burst.enemies.Count; i++)
            {
                Vector2 vieportSpawnPosition = burst.position;

                vieportSpawnPosition.x += ((Random.Range(0, 2) == 0) ? 1 : -1) * Random.Range(0.01f, 0.1f);
                vieportSpawnPosition.x = Mathf.Clamp01(vieportSpawnPosition.x);
                vieportSpawnPosition.y += ((Random.Range(0, 2) == 0) ? 1 : -1) * Random.Range(0.01f, 0.1f);
                vieportSpawnPosition.y = Mathf.Clamp01(vieportSpawnPosition.y);

                Vector3 burstPos = room.ViewportToWorldPoint(vieportSpawnPosition);
                GameObject enemy = EnemyFactory.getInstance().InstantiateEnemy(burst.enemies[i], burstPos, Quaternion.identity);
                enemy.GetComponent<AICoreUnity.MovementAI>().target = Globals.GetPlayer().GetComponent<Rigidbody2D>();
                spawnedEnemies.Add(enemy.GetComponent<Enemy>());
            }

            bursts.RemoveAt(bursts.Count - 1);
            if (bursts.Count == 0)
                hasSpawnedEverything = true;
        }

        timer += Time.deltaTime;
	}

    public bool IsCleared()
    {
        if (!hasSpawnedEverything)
            return false;

        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            if (spawnedEnemies[i].Life > 0)
            {
                return false;
            }
        }

        return true;
	}
}
