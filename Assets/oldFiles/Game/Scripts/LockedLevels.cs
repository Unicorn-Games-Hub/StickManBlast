using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockedLevels : MonoBehaviour {
	
	public GameObject Locked;
	public GameObject Unlocked;
	public string levelname;
	void Awake(){
		Debug.Log(levelname + "---"+ PlayerPrefs.GetInt (levelname));
	}
	void  Start (){
		
		//Locked.SetActive (true);
		//Unlocked.SetActive (false);
		//GetComponent<Button> ().interactable = false;
		UnLockLevels ();
	

	}
	private void  UnLockLevels (){
		//Debug.Log ("level unlocked");
		if (PlayerPrefs.GetInt (levelname) == 1) {
			Locked.SetActive (false);
			Unlocked.SetActive (true);
			GetComponent<Button> ().interactable = true;

		} else {
			Locked.SetActive (true);
			Unlocked.SetActive (false);
			GetComponent<Button> ().interactable = false;
		}
		
  }
	void Update(){
		if (Input.GetKeyDown (KeyCode.Delete)) {
			PlayerPrefs.DeleteAll ();
		}


		}

}

