using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RopeController : MonoBehaviour
{
  public Transform _connectedTo;
  private LineRenderer _line;
 // public Transform _mainParent;

  void Start()
  {
      _line=GetComponent<LineRenderer>();
      _line.startWidth=0.13f;
  }

  void Update()
  {
    _line.SetPosition (0, transform.position);
    _line.SetPosition (1, _connectedTo.position);
  }
}
