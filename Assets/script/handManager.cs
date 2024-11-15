using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class handManager : MonoBehaviour
{
    private List<int> values= new List<int> { 0,0,0,0,0,0};
    int handClass = -1;
    int hand1Combynation = -1;
    int hand2Combynation = -1;
    List <int> maxDiceWithoutCombination = new List<int>();

    public List<int> getValues()
    {
        return values;
    }
    public int getHandClass()
    {
        return handClass;
    }
    public int getHand1Combynation()
    {
        return hand1Combynation;
    }
    public int getHand2Combynation()
    {
        return hand2Combynation;
    }
    public List<int> getMaxDiceWithoutCombination()
    {
        return maxDiceWithoutCombination;
    }

    public void addValue(int value)
    {
        if (value >= 1)
        {
            values[value - 1]++;
        }
        int sum = 0;
        foreach(int i in values)
        {
            sum += i;
        }
        if (sum == 5)
        {
            findCombination();
            Debug.Log(handClass);
            GameObject.FindGameObjectWithTag("game").GetComponent<gameLogic>().changeMove();
        }
    }

    public void removeValue(int value)
    {
        if (value >= 1)
        {
            values[value - 1]--;
        }
    }


    private void findCombination()
    {
        int maxClass = -1;
        hand1Combynation = -1;
        int maxComb1 = 0;
        int maxComb2 = 0;
        hand2Combynation = -1;
        maxDiceWithoutCombination = new List<int>();

        for (int i = 0; i < values.Count(); i++)
        {
            if (values[i] == 5)
            {
                maxClass = 7;
            }
            else if ((values[i] == 4) && maxClass < 6)
            {
                maxClass = 6;
            }
            else if ((values[i] == 3) && maxClass < 3)
            {
                maxClass = 3;
            }
            else if ((values[i] == 2) && maxClass < 1)
            {
                maxClass = 1;
            }
            else if (maxClass < 1)
            {
                maxClass = 0;
            }
        }

        if (maxClass < 4)
        {
            if (CheckStraight())
            {
                maxClass = 4;
            }
        }

        int pairCount = 0;
        int trioCount = 0;
        for (int i = 0; i < values.Count(); i++)
        {
            if (values[i] == 2) { pairCount++; }
            if (values[i] == 3) { trioCount++; }
        }

        if (pairCount == 1 && trioCount == 1)
        {
            if (maxClass < 5)
                maxClass = 5;
        }

        else if (pairCount == 2)
        {
            if (maxClass < 2)
                maxClass = 2;
        }

        for (int i = 0; i < values.Count; i++)
        {
            if (values[i] >= maxComb1)
            {
                maxComb2 = maxComb1;
                hand2Combynation = hand1Combynation;
                maxComb1 = values[i];
                hand1Combynation = i + 1;
            }
        }

        for(int i = 0; i < values.Count; i++)
        {
            if (values[i] != hand1Combynation || values[i] != hand2Combynation)
            {
                maxDiceWithoutCombination.Add(values[i]);
            }
        }
        maxDiceWithoutCombination.Reverse();

        Debug.Log("Max Comb 1 = " + maxComb1);
        Debug.Log("Max Comb 2 = " + maxComb2);
        Debug.Log("Comb 1 = " + hand1Combynation);
        Debug.Log("Comb 2 = " + hand2Combynation);
        Debug.Log("List = " + maxDiceWithoutCombination);

        handClass = maxClass;
    }  


    private bool CheckStraight()
    {

        foreach (int item in values) 
        {
            if (item > 1)
            {
                return false;
            }
        }
        return  (values[0] == 0 || values[5] == 0);
    }
}
