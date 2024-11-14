using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class handManager : MonoBehaviour
{
    private List<int> values= new List<int> { 0,0,0,0,0,0};
    int handClass = -1;


    public void addValue(int value)
    {
        values[value - 1]++;
        int sum = 0;
        foreach(int i in values)
        {
            sum += i;
        }
        if (sum == 5)
        {
            findCombination();
            Debug.Log(handClass);
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
        bool isStraight = CheckStraight();
        int maxClass=-1; 
        
        foreach(int i in values)
        {
            if (i == 5)
            {
                maxClass = 7;
            }
            else if ((i == 4)&&maxClass<6)
            {
                maxClass = 6;
            }
            else if ((i == 3) && maxClass < 3)
            {
                maxClass = 3;
            }
            else if ((i == 2) && maxClass < 1)
            {
                maxClass = 1;
            }
            else if(maxClass<1)
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
        foreach (int i in values)
        {
            if(i==2) { pairCount++; }
            if (i==3) { trioCount++;}
        }
        if (pairCount == 1 && trioCount == 1)
        {
            if(maxClass<5)
            maxClass = 5;
        }
        else if (pairCount == 2)
        {
            if(maxClass<2)
            maxClass = 2;
        }

        
    
   
        handClass=maxClass;
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
