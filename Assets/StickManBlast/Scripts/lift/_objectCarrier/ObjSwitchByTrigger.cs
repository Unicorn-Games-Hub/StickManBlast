using UnityEngine;

public class ObjSwitchByTrigger : MonoBehaviour
{
    public Transform _objCarrierLift;
    private bool _isInteractable=true;

    void OnTriggerEnter2D(Collider2D _col)
    {
        if(_col.gameObject.CompareTag("Gear")&&_isInteractable)
        {
            _objCarrierLift.gameObject.GetComponent<ObjectCarrier>()._liftStarted=true;
            _isInteractable=false;
        }
    }
}
