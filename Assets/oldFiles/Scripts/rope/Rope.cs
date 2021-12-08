using UnityEngine;

public class Rope : MonoBehaviour 
{
	public Rigidbody2D _hook;
	public int _totalLinks;
	public GameObject _linkPrefab;
	private Player _player;

	// Use this for initialization
	void Start () 
	{
		_player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		GenerateRope ();
	}
	
	void GenerateRope()
	{
		Rigidbody2D _previousLink = _hook;
	

		for (int i = 0; i < _totalLinks; i++) 
		{
			GameObject _link = Instantiate (_linkPrefab, transform);
			HingeJoint2D _joint = _link.GetComponent<HingeJoint2D> ();
			_joint.connectedBody = _previousLink;

			if (i < _totalLinks - 1) 
			{
				_previousLink = _link.GetComponent<Rigidbody2D> ();
			} 
			else 
			{
				_player.ConnectToRopeEnd (_link.GetComponent<Rigidbody2D> ());
			}
		}

		for (int i = 0; i < transform.childCount; i++)
		{
			if (i < transform.childCount - 1) 
			{
				transform.GetChild (i).GetComponent<Line> ()._nextPoint = transform.GetChild (i + 1).transform;
			}
			else 
			{
				transform.GetChild (i).GetComponent<Line> ()._nextPoint = _player.transform;
			}
		}
	}
}
