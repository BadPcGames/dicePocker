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


    private void FixedUpdate()
    {
        if (GameObject.FindGameObjectWithTag("player").GetComponent<playerStats>().getCanChange())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform)
                    {
                        Pick();
                    }
                }
            }
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
            transform.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-250, 250), Random.Range(100, 250), Random.Range(150, 350)));
            transform.gameObject.GetComponent<Rigidbody>().AddTorque((new Vector3(Random.Range(-100f,200f), Random.Range(-100f, 200f),Random.Range(-100f, 200f))));
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
            GameObject.FindGameObjectWithTag("player").GetComponent<handManager>().addValue(value);
        }
    }



    public int findValue()
    {
        int value = -1;
        Vector3 direction=gameObject.transform.eulerAngles;

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
