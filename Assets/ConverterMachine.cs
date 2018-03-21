using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConverterMachine : Switch{
    public IngredientAmountPair[] ingredients;

    public GameObject outputPrefab;

    public TextMesh[] ingredientTexts;

    public float buildTime;


    bool hasFinished = false;
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
        if (hasFinished == true)
            return false;
        for (int i = 0; i < ingredients.Length; i++)
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

    IEnumerator BuildTime(float pauseTime)
    {
        yield return new WaitForSeconds(pauseTime);
    }

    bool checkSatisfied()
    {
        foreach(IngredientAmountPair iaPair in ingredients)
        {
            if (iaPair.currentAmount < iaPair.amountRequired)
                return false;
        }

        //Satisfied
        StartCoroutine(BuildTime(buildTime));
        var spot = transform.Find("OutputSpot");
                Instantiate(outputPrefab, spot.transform.position, Quaternion.identity);
        hasFinished = true;
        foreach(Activateable ai in targetObjects)
        {
            ai.Activate();
        }
        return true;
    }




}
