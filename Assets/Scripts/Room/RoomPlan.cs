using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlan {

    public List<Resource> loot { get; private set; }

    private List<Burst> bursts;

    private List<Enemy> spawnedEnemies = new List<Enemy>();
    private float timer = 0;
    private bool hasSpawnedEverything = false;

    private Room room;

    public RoomPlan(List<Burst> bursts, List<Resource> loot, Room room)
    {
        this.bursts = bursts;
        bursts.Sort();
        this.loot = loot;
        this.room = room;

        if (bursts.Count == 0)
            hasSpawnedEverything = true;
    }

	public void GeneratePlan() {
        // Not sure if useful or not
	}

	public void UpdatePlan() {
        if (bursts.Count >= 1 && timer > bursts[bursts.Count - 1].time)
        {
            // Spawn burst
            Burst burst = bursts[bursts.Count - 1];
            for (int i = 0; i < burst.enemies.Count; i++)
            {
                Vector3 burstPos = room.ViewportToWorldPoint(burst.position);
                GameObject enemy = EnemyFactory.getInstance().InstantiateEnemy(burst.enemies[i], burstPos, Quaternion.identity);
                enemy.GetComponent<AICoreUnity.MovementAI>().target = Globals.GetPlayer().GetComponent<Rigidbody2D>();
            }

            bursts.RemoveAt(bursts.Count - 1);
            if (bursts.Count == 0)
                hasSpawnedEverything = true;
        }

        timer += Time.deltaTime;
	}

	public bool IsCleared() {
        if (!hasSpawnedEverything)
            return false;

        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            if (spawnedEnemies[i].Life > 0)
                return false;
        }

        return true;
	}
}
