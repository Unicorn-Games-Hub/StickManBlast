using UnityEngine;

public class BladeTutorial : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            this.gameObject.SetActive(false);
        }
    }
}
