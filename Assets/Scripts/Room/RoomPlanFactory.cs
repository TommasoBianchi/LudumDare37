using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlanFactory : MonoBehaviour {

    public static RoomPlanFactory instance;

    private const float BOSS_PROB = 0.25f;

    public static RoomPlanFactory getInstance() {
        return instance;
    }

    void Start()
    {
        instance = this;
    }

	public RoomPlan getRoomPlan(int roomID, Room room) {

        List<Burst> bursts = new List<Burst>();
        List<ResourceType> loot = new List<ResourceType>();

        int dropAmount = Random.Range(10, 16);

        bool bossRoom = Random.value < BOSS_PROB;
        
        if (bossRoom)
        {
            dropAmount = Mathf.RoundToInt(dropAmount * 1.20f);

            List<EnemyData> enemies = new List<EnemyData>();
            enemies.Add(EnemyFactory.getInstance().getBoss(roomID, Random.Range(4f, 6f)));

            bursts.Add(new Burst(0, enemies, new Vector2(0.5f, 0.5f)));
        }
        else
        {
            int numberOfEnemies = Random.Range(3, 5) + roomID;
            int numberOfBursts = 2 + roomID / 4;
            int numberOfEnemiesPerBurst = numberOfEnemies / numberOfBursts;

            for (int b = 0; b < numberOfBursts; b++)
            {
                if (b == numberOfBursts - 1)
                    numberOfEnemiesPerBurst = numberOfEnemies;

                List<EnemyData> enemies = new List<EnemyData>();
                for (int i = 0; i < numberOfEnemiesPerBurst; i++)
                {
                    enemies.Add(EnemyFactory.getInstance().getEnemy(roomID, Random.Range(0.9f, 1.1f)));
                }
                numberOfEnemies -= numberOfEnemiesPerBurst;
                bursts.Add(new Burst(b * Random.Range(1.5f, 3f), enemies, (Random.insideUnitCircle + Vector2.one) / 2f));
            }
        }
		
	var roomTier = 1;
	while(roomId - (roomTier + 1) * (roomTier + 2) / 2 >= 0)
		roomTier++;

        var resources = System.Enum.GetValues(typeof(ResourceType));
        for (int i = 0, remainingLoot = dropAmount; i < Mathf.Min(resources.Length, roomTier + 2); i++)
        {
	    int resourceLootAmount = Mathf.RoundToInt(remainingLoot * ((7 - roomTier) / 10f));
	    if(resourceLootAmount <= 0)
	 	resourceLootAmount = Random.Range(0, 2);
		
            for (int j = 0; j < resourceLootAmount; j++)
            {
                loot.Add((ResourceType)resources.GetValue(i));
            }

            remainingLoot -= resourceLootAmount;
        }

        return new RoomPlan(bursts, loot, room);
	}    
}
