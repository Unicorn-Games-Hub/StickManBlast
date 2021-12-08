using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterShopHandler : MonoBehaviour 
{
	public static CharacterShopHandler _instance{set;get;}
	public Transform _indicatorContainer;
	public GameObject _indicatorPrefab;
	private int _characterSelector=0;

	public Button _nextButton;
	public Button _previousButton;
	public Button _choosePlayerButton;

	[System.Serializable]
	public class _characterInfo
	{
		public string _characterName;
		public string _unlockInfo;
		public int _charID;
	}

	public List<_characterInfo> _gameCharInfo = new List<_characterInfo> ();
	private string _nameOfCharacter;
	private string _characterDetail;
	private int _characterID;

	public Text _characterNameText;
	public Text _unlockInfoText;
	public Text _selectionText;

	//for changing character by swiping
	private Vector2 _initialTouchPoint,_finalTouchPoint;
	private float _swipeXvalue,_swipeYvalue;
	private int _horizontalValue;
	private bool _canSwipe=false;

	[Header("Locked Character Settings")]
	public Color _lockedCharCol;

	//
	public GameObject _errorButton;
	private AudioSource _aud;
	public AudioClip[] _charSelectSound;

	//for error message display
	private bool _showError=true;
	public GameObject _lockedIcon;

	void Awake()
	{
		if(_instance!=null)
		{
			return;
		}
		else
		{
			_instance=this;
		}
	}
	
	void Start()
	{
		_errorButton.SetActive(false);
		_lockedIcon.SetActive(false);
		_characterSelector=PlayerPrefs.GetInt ("CurrentCharacter");
		//for always unlocking the first character
		if(PlayerPrefs.GetInt("Unlocked"+0)==0)
		{
			PlayerPrefs.SetInt("Unlocked"+0,1);
		}
		for (int i = 0; i < _gameCharInfo.Count; i++) 
		{
			GameObject _indicator = Instantiate (_indicatorPrefab)as GameObject;
			_indicator.transform.SetParent (_indicatorContainer.transform, false);
		}
		AdjustTheCharacterIndicatorSprite ();
		_aud=GetComponent<AudioSource>();
	}

	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			_initialTouchPoint=Camera.main.ScreenToViewportPoint(Input.mousePosition);
		}
		else if(Input.GetMouseButtonUp(0))
		{
			_finalTouchPoint=Camera.main.ScreenToViewportPoint(Input.mousePosition);
			CalculateTheDirectionOfSwipe();
		}
	}

	void CalculateTheDirectionOfSwipe()
	{
		_swipeXvalue=_initialTouchPoint.x-_finalTouchPoint.x;
		_swipeYvalue=_initialTouchPoint.y-_finalTouchPoint.y;

		_initialTouchPoint.x=-1;

		if(Mathf.Abs(_swipeXvalue)>Mathf.Abs(_swipeYvalue))
		{
			_horizontalValue=_swipeXvalue>0?1:-1;
			_canSwipe=true;
		}

		if(_horizontalValue!=0&&_canSwipe)
		{
			if(_horizontalValue>0)
			{
				Next();
			}
			else 
			{
				Previous();
			}
			_canSwipe=false;
		}
	}

	public void ResetSelector()
	{
		_characterSelector=PlayerPrefs.GetInt ("CurrentCharacter");
		AdjustTheCharacterIndicatorSprite();
		_lockedIcon.SetActive(false);
		_showError=true;
	}

	public void Next()
	{
		if (_characterSelector < _gameCharInfo.Count - 1) {
			_characterSelector++;
			AdjustTheCharacterIndicatorSprite ();
		} 
		PlayButtonSound();
	}

	public void Previous()
	{
		if (_characterSelector > 0) {
			_characterSelector--;
			AdjustTheCharacterIndicatorSprite ();
		}
		PlayButtonSound();
	}

	void AdjustTheCharacterIndicatorSprite()
	{
		for (int i = 0; i < _indicatorContainer.childCount; i++) 
		{
			_indicatorContainer.GetChild (i).GetChild(0).gameObject.SetActive (false);
		}
		_indicatorContainer.GetChild (_characterSelector).GetChild(0).gameObject.SetActive (true);

		if (_characterSelector == 0) {
			_previousButton.interactable = false;
		} else {
			_previousButton.interactable=true;
		}

		if (_characterSelector == _gameCharInfo.Count - 1) {
			_nextButton.interactable = false;
		} else {
			_nextButton.interactable=true;
		}
		UpdateTheCharacterInformation ();
	}

	void UpdateTheCharacterInformation()
	{
		_nameOfCharacter = _gameCharInfo [_characterSelector]._characterName;
		_characterDetail = _gameCharInfo [_characterSelector]._unlockInfo;
		_characterID = _gameCharInfo [_characterSelector]._charID;
		//
		_characterNameText.text = _nameOfCharacter;
		_unlockInfoText.text = _characterDetail;
		_choosePlayerButton.interactable = true;
		//
		if (PlayerPrefs.GetInt ("Unlocked" + _characterID) == 1)
		{
			_unlockInfoText.text = "";
			_choosePlayerButton.interactable = true;
			if (PlayerPrefs.GetInt ("CurrentCharacter") == _characterID) 
			{
				_selectionText.text = "Selected";

			} else
			{
				_selectionText.text = "Select";
			}

			LetsGoForColor(Color.white);
			_errorButton.SetActive(false);
		} 
		else 
		{
			_choosePlayerButton.interactable = false;
			_selectionText.text = "Locked";
			LetsGoForColor(_lockedCharCol);
			_errorButton.SetActive(true);
		}
		Character._instance.IdentifyTheSelectedCharacter (_characterSelector);
	}

	void LetsGoForColor(Color _newCol)
	{
		if(Character._instance!=null)
		{
			Character._instance.LetsChangeTheCharacterColor(_newCol);
		}
	}

	public void ChooseTheCharacter()
	{
		if (PlayerPrefs.GetInt ("Unlocked" + _characterID) == 1)
		{
			PlayerPrefs.SetInt ("CurrentCharacter", _characterID);
		}
		UpdateTheCharacterInformation ();
		PlaySelectSound();
	}

	//error message
	public void ShowCharLockedMessage()
	{
		if(_showError)
		{
			StartCoroutine(ShowLockedInfo());
			_showError=false;
		}
	}

	IEnumerator ShowLockedInfo()
	{
		_lockedIcon.SetActive(true);
		yield return new WaitForSeconds(1.5f);
		_lockedIcon.SetActive(false);
		_showError=true;
	}

	void PlaySelectSound()
	{
		if(PlayerPrefs.GetInt("GameAudio")==0)
		{
			_aud.clip=_charSelectSound[0];
		}
	}

	 void PlayErrorSound()
	{
		if(PlayerPrefs.GetInt("GameAudio")==0)
		{
			_aud.clip=_charSelectSound[1];
		}
	}

	void PlayButtonSound()
	{
		if(MenuController._instance!=null)
		{
			MenuController._instance.PlayButtonSound();
		}
	}
}
