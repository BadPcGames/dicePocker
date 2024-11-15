using System.Collections.Generic;
using UnityEngine;

public class enemyDice : MonoBehaviour
{
    private bool isSelected;
    private int value;
    private Vector3 stratPosition;

    public int getValue()
    {
        return value;
    }

    private void Start()
    {
        stratPosition = transform.position;
    }

    public void Pick()
    {
        if (isSelected)
        {
            isSelected = false;
            transform.position += new Vector3(0, 0, 3);
        }
        else
        {
            isSelected = true;
            transform.position -= new Vector3(0, 0, 3);
        }
    }

    public void Throw()
    {
        if (isSelected)
        {
            GameObject.FindGameObjectWithTag("enemy").GetComponent<handManager>().removeValue(value);
            transform.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-250, 250), Random.Range(100, 250), Random.Range(-350, -150)));
            transform.gameObject.GetComponent<Rigidbody>().AddTorque((new Vector3(Random.Range(-200f,100f), Random.Range(-200f, 100f),Random.Range(-200f, 100f))));
            Invoke("returnToStart", 3.5f);
        }   
    }

    private void returnToStart()
    {
        transform.position = stratPosition;
        isSelected=false;
        tryGetvalue();
    }

    private void tryGetvalue()
    {
        if (findValue() < 0)
        {
            Invoke("tryGetvalue", 0.1f);
        }
        else
        {
            value = findValue();
            GameObject.FindGameObjectWithTag("enemy").GetComponent<handManager>().addValue(value);
        }
    }

    public int findValue()
    {
        int value = -1;
        Vector3 direction = gameObject.transform.eulerAngles;

        if (transform.GetComponent<Rigidbody>().angularVelocity == Vector3.zero)
        {
            List<Transform> sides = new List<Transform>();
            for (int i = 1; i < 7; i++)
            {
                sides.Add(transform.Find(i.ToString()));
            }

            Transform goalSide = sides[0];
            for (int i = 1; i < 6; i++)
            {
                if (goalSide.transform.position.y < sides[i].transform.position.y)
                {
                    goalSide = sides[i];
                }
            }
            value = goalSide.gameObject.GetComponent<diceValue>().getValue();
        }



        return value;
    }
}
