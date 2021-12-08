using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelManager : MonoBehaviour {
	LockedLevels levelIndicator;

	public string levelName;

	int newNum;
	void Start () {
		Time.timeScale = 1;

		levelIndicator = FindObjectOfType<LockedLevels> ();
	}
	void Update () {
	
	}
	public  void LoadLevel(){
		
		//SceneManager.LoadScene (EventSystem.current.currentSelectedGameObject.name);
	
	}
	public void MianMenuLoad()
	{
		SceneManager.LoadScene ("MainMenuUI");
	}
	public void Restart()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		Time.timeScale= 1;
	}
	public void Paused()
	{
		Time.timeScale = 0;
	}
	public void Resume()
	{
		Time.timeScale = 1;
	}
	public void NextLevel(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		Time.timeScale = 1;

	}
	public void PreviousLevel(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 1);
		Time.timeScale = 1;

	}

}
