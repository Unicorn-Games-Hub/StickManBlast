using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{
	public static GameController _instance { set; get; }

	//enum for creating the game states
	public enum _states
	{
		_ingame,
		_pauesd,
		_gameOver,
		_levelCompleted
	}
	public _states _gameStates;

	[Header("Refrences to game UI")]
	public GameObject _ingameUI;
	public GameObject _pauseUI;
	public GameObject _levelCompletedUI;

	[Header("Ingame ui info")]
	public Text _ingameTimerText;
	public float _ingameTimeCounter = 65f;

	[Header("Game Pause")]
	private bool _canPauseTheGame=false;
	private string _leftTime;
	public Text _leftTimeText;
	public bool _isPaused=false;
	//
	public Button _sfxButton;
	public Button _musicButton;

	[Header("Hint Info")]
	public Text _ingameHintText;
	private int _avaliableHints;
	private bool _canUseHints=true;

	[Header("Current level info")]
	private float _levelHighScore=0;
	private Scene _currentScene;
	private string _levelName;
	private string _curlvlNum;
	public int _levelnum=0;
	private int _nextLevelNum = 0;

	[Header("Level completed ui info")]
	public bool _isCompleted=false;
	public GameObject[] _stars;
	public Text _lvlCompletedscoreTitleText;
	public Text _lvlCompletedScoreText;
	public Text _lvlCompletedBesstScoreText;
	private bool _playStar1Sound=true;
	private bool _playStar2Sound=true;
	private bool _playStar3Sound=true;
	private int _previousBestScore;
	private int _tempScoreHolder=0;
	private int _scoreCounter=0;
	private int _countHolder;
	private int _lvlCompletedScore =1000;
	private int _scoreFromStar= 0;
	private int _bonusScore = 0;

	//
	public Transform _btnContainer;
	public GameObject _notificationUI;
	private bool _isUnlocked=false;
	
	public bool _canPlayGame=true;

	[Header("Destroyed Particle")]
	public GameObject _destroyParticle;

	//Character Unlocking
	private int _noOfTotalCharacter=20;

	[Header("Unlocking the new BG image")]
	public Image _bgImage;
	public Sprite[] _bgSprite;
	

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
		InitialAdjustment ();
	}

	void InitialAdjustment()
	{
		_ingameUI.SetActive (true);
		_pauseUI.SetActive (false);
		_levelCompletedUI.SetActive (false);
		_notificationUI.SetActive(false);
		_canPauseTheGame = true;
		UpdateGameHints ();
		//for finding out the level number from scene name
		FindOutTheCurrentLevelNumber ();
		AdjustTheAudioSprite ();
		_gameStates=_states._ingame;
		//function is called only once in the game
		ChangingTheBGsprite();
		SetInitialValueForCharUnlocking();
	}

	void FindOutTheCurrentLevelNumber()
	{
		_currentScene=SceneManager.GetActiveScene();
		_levelName = _currentScene.name;

		for (int i = 0; i < _levelName.Length; i++) 
		{
			if (i > 4) 
			{
				_curlvlNum += _levelName [i].ToString ();
			}
		}
		//converting the string to int
		_levelnum = int.Parse(_curlvlNum);
		_nextLevelNum = _levelnum + 1;
		_previousBestScore = PlayerPrefs.GetInt ("BestScore"+_levelnum);

		//
		if(GameAnalyticsHandler._instance!=null)
		{
			GameAnalyticsHandler._instance.TrackTheLevelStart(_levelnum);

			if(_levelnum==5)
			{
				GameAnalyticsHandler._instance.Level5Token();
			}
			else if(_levelnum==15)
			{
				GameAnalyticsHandler._instance.Level15Token();
			}
		}
	}
		
	// Update is called once per frame
	void Update () 
	{
		if (!_isCompleted&&!_isPaused) 
		{
			if (_ingameTimeCounter > 0) {
				string _timeHolder = "";
				_timeHolder += ((int)_ingameTimeCounter / 60).ToString ("00");
				_timeHolder += (_ingameTimeCounter % 60).ToString (":00");
				_ingameTimerText.text = _timeHolder;
				_ingameTimeCounter -= Time.deltaTime;
			} 
			else
			{
				GameOver ();
			}
		}


		if(_gameStates==_states._ingame)
		{
			if(Input.GetMouseButtonDown(0))
			{
				Vector2 _curmousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
				RaycastHit2D _hit2D=Physics2D.Raycast(_curmousePos,Vector2.zero);
				if(_hit2D.collider!=null)
				{
				if(_hit2D.collider.gameObject.CompareTag("Destroyable"))
				{
					GameObject _theParticle=Instantiate(_destroyParticle) as GameObject;
					_theParticle.transform.position=_curmousePos;
					_theParticle.GetComponent<ParticleSystem>().Play();
					_hit2D.collider.gameObject.SetActive(false);
				}
				}
			}
		}
	}
		
	#region pause and unpaused
	public void PauseTheGame()
	{
		if (_canPauseTheGame)
		{
			_isPaused = !_isPaused;
			if (_isPaused) {
				_pauseUI.SetActive (true);
				_pauseUI.GetComponent<Animator> ().SetBool ("_pause", true);
			} else {
				_pauseUI.SetActive (false);
			}
		}
		_leftTime = ((int)_ingameTimeCounter / 60).ToString ("00")+(_ingameTimeCounter % 60).ToString (":00");
		_leftTimeText.text = _leftTime;
		//playing button sound on clicking the button
		AudioHandler._instance.PlayButtonSound ();
		playerDuringPause ();
	}

	public void ResumeTheGame()
	{
		//_pauseUI.SetActive (false);
		//playing button sound on clicking the button
		AudioHandler._instance.PlayButtonSound ();
		StartCoroutine (HidePauseUI ());
	}

	IEnumerator HidePauseUI()
	{
		_pauseUI.GetComponent<Animator> ().SetBool ("_pause", false);
		yield return new WaitForSeconds (1f);
		_isPaused = false;
		playerDuringPause ();
		_pauseUI.SetActive (false);
	}

	void playerDuringPause()
	{
		_canPlayGame = !_isPaused;
		//GameObject.FindGameObjectWithTag ("Character").GetComponent<Character> ().CheckTheGamePauseCondition (_isPaused);
	}

	public void ControlTheSFX()
	{
		if (PlayerPrefs.GetInt ("GameAudio") == 0)
		{
			PlayerPrefs.SetInt ("GameAudio", 1);
		}
		else if(PlayerPrefs.GetInt ("GameAudio")==1)
		{
			PlayerPrefs.SetInt ("GameAudio", 0);
		}

		//playing button sound on clicking the button
		AudioHandler._instance.PlayButtonSound ();
		AdjustTheAudioSprite ();
	}

	public void ControlTheMusic()
	{
		if (PlayerPrefs.GetInt ("GameAudio") == 0)
		{
			PlayerPrefs.SetInt ("GameAudio", 1);
		}
		else if(PlayerPrefs.GetInt ("GameAudio")==1)
		{
			PlayerPrefs.SetInt ("GameAudio", 0);
		}

		if (BackgroundMusicHandler._instance != null)
		{
			BackgroundMusicHandler._instance.ControlTheBgMusic ();
		}
			
		//playing button sound on clicking the button
		AudioHandler._instance.PlayButtonSound ();
		AdjustTheAudioSprite ();
	}

	void AdjustTheAudioSprite()
	{
		//adjusting to toggle icons in pause menu
		if (PlayerPrefs.GetInt ("GameAudio") == 0) {
			_sfxButton.transform.GetChild (0).gameObject.SetActive (true);
		} else {
			_sfxButton.transform.GetChild (0).gameObject.SetActive (false);
		}
		if (PlayerPrefs.GetInt ("GameAudio") == 0)
		{
			_musicButton.transform.GetChild (0).gameObject.SetActive (true);
		} else {
			_musicButton.transform.GetChild (0).gameObject.SetActive (false);
		}
	}
	#endregion

	#region level completed ui
	//the method is called only when the player reaches the goal
	public void LevelCompleted()
	{
		_canPlayGame = false;
		AddScore ();
		//stoping player from falling
		//_player.bodyType=RigidbodyType2D.Static;
		//unlocking the nextLevel
		//check if the next level is already unlocked or not
		if(PlayerPrefs.GetInt("UnlockedLevels")<_nextLevelNum)
		{
			PlayerPrefs.SetInt("UnlockedLevels",_nextLevelNum);
		}
		TimeToUnlockTheCharacter();
		TimeToUnlockBackground();
	}
		
	void AddScore()
	{
		_canPauseTheGame = false;
		_ingameUI.SetActive (false);
		_levelCompletedUI.SetActive (true);
		_lvlCompletedBesstScoreText.enabled=false;
		//
		_levelCompletedUI.GetComponent<Animator> ().SetBool ("_completed", true);
		_isCompleted = true;
		_tempScoreHolder = Mathf.RoundToInt (_ingameTimeCounter);

		//deactivating all the stars at first and enable them according to the score 
		for (int i = 0; i < _stars.Length; i++) 
		{
			_stars [i].SetActive (false);
		}
			
		if (_tempScoreHolder <= 20) {
			//1 star only
			_scoreFromStar=500;
		} else if (_tempScoreHolder > 20 && _tempScoreHolder < 40) {
			//2 stars
			_scoreFromStar=1000;
		} else if (_tempScoreHolder >= 40) {
			//all 3
			_scoreFromStar=1500;
		}
			
		//calculating the bonus score
		_bonusScore = Mathf.RoundToInt (_ingameTimeCounter) * 10;
		_scoreCounter = _bonusScore + _lvlCompletedScore+_scoreFromStar;
		StartCoroutine(StartScoreAnimation());

		if(GameAnalyticsHandler._instance!=null)
		{
			GameAnalyticsHandler._instance.TrackTheLevelComplete(_levelnum,_scoreCounter);
		}
		//
		ControlTheButtonInteraction(false);
	}
		
	IEnumerator StartScoreAnimation ()
	{
		yield return new WaitForSeconds (0.5f);
		while (_countHolder < _scoreCounter)
		{
			yield return new WaitForSeconds(0.02f);
			_lvlCompletedScoreText.text=_countHolder.ToString();

			_lvlCompletedscoreTitleText.text = "Score :";
			PlayStarAnimation (_countHolder);
			_countHolder+=50;
		}

		if(_scoreCounter>PlayerPrefs.GetInt ("BestScore"+_levelnum))
		{
			_lvlCompletedBesstScoreText.text="new best";
			PlayerPrefs.SetInt ("BestScore"+_levelnum, _countHolder);
		}
		else
		{
			_lvlCompletedBesstScoreText.text= "best : "+PlayerPrefs.GetInt ("BestScore"+_levelnum).ToString();
		}
		_lvlCompletedBesstScoreText.enabled=true;
		//
		ControlTheButtonInteraction(true);
		
		if(_isUnlocked)
		{
			PlayNotificationAnimation();
		}
	}

	void ControlTheButtonInteraction(bool _theInput)
	{
		for(int i=0;i<_btnContainer.childCount;i++)
		{
			_btnContainer.GetChild(i).gameObject.GetComponent<Button>().interactable=_theInput;
		}

	}

	void PlayStarAnimation(int _starScore)
	{
		if (_countHolder<=1690) 
		{
			_stars [0].SetActive (true);
			if (_playStar1Sound) {
				StartCoroutine (PlayStarSmashSoundEffect ());
				_playStar1Sound = false;
			}
			//save 1 star
			if (PlayerPrefs.GetInt ("StarsEarned" + _levelnum)<1) 
			{
				PlayerPrefs.SetInt ("StarsEarned" + _levelnum, 1);
			}
		} else if (_countHolder > 1690 && _countHolder < 2900) 
		{
			_stars [0].SetActive (true);
			_stars [1].SetActive (true);
			if (_playStar2Sound) {
				StartCoroutine (PlayStarSmashSoundEffect ());
				_playStar2Sound = false;
			}
			//save 2 star
			if (PlayerPrefs.GetInt ("StarsEarned" + _levelnum)<2) 
			{
				PlayerPrefs.SetInt ("StarsEarned" + _levelnum, 2);
			}
		} else if (_countHolder >= 2900) 
		{
			_stars [0].SetActive (true);
			_stars [1].SetActive (true);
			_stars [2].SetActive (true);
			if (_playStar3Sound) {
				StartCoroutine (PlayStarSmashSoundEffect ());
				_playStar3Sound = false;
			}
			if (PlayerPrefs.GetInt ("StarsEarned" + _levelnum)<3) 
			{
				PlayerPrefs.SetInt ("StarsEarned" + _levelnum, 3);
			}
		}
	}

	IEnumerator PlayStarSmashSoundEffect ()
	{
		yield return new WaitForSeconds (0.2f);
		AudioHandler._instance.PlayStarSound ();
	}
	#endregion

	public void GameOver()
	{
		SceneManager.LoadScene (_currentScene.name);
	}

	#region hints
	public void UseHint()
	{
		_avaliableHints = PlayerPrefs.GetInt ("TotalHints");

		if (_canUseHints) 
		{
			if (_avaliableHints > 0) 
			{
				_avaliableHints--;
				PlayerPrefs.SetInt ("TotalHints", _avaliableHints);
				ShowHints ();
				//playing use hint sound and enable hint
				AudioHandler._instance.PlayUseHintSound ();
			} else {
				AudioHandler._instance.PlayButtonSound ();
				GetHintsUsingRewardVideoAds ();
			}
			_canUseHints = false;
		}
		UpdateGameHints ();
	}
		
		
	void UpdateGameHints()
	{
		_ingameHintText.text = PlayerPrefs.GetInt ("TotalHints").ToString ();
	}

	void GetHintsUsingRewardVideoAds()
	{
		//AdmobController._instance.ShowRewardVideoAds();
	}
		
	void AddHintsToTheGame(int _noOfHints)
	{
		PlayerPrefs.SetInt ("TotalHints", PlayerPrefs.GetInt ("TotalHints") + _noOfHints);
		UpdateGameHints ();
	}

	void ShowHints()
	{
		//starting hint animation form here

	}
	#endregion

	#region button control handler
	public void Home()
	{
		//playing button sound on clicking the button
		AudioHandler._instance.PlayButtonSound ();
		StartCoroutine (WaitBeforeLoadingHome ());
	}

	IEnumerator WaitBeforeLoadingHome ()
	{
		yield return new WaitForSeconds (0.3f);
		SceneManager.LoadScene ("MainMenu");
	}

	public void RestartLevel()
	{
		//playing button sound on clicking the button
		AudioHandler._instance.PlayButtonSound ();
		StartCoroutine (WaitBeforeRestartingLevel ());
	}
	IEnumerator WaitBeforeRestartingLevel ()
	{
		yield return new WaitForSeconds (0.3f);
		SceneManager.LoadScene (_levelName);
	}

	public void NextLevel()
	{
		//playing button sound on clicking the button
		AudioHandler._instance.PlayButtonSound ();
		StartCoroutine (WaitBeforeLoadingNextLevel ());
	}

	IEnumerator WaitBeforeLoadingNextLevel ()
	{
		yield return new WaitForSeconds (0.3f);
		SceneManager.LoadScene ("Level" + _nextLevelNum);
	}
	#endregion

	#region  character unlocking
	void SetInitialValueForCharUnlocking()
	{
		if(PlayerPrefs.GetInt("SetCharValueFirst")==0)
		{
			PlayerPrefs.SetInt("CharUnlockCounter",5);
			PlayerPrefs.SetInt("SetCharValueFirst",1);
			//setting bg image value also
			PlayerPrefs.SetInt("BgUnlockCounter",10);
		}
	}
	void TimeToUnlockTheCharacter()
	{
		if(PlayerPrefs.GetInt("CharUnlockCounter")==_levelnum)
		{
			if(PlayerPrefs.GetInt("_charIdCounter")<_noOfTotalCharacter)
			{
				PlayerPrefs.SetInt("_charIdCounter",PlayerPrefs.GetInt("_charIdCounter")+1);
			}
			PlayerPrefs.SetInt("Unlocked"+PlayerPrefs.GetInt("_charIdCounter"),1);
			PlayerPrefs.SetInt("CharUnlockCounter",PlayerPrefs.GetInt("CharUnlockCounter")+5);
			_isUnlocked=true;
		}	
	}
	#endregion

	#region unlocking new background
	void ChangingTheBGsprite()
	{
		_bgImage.sprite=_bgSprite[PlayerPrefs.GetInt("CurrentBGsprite")];
	}
	
	void TimeToUnlockBackground()
	{
		if(PlayerPrefs.GetInt("BgUnlockCounter")==_levelnum)
		{
			if(PlayerPrefs.GetInt("UnlockedBgCount")<_bgSprite.Length)
			{
				PlayerPrefs.SetInt("UnlockedBgCount",PlayerPrefs.GetInt("UnlockedBgCount")+1);
			}
			PlayerPrefs.SetInt("bgImageUnlocked"+PlayerPrefs.GetInt("UnlockedBgCount"),1);
			PlayerPrefs.SetInt("BgUnlockCounter",PlayerPrefs.GetInt("BgUnlockCounter")+10);
		}
	}
	#endregion

	#region notification
	void PlayNotificationAnimation()
	{
		if(PlayerPrefs.GetInt("GameAudio")==0)
		{
			//play unlock sound from here
		}
		_notificationUI.SetActive(true);
		ShowNotificationUI();
	}

	void ShowNotificationUI()
	{
		_notificationUI.GetComponent<Animator>().SetBool("showNotification",true);
	}

	public void ChooseUnlockedCharacter()
	{
		if(PlayerPrefs.GetInt("GameAudio")==0)
		{
			//play select sound from here
		}
		//select the unlocked character
		StartCoroutine(HideNotificationUI());
		//select the unlocked new character
		PlayerPrefs.SetInt("CurrentCharacter",PlayerPrefs.GetInt("_charIdCounter"));
	}

	IEnumerator HideNotificationUI()
	{
		_notificationUI.GetComponent<Animator>().SetBool("showNotification",false);
		yield return new WaitForSeconds(1f);
		_notificationUI.SetActive(false);
	}
	#endregion
}