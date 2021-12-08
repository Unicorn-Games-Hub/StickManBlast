using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject levels;

	void Start () 
	{
		if (PlayerPrefs.GetInt ("main") == 1) {
			mainMenu.SetActive (false);
			levels.SetActive (true);
			PlayerPrefs.SetInt ("main", 0);
		}
	}
	

	void Update () 
	{
		
	}
}
