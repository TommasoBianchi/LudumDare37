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
        
        for (int i = 0, remainingResources = resourceAmount; i < 1 + tier; i++)
        {
	        int resourceAmount = Mathf.RoundToInt(remainingResources * ((7 - tier) / 10f));
	        if(resourceAmount <= 0)
	 	        resourceAmount = Random.Range(0, 2);
            
            if(amount > 0)
                resources.Add((ResourceType)i, resourceAmount);

            remainingResources -= resourceAmount;
        }
    }
}
