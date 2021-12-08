using UnityEngine;

public class Blade : MonoBehaviour 
{
	private bool _canCut=false;
	private Rigidbody2D _rb2d;
	private CircleCollider2D _collider;

	[Header("Blade")]
	public Transform _bladeTrailContainer;
	private int _trailSelector=0;
	private Transform _currentTrail;
	private Transform _lastTrail;
	//
	private Vector2 _bladeLastPos;
	public float _minBladeVelocity=0.1f;

	// Use this for initialization
	void Start ()
	{
		_rb2d = GetComponent<Rigidbody2D> ();
		_collider = GetComponent<CircleCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			StartCutting ();
		}
		else if (Input.GetMouseButtonUp (0)) 
		{
			StopCutting ();
		}

		if (_canCut) 
		{
			UpdateTheBlade ();
		}
	}

	void StartCutting()
	{
		_canCut = true;
		_bladeLastPos=Camera.main.ScreenToWorldPoint (Input.mousePosition);
		ChooseTheTrail ();
	}

	void StopCutting()
	{
		_canCut = false;
		_lastTrail = _currentTrail;
		_lastTrail.parent = null;
		_lastTrail.transform.SetParent (_bladeTrailContainer.transform);
		_lastTrail.gameObject.SetActive (false);
		_collider.enabled = false;
	}

	void UpdateTheBlade()
	{
		Vector2 _curBladePos=Camera.main.ScreenToWorldPoint (Input.mousePosition);
		_rb2d.position = _curBladePos;

		float _bladeVelocity = (_curBladePos - _bladeLastPos).magnitude * Time.deltaTime;

		if (_bladeVelocity > _minBladeVelocity)
		{
			_collider.enabled = true;
			_currentTrail.gameObject.SetActive (true);
		} 
		else 
		{
			_collider.enabled = false;
			_currentTrail.gameObject.SetActive (false);
		}

		_bladeLastPos = _curBladePos;
	}

	void ChooseTheTrail()
	{
		_currentTrail = _bladeTrailContainer.GetChild (_trailSelector).transform;
		_currentTrail.parent = null;
		_currentTrail.SetParent (this.transform);
		_currentTrail.transform.localPosition = Vector2.zero;
		if (_trailSelector < _bladeTrailContainer.childCount - 1) {
			_trailSelector++;
		} else {
			_trailSelector = 0;
		}
	}
}
