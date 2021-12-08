using UnityEngine;

public class AudioHandler : MonoBehaviour
{
	public static AudioHandler _instance { set; get;}

	//refrence to audiosource
	public AudioSource aud_button;
	public AudioSource aud_blade;
	public AudioSource aud_Hint;
	public AudioSource aud_blast;
	public AudioSource aud_star;
	public AudioSource aud_highScore;

	void Awake()
	{
		if (_instance != null){
			return;
		} 
		else {
			_instance = this;
		}
	}

	public void PlayButtonSound()
	{
		if (PlayerPrefs.GetInt ("GameAudio") == 0)
		{
			aud_button.Play ();
		}
	}

	public void PlayBladeSound()
	{
		if (PlayerPrefs.GetInt ("GameAudio") == 0)
		{
			aud_blade.Play ();
		}
	}

	public void PlayUseHintSound()
	{
		if (PlayerPrefs.GetInt ("GameAudio") == 0)
		{
			aud_Hint.Play ();
		}
	}
		
	public void PlayBlastSound()
	{
		if (PlayerPrefs.GetInt ("GameAudio") == 0)
		{
			aud_blast.Play ();
		}
	}

	public void PlayStarSound()
	{
		if (PlayerPrefs.GetInt ("GameAudio") == 0)
		{
			aud_star.Play ();
		}
	}
		
	public void PlayNewHighScoreSound()
	{
		if (PlayerPrefs.GetInt ("GameSFX") == 0)
		{
			aud_highScore.Play ();
		}
	}
}
