using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ItemRecipeUI : MonoBehaviour {

    public Text itemNameText;
    public RectTransform resourcesPanel;

    private Recipe recipe;
    private WeaponData weapon;

    void OnEnable()
    {
        if (recipe == null)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            return;
        }

        Dictionary<ResourceType, int> playerResources = Globals.GetPlayerController().resources;

        ResourceType[] resourcesNeeded = recipe.resources.Keys.ToArray();
        bool enable = true;

        for (int i = 0; i < resourcesNeeded.Length; i++)
        {
            if (playerResources.ContainsKey(resourcesNeeded[i]) == false || playerResources[resourcesNeeded[i]] < recipe.resources[resourcesNeeded[i]])
            {
                enable = false;
            }
        }

        transform.GetChild(0).gameObject.SetActive(enable);

        if (enable)
            SetLabel(recipe, playerResources, resourcesNeeded);
    }

    public void SetRecipe(Recipe recipe)
    {
        this.recipe = recipe;
        this.weapon = recipe.weaponData;

        itemNameText.text = weapon.Type + " T" + recipe.weaponData.Tier;

        var playerResources = Globals.GetPlayerController().resources;

        ResourceType[] resourcesNeeded = recipe.resources.Keys.ToArray();

        SetLabel(recipe, playerResources, resourcesNeeded);

        for (int i = resourcesNeeded.Length; i < 6; i++)
        {
            resourcesPanel.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void SetLabel(Recipe recipe, Dictionary<ResourceType, int> playerResources, ResourceType[] resourcesNeeded)
    {
        for (int i = 0; i < resourcesNeeded.Length; i++)
        {
            Text[] text = resourcesPanel.GetChild(i).GetComponentsInChildren<Text>();
            text[0].text = resourcesNeeded[i].ToString();
            int playerResourceAmount = playerResources.ContainsKey(resourcesNeeded[i]) ? playerResources[resourcesNeeded[i]] : 0;
            text[1].text = playerResourceAmount + " / " + recipe.resources[resourcesNeeded[i]];
        }
    }

    public void CraftWeapon()
    {
        var rolls = System.Enum.GetValues(typeof(Roll));
        weapon.Roll = (Roll)rolls.GetValue(Random.Range(0, rolls.Length));

        Globals.GetPlayerController().WeaponData = weapon;

        var playerResources = Globals.GetPlayerController().resources;
        ResourceType[] resourcesNeeded = recipe.resources.Keys.ToArray();

        for (int i = 0; i < resourcesNeeded.Length; i++)
        {
            playerResources[resourcesNeeded[i]] -= recipe.resources[resourcesNeeded[i]];
        }

        GameObject text = Instantiate(Globals.GetPlayerController().Text, Globals.GetPlayer().transform.position, Quaternion.identity) as GameObject;
        text.transform.SetParent(GameObject.Find("OverlayCanvas").transform);
        text.GetComponent<Text>().color = Color.white;
        string rollName = ("" + Globals.GetPlayerController().WeaponData.Roll).Equals("None") ? "" : ("" + Globals.GetPlayerController().WeaponData.Roll);
        text.GetComponent<Text>().text = rollName + " " + Globals.GetPlayerController().WeaponData.Type + " T" + Globals.GetPlayerController().WeaponData.Tier;
        text.GetComponent<DestroyAfter>().after = 3.0f;
        text.GetComponent<MoveUp>().speed = 0.005f;

        RecipeBook.GetInstance().gameObject.SetActive(false);
    }
}
