using UnityEngine;

public class Line : MonoBehaviour 
{
	public Transform _nextPoint;
	private LineRenderer _line;

	private GameObject _connectedBody;

	public bool _startFading=false;

	[Header("Fading the color of rope")]
	private Gradient _tempGradient;
	private float _fadeSpeed=1.5f;
	private float _currentAlpha=1f;

	private Gradient _lineGradient;
	private GradientAlphaKey[] _alphaKeys;


	void Start()
	{
		_line = GetComponent<LineRenderer> ();
		_connectedBody = GetComponent<HingeJoint2D> ().connectedBody.gameObject;
		_lineGradient = _line.colorGradient;
		_alphaKeys = _lineGradient.alphaKeys;
	}
		
	void Update()
	{
		_line.SetPosition (0, transform.position);
		_line.SetPosition (1, _nextPoint.position);
	
		if (_startFading) 
		{
			if (_currentAlpha > 0)
			{
				_currentAlpha -= Time.deltaTime / _fadeSpeed;

				_alphaKeys [0].alpha = _currentAlpha;
				_alphaKeys [1].alpha = _currentAlpha;

				_lineGradient.alphaKeys = _alphaKeys;

				_line.colorGradient = _lineGradient;
			} 
			else 
			{
				this.gameObject.SetActive (false);
			}
		}
	}
}
