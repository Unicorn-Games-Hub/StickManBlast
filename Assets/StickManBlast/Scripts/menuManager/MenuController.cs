using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	public static MenuController _instance{ set; get; }
	public GameObject _menuUI;
	public GameObject _levelUI;
	public GameObject _characterShopUI;
	public GameObject _bgShopUI;
	public AudioSource aud_Button;

	//audio sprite
	public Sprite[] _musicSprite;
	public Button _muiscButton;

	//public Sprite[] _sfxSprite;
	//public Button _sfxButton;

	[Header("Dynamic background")]
	public Image _bgImage;
	public Sprite[] _bgSprites;
	private int _bgCounter;
	
	void Awake()
	{
		if (_instance != null) {
			return;
		} else {
			_instance = this;
		}
	}

	// Use this for initialization
	void Start () 
	{
		_menuUI.SetActive (true);
		_levelUI.SetActive (false);
		_bgShopUI.SetActive(false);
		_characterShopUI.SetActive (false);
		ChangeTheAudioSprite();
		if (PlayerPrefs.GetInt ("ForTheFirstTime") == 0) 
		{
			PlayerPrefs.SetInt ("TotalHints", 5);
			//for unlocking the first character always when game starts
			//PlayerPrefs.SetInt ("Unlocked" + 0, 1);
			PlayerPrefs.SetInt ("ForTheFirstTime", 1);
		}
		ChangeBackgroundImage();
	}

	//can be called only at start and from backgroundspriteshop
	public void ChangeBackgroundImage()
	{
		_bgCounter=PlayerPrefs.GetInt("CurrentBGsprite");
       	_bgImage.sprite=_bgSprites[_bgCounter];
	}

	public void Play()
	{
		_menuUI.SetActive (false);
		_levelUI.SetActive (true);
		PlayButtonSound ();
	}

	public void BackFromLevelMenu()
	{
		_menuUI.SetActive (true);
		_levelUI.SetActive (false);
		PlayButtonSound ();
	}

	#region settings

	public void TheAudioSetting()
	{
		if(PlayerPrefs.GetInt("GameAudio")==0)
		{
			PlayerPrefs.SetInt("GameAudio",1);
		}
		else if(PlayerPrefs.GetInt("GameAudio")==1)
		{
			PlayerPrefs.SetInt("GameAudio",0);
		}
		ChangeTheAudioSprite();
		PlayButtonSound();
		ControlTheBgMusic();
	}

	void ChangeTheAudioSprite()
	{
		if (PlayerPrefs.GetInt ("GameAudio") == 0) {
			_muiscButton.GetComponent<Image>().sprite=_musicSprite[0];
		}
		 else 
		{
			_muiscButton.GetComponent<Image>().sprite=_musicSprite[1];
		}
	}

	public void ControlTheBgMusic()
	{
		if (BackgroundMusicHandler._instance != null) {
			BackgroundMusicHandler._instance.ControlTheBgMusic ();
		}
	}

	#endregion 

	public void PlayButtonSound()
	{
		if (PlayerPrefs.GetInt ("GameAudio") == 0) {
			aud_Button.Play ();
		}
	}

	public void GoToCharacterShop()
	{
		PlayButtonSound();
		_menuUI.SetActive (false);
		_characterShopUI.SetActive (true);
	}

	public void CloseCharacterShop()
	{	
		PlayButtonSound();
		CharacterShopHandler._instance.ResetSelector();
		Character._instance.IdentifyTheSelectedCharacter(PlayerPrefs.GetInt ("CurrentCharacter"));
		Character._instance.LetsChangeTheCharacterColor(Color.white);
		_characterShopUI.SetActive (false);
		_menuUI.SetActive (true);
	}

	public void GoToBgShop()
	{
		PlayButtonSound();
		_menuUI.SetActive(false);
		_bgShopUI.SetActive(true);
	}

	public void BackFromBgShop()
	{
		PlayButtonSound();
		_bgShopUI.SetActive(false);
		_menuUI.SetActive(true);
	}

}
