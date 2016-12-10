using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlanFactory : MonoBehaviour {

	public List<Resource> resources;

    public static RoomPlanFactory instance { get; private set; }

    void Start()
    {
        if (instance == null)
            instance = this;
    }

	public static RoomPlan getRoomPlan(int roomID) {
        List<Burst> bursts = new List<Burst>();
        List<Resource> loot = new List<Resource>();

        int dropAmount = Random.Range(3, 7);
        //int life = (27 + (tier * 5)) * 5 / (dmg_calc)

        bool bossRoom = Random.Range(0, 5) == 0;
        if (bossRoom)
        {
            dropAmount = Mathf.RoundToInt(dropAmount * 1.20f);

            List<Enemy> enemies = new List<Enemy>();
            enemies.Add(EnemyFactory.getBoss(0));

            bursts.Add(new Burst(0, enemies, new Vector2(0.5f, 0.5f)));
        }
        else
        {
            int numberOfEnemies = Random.Range(3, 5);

            List<Enemy> enemies = new List<Enemy>();
            for (int i = 0; i < numberOfEnemies / 2; i++)
            {
                enemies.Add(EnemyFactory.getEnemy(50));
            }
        }

        for (int i = 0, remainingLoot = dropAmount; i < instance.resources.Count; i++)
        {
            if (remainingLoot <= 0)
                break;

            int resourceLootAmount = Random.Range(0, remainingLoot);
            for (int j = 0; j < resourceLootAmount; j++)
            {
                loot.Add(instance.resources[i]);
            }
        }

        return new RoomPlan(bursts, loot);
	}

    float dmg_calc(int liv, Weapon arma, Enemy enemy, Roll roll)
    {
        float dmg_modif;
        int oldupper = 0;
        int upper = 0;
        int lower = 0;
        int i = 0;
        for (i = 1; i <= arma.Tier; i++)
        {
            upper += (10 + (i - 1) * 2);
            lower = oldupper;
            oldupper = upper;
        }

        dmg_modif = (-0.5f / (upper - lower) * (liv - lower) + 0.25f) + 1;
        if (dmg_modif <= 0.01f)
            dmg_modif = 0.01f;

        switch (roll)
        {
            case Roll.Strong:
                dmg_modif = dmg_modif * (1);
                break;
            case Roll.Audacious:
                dmg_modif = dmg_modif * (1 + 0.22f);
                break;
            case Roll.Cruel:
                dmg_modif = dmg_modif * (1 - 0.18f);
                break;
            default:
                break;
        }

        if (enemy.Weaknesses.Contains(arma.Type))
        {
            dmg_modif = dmg_modif * (1 + 0.30f);
        }
        else
        {
            dmg_modif = dmg_modif * (1 - 0.12f);
        }

        return dmg_modif;
    }
}
