using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe {

    public WeaponData weaponData;
    public Dictionary<ResourceType, int> resources;

    public Recipe(WeaponData wd, int tier)
    {
        this.weaponData = wd;
        this.resources = new Dictionary<ResourceType,int>();

        int resourceAmount = 25 * tier;
        for (int i = 0; i < 1 + tier; i++)
        {
            int amount = (i == tier) ? resourceAmount : Random.Range(0, Mathf.FloorToInt(resourceAmount * 0.95f));
            resources.Add((ResourceType)i, amount);
            resourceAmount -= amount;
        }
    }
}
