using UnityEngine;

public class Player : MonoBehaviour 
{
	public float _distanceFromRopeEnd = -5f;


	public void ConnectToRopeEnd(Rigidbody2D _ropeEnd)
	{
		HingeJoint2D _playerJoint = gameObject.AddComponent<HingeJoint2D> ();
		_playerJoint.autoConfigureConnectedAnchor = false;
		_playerJoint.connectedBody = _ropeEnd;
		_playerJoint.anchor = Vector2.zero;
		_playerJoint.connectedAnchor = new Vector2 (0f, _distanceFromRopeEnd);
	
	}
		
}
