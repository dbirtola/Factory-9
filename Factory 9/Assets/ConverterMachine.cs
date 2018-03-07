using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConverterMachine : MonoBehaviour {
    public IngredientAmountPair[] ingredients;

    public GameObject outputPrefab;

    public TextMesh[] ingredientTexts;

	// Use this for initialization
	void Start () {
		if(ingredients == null)
        {
            Debug.LogWarning("No ingredients added to ConverterMachine");
        }

        for(int i = 0; i < ingredients.Length; i++)
        {
            if(ingredientTexts[i] != null)
            {
                ingredientTexts[i].text = ingredients[i].currentAmount + "/" + ingredients[i].amountRequired; 
            }
        }
       
	}


    public bool receivedIngredient(Ingredient ingredient)
    {
        for(int i = 0; i < ingredients.Length; i++)
        {
            if (ingredients[i].ingredient == ingredient.ingredient)
            {
                ingredients[i].currentAmount++;
                if(ingredientTexts[i] != null)
                {
                    ingredientTexts[i].text = ingredients[i].currentAmount + "/" + ingredients[i].amountRequired;
                }
                break;
            }
        }


        return checkSatisfied();
    }

    bool checkSatisfied()
    {
        foreach(IngredientAmountPair iaPair in ingredients)
        {
            if (iaPair.currentAmount < iaPair.amountRequired)
                return false;
        }

        //Satisfied
        var spot = transform.Find("OutputSpot");
        Instantiate(outputPrefab, spot.transform.position, Quaternion.identity);
        return true;
    }




}
