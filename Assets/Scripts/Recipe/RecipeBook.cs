using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBook : MonoBehaviour
{

    public List<Recipe> Recipes { get; private set; }

    public RectTransform recipeUIPrefab;
    public RectTransform whereToSpawnRecipeUI;

	private static RecipeBook recipeBook = null;

	private RecipeBook() {
		
	}

	public static RecipeBook GetInstance() {
		return recipeBook;
	}

    void Start()
    {
        if (recipeBook == null)
        {
            recipeBook = this;
        }

        Randomize();
	}
	
	void Update () {
		
	}

	public static void Randomize() {
		List<Recipe> recipeList = new List<Recipe>();

        List<Weapon> weapons = WeaponFactory.getInstance().weapons;

        for (int i = 0; i < weapons.Count; i++)
        {
            for (int tier = 1; tier <= 5; tier++)
            {
                Recipe recipe = new Recipe(weapons[i].weaponData, tier);

                recipeList.Add(recipe);
                GameObject newRecipePanel = Instantiate(recipeBook.recipeUIPrefab.gameObject, recipeBook.whereToSpawnRecipeUI) as GameObject;
                newRecipePanel.GetComponent<ItemRecipeUI>().SetRecipe(recipe, tier);
            }
        }

		GetInstance().Recipes = recipeList;
	}
}
