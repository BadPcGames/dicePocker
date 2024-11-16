using System.Collections.Generic;
using UnityEngine;

public class playerDice : MonoBehaviour
{
    public  bool isSelected;
    private int value;
    private Vector3 stratPosition;




    public void ChangeForStart()
    {
        stratPosition = transform.position;
        isSelected = false;
        Pick();
    }


    private void OnMouseDown()
    {
        tryPick();
    }

    public void tryPick()
    {
        if (GameObject.FindGameObjectWithTag("player").GetComponent<playerStats>().getCanChange())
        {
            Pick();
        }
    }

    public void Pick()
    {
        if (isSelected)
        {
            transform.position -= new Vector3(0, 0, 3);
            isSelected = false;
        }
        else
        {
            transform.position += new Vector3(0, 0, 3);
            isSelected = true;
        }
        transform.gameObject.GetComponent<diceSound>().playPickSound();
    }

    public void Throw()
    {
        if (isSelected)
        {
            GameObject.FindGameObjectWithTag("player").GetComponent<handManager>().removeValue(value);
            GameObject.FindGameObjectWithTag("player").GetComponent<playerStats>().setCanChange(false);
            transform.gameObject.GetComponent<Rigidbody>().AddForce(0, Random.Range(50, 150), Random.Range(100, 250));
            transform.gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(100, 350), Random.Range(100, 300), Random.Range(-300,300)));
            Invoke("tryGetvalue", 3.5f);
        }   
    }

    private void returnToStart()
    {
        transform.position = stratPosition;
        isSelected = false;
        if (value == 1 || value == 6)
        {
            float currentAngle = transform.eulerAngles.z;

            float roundedAngle = Mathf.Round(currentAngle / 90) * 90;

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, roundedAngle);
        }
        else if (value == 4 || value == 3)
        {
            float currentAngle = transform.eulerAngles.y;

            float roundedAngle = Mathf.Round(currentAngle / 90) * 90;

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, roundedAngle, transform.eulerAngles.z);
        }
        else if (value == 2 || value == 5)
        {
            float currentAngle = transform.eulerAngles.x;

            float roundedAngle = Mathf.Round(currentAngle / 90) * 90;

            transform.eulerAngles = new Vector3(roundedAngle, transform.eulerAngles.y, transform.eulerAngles.z);
        }
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
            GameObject.FindGameObjectWithTag("player").GetComponent<handManager>().addValue(value);
            returnToStart();
        }
    }



    public int findValue()
    {
        int value = -1;
        if(transform.GetComponent<Rigidbody>().angularVelocity == Vector3.zero)
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
