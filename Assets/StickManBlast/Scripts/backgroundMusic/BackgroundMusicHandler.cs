using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class BackgroundMusicHandler : MonoBehaviour 
{
	public static BackgroundMusicHandler _instance{ set; get;}
	private AudioSource aud_bg;

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
		DontDestroyOnLoad (this);

		if (FindObjectsOfType (GetType ()).Length > 1) 
		{
			Destroy (gameObject);
		}

		ControlTheBgMusic ();
	}

	public void ControlTheBgMusic()
	{
		aud_bg = GetComponent<AudioSource> ();
		if (PlayerPrefs.GetInt ("GameAudio") == 0) {
			aud_bg.mute = false;
		} else 
		{
			aud_bg.mute = true;
		}
	}
}
