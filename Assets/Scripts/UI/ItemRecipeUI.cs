using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemRecipeUI : MonoBehaviour {

    private Recipe recipe;

    void OnEnable()
    {
        Dictionary<ResourceType, int> playerResources = Globals.GetPlayerController().resources;

        bool enable = true;

        for (int i = 0; i < recipe.resources.Count; i++)
        {
            
        }
    }

    public void SetRecipe(Recipe recipe)
    {
        this.recipe = recipe;
    }
}
