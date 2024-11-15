using UnityEngine;

public class playerStats : MonoBehaviour
{
    private bool canChange;

    private void Start()
    {
        canChange = false;
    }
    public void setCanChange(bool value)
    {
        canChange=value;
    }
    public bool getCanChange()
    {
        return canChange;
    }
}
