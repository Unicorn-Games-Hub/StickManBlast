using UnityEngine;

public class SawHandler : MonoBehaviour
{
    public void DisableAllTheShawMachine()
    {
        for(int i=0;i<transform.childCount;i++)
        {
           transform.GetChild(i).GetComponent<SawMachine>()._canSawRotate=false;
        }
    }
}
