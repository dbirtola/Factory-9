using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientsEnum
{
    ExplosiveCrate,
    Legs,
    Warhead,
    Arm,
    MetalCyclinder
}

[System.Serializable]
public class IngredientAmountPair
{
    public IngredientsEnum ingredient;
    public int currentAmount = 0;
    public int amountRequired;
}


public class Ingredient : MonoBehaviour {

    public IngredientsEnum ingredient;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
