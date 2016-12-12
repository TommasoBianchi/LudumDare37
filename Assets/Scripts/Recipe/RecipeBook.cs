using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBook : MonoBehaviour
{

    public List<Recipe> Recipes { get; private set; }

    public RectTransform recipeUIPrefab;
    public RectTransform whereToSpawnRecipeUI;

    private static RecipeBook recipeBook = null;

    void Start()
    {
        if (recipeBook == null)
        {
            recipeBook = this;
        }

        Randomize();

        gameObject.SetActive(false);
    }

    public void OpenOnKeypress()
    {
        if (Input.GetKeyDown(KeyCode.E))
            gameObject.SetActive(!gameObject.activeSelf);

        if (Input.GetKeyDown(KeyCode.Escape))
            gameObject.SetActive(false);
    }

	public static RecipeBook GetInstance() {
		return recipeBook;
	}

	public static void Randomize() {
		List<Recipe> recipeList = new List<Recipe>();

        List<Weapon> weapons = WeaponFactory.getInstance().weapons;

        for (int i = 0; i < weapons.Count; i++)
        {
            for (int tier = 1; tier <= 5; tier++)
            {
                Recipe recipe = new Recipe(new WeaponData(weapons[i].weaponData.Type, tier, Roll.None));

                recipeList.Add(recipe);                
            }
        }

        // Randomize list positions
        for (int i = 0; i < recipeList.Count * 2; i++)
        {
            int firstIndex = Random.Range(0, recipeList.Count);
            int secondIndex = Random.Range(0, recipeList.Count);

            Recipe tmp = recipeList[firstIndex];
            recipeList[firstIndex] = recipeList[secondIndex];
            recipeList[secondIndex] = tmp;
        }

        // Generate UI
        for (int i = 0; i < recipeList.Count; i++)
        {
            GameObject newRecipePanel = Instantiate(recipeBook.recipeUIPrefab.gameObject, recipeBook.whereToSpawnRecipeUI) as GameObject;
            newRecipePanel.GetComponent<ItemRecipeUI>().SetRecipe(recipeList[i]);
        }

		GetInstance().Recipes = recipeList;
	}
}
