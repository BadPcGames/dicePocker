using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class resultShow : MonoBehaviour
{
    public float dilay;
    public Animator animator;

    public void show(string message,Color32 color)
    {
        Debug.Log(color);
        transform.GetChild(0).GetComponent<TMP_Text>().text=message;
        transform.GetChild(0).GetComponent<TMP_Text>().color =color;
        animator.SetTrigger("appear");
        Invoke("back",dilay);
    }

    private void back()
    {
        animator.SetTrigger("back");
    }
}
