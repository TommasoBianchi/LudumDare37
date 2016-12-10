using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlan {

    public List<Resource> loot { get; private set; }

    private List<Burst> bursts;

    private List<Enemy> spawnedEnemies;
    private float timer = 0;

    public RoomPlan(List<Burst> bursts, List<Resource> loot)
    {
        this.bursts = bursts;
        bursts.Sort();
        this.loot = loot;
    }

	public void GeneratePlan() {
		throw new System.NotImplementedException ();
	}

	public void UpdatePlan() {
        if (timer > bursts[bursts.Count - 1].time)
        {
            // spawn burst
            bursts.RemoveAt(bursts.Count - 1);
        }

        timer += Time.deltaTime;
	}

	public bool IsCleared() {
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            if (spawnedEnemies[i].Life > 0)
                return false;
        }

        return true;
	}
}
