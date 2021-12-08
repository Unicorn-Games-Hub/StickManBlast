using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

	public AudioSource fall;
	public AudioSource gameBg;
	public  AudioSource tap;
	public AudioSource button;
	public static SoundController instance;

	void Start ()
	 {
		if (!instance) {
			DontDestroyOnLoad (this.gameObject);
			instance = this;
		} else {
			DestroyImmediate (this.gameObject);

		}
	}
	
	public void PlayButtonSoundEffect()
	{
		if(PlayerPrefs.GetInt("game_Sound")==0)
		{
			button.Play ();
		}
	}
	public void PlayTapSoundEffect()
	{
		if(PlayerPrefs.GetInt("game_Sound")==0)
		{
			tap.Play();
		}
	}
	public void PlayFallSoundEffect()
	{
		if(PlayerPrefs.GetInt("game_Sound")==0)
		{
			fall.Play();
		}
	}



	public void PlayBackgroundMusic()
	{
		if(PlayerPrefs.GetInt("game_Sound")==0)
		{
			gameBg.mute=false;
		}
		else
		{
			gameBg.mute=true;
		}
	}
}
