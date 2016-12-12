using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe {

    public WeaponData weaponData;
    public Dictionary<ResourceType, int> resources;

    public Recipe(WeaponData wd)
    {
        this.weaponData = wd;
        this.resources = new Dictionary<ResourceType,int>();
        int tier = wd.Tier;

        int totalAmount = Constants.RESOURCE_COST_PER_TIER * tier;

        for (int i = 0, remainingResources = totalAmount; i < 1 + tier; i++)
        {
            int resourceAmount = Mathf.RoundToInt(remainingResources * ((7 - tier) / 10f) * Random.Range(0.7f, 1.3f));
            if (resourceAmount <= 0)
                resourceAmount = Random.Range(0, 2);

            if (resourceAmount > 0)
                resources.Add((ResourceType)i, resourceAmount);

            remainingResources -= resourceAmount;
        }
    }
}
