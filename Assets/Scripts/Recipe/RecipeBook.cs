using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBook : MonoBehaviour {
	private static RecipeBook recipeBook = null;

	public List<Recipe> Recipes;

	private RecipeBook() {
		
	}

	public static RecipeBook GetInstance() {
		if (recipeBook == null) {
			recipeBook = new RecipeBook ();
		}
		return recipeBook;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void Randomize() {
		List<Recipe> recipeList = new List<Recipe>();

		WeaponData wd = new WeaponData(WeaponType.Sword, 1, Roll.None);
		List<KeyValuePair<ResourceType, int>> resList = new List<KeyValuePair<ResourceType, int>>();
		resList.Add(new KeyValuePair<ResourceType, int>(ResourceType.Wood, 1));
		recipeList.Add(new Recipe(wd, resList));

		GetInstance().Recipes = recipeList;
	}
}
