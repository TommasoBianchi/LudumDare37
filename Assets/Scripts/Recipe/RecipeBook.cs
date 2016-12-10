using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBook : MonoBehaviour {
	private static RecipeBook recipeBook = null;

	public List<Recipe> Recipes;

	private RecipeBook() {
		
	}

	public RecipeBook GetInstance() {
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

	public void static Randomize() {
		throw new System.NotImplementedException ();
	}
}
