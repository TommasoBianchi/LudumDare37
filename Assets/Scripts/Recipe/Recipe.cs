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

        int resourceAmount = Constants.RESOURCE_COST_PER_TIER * tier;
        for (int i = 0; i < 1 + tier; i++)
        {
            int amount = (i == tier) ? resourceAmount : Random.Range(0, Mathf.FloorToInt(resourceAmount * 0.95f));
            if(amount > 0)
                resources.Add((ResourceType)i, amount);
            resourceAmount -= amount;
        }
    }
}
