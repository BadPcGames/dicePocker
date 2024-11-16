using UnityEngine;

public class settings : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;

    public void changeActive()
    {
        if (menu.active)
        {
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
        }
    }
}
