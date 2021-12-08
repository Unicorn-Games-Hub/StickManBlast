using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour 
{
	public static Character _instance{ set; get; }

	public List<Rigidbody2D> _playerBodyParts = new List<Rigidbody2D> ();

	[System.Serializable]
	public class _characterBody
	{
		public string _characterName;
		public Sprite _normal;
		public Sprite _blink;
		public Sprite _painful;
		public Sprite _dead;

		public Sprite[] _parts;
	}
	public List<_characterBody> _charBodySpriteContainer = new List<_characterBody> ();

	private int _characterID;
	private int _bodyPartCount=0;

	private int _curSelectedChar;

	public bool _canBlink=true;


	//for body part scattering
	private float _dirX;
	private float _dirY;
	private float _angle;
	private float _shatteringForce=5f;


	//for blood splatter effect
	public GameObject _bloodParticlePrefab;

	
	void Awake()
	{
		if (_instance != null) {
			return;
		} else {
			_instance = this;
		}
	}

	void Start()
	{
		_curSelectedChar = PlayerPrefs.GetInt ("CurrentCharacter");
		IdentifyTheSelectedCharacter (_curSelectedChar);
		CharacterEyeAnimation ();
	}

	public void IdentifyTheSelectedCharacter(int _charNum)
	{
		_curSelectedChar = _charNum;
		for (int i = 0; i < _playerBodyParts.Count; i++) 
		{
			_playerBodyParts [i].GetComponent<SpriteRenderer> ().sprite = _charBodySpriteContainer [_charNum]._parts [i];
		}
	}

	void CharacterEyeAnimation()
	{
		StartCoroutine (BlinkTheEye ());
	}

	private float _blinkTimer=0f;

	IEnumerator BlinkTheEye()
	{
		if (_canBlink) 
		{
			while (_blinkTimer <1f) 
			{
				_blinkTimer += 0.1f;
				yield return new WaitForSeconds (0.3f);
				_playerBodyParts [0].GetComponent<SpriteRenderer> ().sprite = _charBodySpriteContainer [_curSelectedChar]._normal;
				if (_blinkTimer >= 1)
				{
					_blinkTimer = 0f;
					_playerBodyParts [0].GetComponent<SpriteRenderer> ().sprite = _charBodySpriteContainer [_curSelectedChar]._blink;
				}
			}
		}else
		{
			yield return null;
		}
	}

	void CharacterFace()
	{
		_canBlink = false;
	}

	void ChangeFaceToSuffocate()
	{
		_canBlink=false;
		_playerBodyParts [0].GetComponent<SpriteRenderer> ().sprite=_charBodySpriteContainer[0]._painful;
	}

	void ChangeFaceToDead()
	{
		_canBlink=false;
		_playerBodyParts [0].GetComponent<SpriteRenderer> ().sprite=_charBodySpriteContainer[0]._dead;
	}

	public void CheckTheGamePauseCondition(bool _freezeIt)
	{
		if (_freezeIt)
		{
			FreezeThePlayer (RigidbodyType2D.Static);
		}
		else
		{
			FreezeThePlayer (RigidbodyType2D.Dynamic);
		}
	}

	void FreezeThePlayer(RigidbodyType2D _bodyType)
	{
		foreach (Rigidbody2D _rbd in _playerBodyParts) 
		{
			_rbd.bodyType = _bodyType;
		}
	}

	#region bodyparts scattering
	public void ScatterPlayerBodyParts()
	{
		for(int i=0;i<_playerBodyParts.Count;i++)
		{
			_playerBodyParts[i].gameObject.GetComponent<HingeJoint2D>().enabled=false;
			_angle=_playerBodyParts[i].transform.eulerAngles.z+90f;
			_dirX=Mathf.Cos(_angle*Mathf.Deg2Rad);
			_dirY=Mathf.Sin(_angle*Mathf.Deg2Rad);
			_playerBodyParts[i].velocity=new Vector2(_dirX,_dirY)*_shatteringForce;
			_playerBodyParts[i].gameObject.tag="Untagged";
		}
		transform.GetChild(0).gameObject.tag="Untagged";
		ChangeFaceToDead();

		GameObject _blood =Instantiate(_bloodParticlePrefab) as GameObject;
		_blood.transform.position=new Vector2(_playerBodyParts[0].transform.position.x,_playerBodyParts[0].transform.position.y);
		_blood.GetComponent<ParticleSystem>().Play();
	}
	#endregion

	#region burning body by laser
	public void BurnTheBodyPart()
	{
		for(int i=0;i<_playerBodyParts.Count;i++)
		{
			_playerBodyParts[i].gameObject.GetComponent<SpriteRenderer>().color=new Color(0f,0f,0f,1f);
		}
	}
	#endregion

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			BurnTheBodyPart();
		}
	}

	public void LetsChangeTheCharacterColor(Color _theCharCol)
	{
		for(int i=0;i<_playerBodyParts.Count;i++)
		{
			_playerBodyParts[i].gameObject.GetComponent<SpriteRenderer>().color=_theCharCol;
		}
	}
}
