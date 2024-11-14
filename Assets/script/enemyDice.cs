using UnityEngine;

public class enemyDice : MonoBehaviour
{
    private bool isSelected;
    private int value;
    private Vector3 stratPosition;


    private void Start()
    {
        stratPosition = transform.position;
        isSelected = true;
        transform.position -= new Vector3(7, 0, 0);
    }


   

    public void Throw()
    {
        if (isSelected)
        {
            GameObject.FindGameObjectWithTag("enemy").GetComponent<handManager>().removeValue(value);
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
            GameObject.FindGameObjectWithTag("enemy").GetComponent<handManager>().addValue(value);
        }
    }



    public int findValue()
    {
        int value = -1;
        Vector3 direction=gameObject.transform.eulerAngles;

        direction = new Vector3(Mathf.RoundToInt(direction.x), Mathf.RoundToInt(direction.y), Mathf.RoundToInt(direction.z));
        if(direction.x==180&&direction.y==270||
            direction.x == 0 && direction.z == 90)
        {
            value = 1;
        }
        else if (direction.x == 270)
        {
            value = 2;
        }
        else if (direction.x == 180 && direction.z == 0 ||
           direction.x == 0 && direction.z == 180)
        {
            value = 3;
        }
        else if (direction.x == 180 && direction.z == 180 ||
           direction.x == 0 && direction.z == 0)
        {
            value = 4;
        }
        else if (direction.x == 90)
        {
            value = 5;
        }
        else if (direction.x == 0 && direction.z == 270 ||
           direction.x == 180 && direction.z == 90)
        {
            value = 6;
        }

        return value;
    }
}
