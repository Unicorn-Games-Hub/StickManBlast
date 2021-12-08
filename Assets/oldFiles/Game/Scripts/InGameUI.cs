using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InGameUI : MonoBehaviour 
{
	public GameObject All;
	public GameObject LevelFinish;
	SoundController mySound;
	public GameObject pause;
	public bool GameComplete;

	//-------------------------------//
	private Scene currentScene;
	private string sceneName;
	private string num;
	public int levelNumber;
	private List<char> curLevel=new List<char>();

	void Start () 
	{
		Time.timeScale = 1;
		All.SetActive (true);
		LevelFinish.SetActive (false);
		mySound = FindObjectOfType<SoundController> ();
		//------------------------//
		currentScene=SceneManager.GetActiveScene();
		sceneName=currentScene.name;
		//------------------------//
		foreach(char c in sceneName)
		{
			curLevel.Add(c);
		}

		switch(curLevel.Count)
		{
			case 6:
				num=curLevel[5].ToString();
			break;

			case 7:
			num=(curLevel[5]+""+curLevel[6]).ToString();
			break;

			case 8:
			num=(curLevel[5]+""+curLevel[6]+""+curLevel[7]).ToString();
			break;
		}	
		levelNumber=System.Convert.ToInt32(num);

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag.Contains("Over"))
		{
			GameComplete = true;
			mySound.PlayFallSoundEffect();
			LevelFinish.SetActive(true);
			All.SetActive (false);
			pause.SetActive (false);
			//---------------------------------------------------------------------------------//
			if(PlayerPrefs.GetInt("MaxLevelUnlocked")<=levelNumber)
			{
				PlayerPrefs.SetInt("MaxLevelUnlocked",PlayerPrefs.GetInt("MaxLevelUnlocked")+1);
			}
		
		}
	}
	public  void PauseButtonScript()
	{
		Time.timeScale = 0;
	}
	public void Home()
	{
		SceneManager.LoadScene ("MainMenu");
		mySound.PlayButtonSoundEffect();
	}

	public void GameFinishScript()
	{
		Time.timeScale = 0;
	}
	public void LevelFailedScript()
	{
		Time.timeScale = 0;
	}

	public void Restart()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		mySound.PlayButtonSoundEffect();
		Time.timeScale= 1;
	}
	public void Resume()
	{
		Time.timeScale = 1;
	}
	public void NextLevel()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		Time.timeScale = 1;
		mySound.PlayButtonSoundEffect();
	}

	public void PreviousLevel()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 1);
		Time.timeScale = 1;
	}
	public void Help()
	{
		Time.timeScale = 0;
	}
	public void Help_Back()
	{
		Time.timeScale = 1;
	}

	public void LoadLevels()
	{
		mySound.PlayButtonSoundEffect();
		SceneManager.LoadScene ("LevelMenu");
	
	}
}
