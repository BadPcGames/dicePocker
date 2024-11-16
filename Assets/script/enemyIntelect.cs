using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class enemyIntelect : MonoBehaviour
{
    private GameObject[] dices;

    private void Start()
    {
        dices = GameObject.FindGameObjectsWithTag("enemyDice");
    }

    public void FirstMove()
    {
        foreach (GameObject item in dices)
        {
            item.GetComponent<enemyDice>().Pick();
            item.GetComponent<enemyDice>().Throw();
        }
 
    }

    public void SecondMove()
    {
        List<int> values=transform.GetComponent<handManager>().getValues();
        int handClass = transform.GetComponent<handManager>().getHandClass();

        if (handClass == 7 || handClass == 5 || handClass == 4)
        {
            transform.GetComponent<handManager>().addValue(0);
        }
        else
        {
            selectDiceForRolling(values);
        }
        foreach (GameObject item in dices)
        {
            item.GetComponent<enemyDice>().Throw();
        }
    }

    private void selectDiceForRolling(List<int> values)
    {
        int maxCount=-1;
        List<int> maxValue= new List<int>();

        for(int i = 0; i < values.Count; i++)
        {
            if (values[i] > maxCount)
            {
                maxCount = values[i];
                maxValue = new List<int>();
                maxValue.Add(i);
            }
            else if (values[i] == maxCount)
            {
                maxValue.Add(i);
            }
        }

        foreach(GameObject dice in dices)
        {
            int diceValue=dice.GetComponent<enemyDice>().getValue();
            bool select = true;
            foreach(int el in maxValue)
            {
                if (el == diceValue-1)
                {
                    select = false;
                }
            }
            if (select)
            {
                dice.GetComponent<enemyDice>().Pick();
            }
        }


    }


}
