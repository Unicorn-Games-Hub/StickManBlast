using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
	public Transform _stageContainer;
	private List<Transform> _lvlBtn = new List<Transform> ();
	private int _lvlNumCounter=1;

	private int _stageCounter;
	public Button[] _stageChangeButton;
	public float _smoothness = 5f;

	private float _stagespos;


	[Header("Stage Indicator")]
	public Transform _stageIndicatorContainer;
	public GameObject _indicatorPrefab;

	//for changing the level stage by swiping
	private Vector2 _initialTouchPoint,_finalTouchPoint;
	private float _swipeXvalue,_swipeYvalue;
	private int _horizontalValue;
	private bool _canSwipe=false;

	void Start () 
	{
		//for making unlocked levels unclickable
		if(PlayerPrefs.GetInt("UnlockedLevels")==0)
		{
			PlayerPrefs.SetInt("UnlockedLevels",1);
		}

		for (int i = 0; i < _stageContainer.childCount; i++) 
		{
			for (int j = 0; j < _stageContainer.GetChild (i).GetChild(0).childCount; j++) 
			{
				_lvlBtn.Add(_stageContainer.GetChild(i).GetChild(0).GetChild(j).transform);
			}
		}

		foreach (Transform t in _lvlBtn)
		{
			t.GetComponent<LevelButton> ()._levelNumber = _lvlNumCounter;
			t.GetChild (0).GetComponent<Text> ().text = t.GetComponent<LevelButton> ()._levelNumber.ToString ();

			t.GetComponent<Button> ().onClick.AddListener (delegate() {
				LoadTheLevel (t.GetComponent<LevelButton> ()._levelNumber);
			});

			//level lock logic from here
			if(t.GetComponent<LevelButton>()._levelNumber<=PlayerPrefs.GetInt("UnlockedLevels"))
			{
				t.GetComponent<Button>().interactable=true;
			}
			else
			{
				t.GetComponent<Button>().interactable=false;
			}

			switch(PlayerPrefs.GetInt ("StarsEarned"+_lvlNumCounter))
			{
			case 0:
				//no value found
				break;
			case 1:
				t.GetChild (1).GetChild (0).GetChild (0).gameObject.SetActive (true);
				break;
			case 2:
				t.GetChild (1).GetChild (0).GetChild (0).gameObject.SetActive (true);
				t.GetChild (1).GetChild (1).GetChild (0).gameObject.SetActive (true);
				break;
			case 3:
				t.GetChild (1).GetChild (0).GetChild (0).gameObject.SetActive (true);
				t.GetChild (1).GetChild (1).GetChild (0).gameObject.SetActive (true);
				t.GetChild (1).GetChild (2).GetChild (0).gameObject.SetActive (true);
				break;
			}
			_lvlNumCounter++;
		}

		ChangeTheStage ();
		InitialStagePosAdjustment ();
	}

	void InitialStagePosAdjustment()
	{
		for (int i = 0; i < _stageContainer.childCount; i++) 
		{
			_stageContainer.GetChild (i).GetComponent<RectTransform> ().anchoredPosition = new Vector3 (_stagespos,0f,0f);
			_stagespos += 1200f;

			Instantiate (_indicatorPrefab, _stageIndicatorContainer.transform);
		}

		ControlTheIndicator ();
	}

	void Update()
	{
		_stageContainer.GetComponent<RectTransform> ().anchoredPosition = Vector3.Lerp (
			_stageContainer.GetComponent<RectTransform> ().anchoredPosition,
			new Vector3(-_stageContainer.GetChild(_stageCounter).GetComponent<RectTransform>().anchoredPosition.x,0f,0f),
			Time.deltaTime*_smoothness
		);

		if(Input.GetMouseButtonDown(0))
		{
			_initialTouchPoint=Camera.main.ScreenToViewportPoint(Input.mousePosition);
		}
		else if(Input.GetMouseButtonUp(0))
		{
			_finalTouchPoint=Camera.main.ScreenToViewportPoint(Input.mousePosition);
			FindoutTheSwipeDirection();
		}
	}

	void FindoutTheSwipeDirection()
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
				GoToNextStage();
			}
			else
			{
				GoToPreviousStage();
			}
			_canSwipe=false;
		}
		
	}

	void LoadTheLevel(int _levelNumber)
	{
		MenuController._instance.PlayButtonSound ();
		StartCoroutine (GoToLevel(_levelNumber));
	}
		
	IEnumerator GoToLevel(int _num)
	{
		yield return new WaitForSeconds (0.3f);
		SceneManager.LoadScene ("Level" + _num);
	}
		
	#region logic for changing the stage
	public void GoToNextStage()
	{
		if (_stageCounter < _stageContainer.childCount - 1)
		{
			_stageCounter++;
		}

		ChangeTheStage ();
		ControlTheIndicator ();
	}

	public void GoToPreviousStage()
	{
		if (_stageCounter > 0)
		{
			_stageCounter--;
		}

		ChangeTheStage ();
		ControlTheIndicator ();
	}

	void ChangeTheStage()
	{
		if (_stageCounter == 0) 
		{
			_stageChangeButton[0].interactable = false;
		} else{
			_stageChangeButton[0].interactable = true;
		}

		if (_stageCounter == _stageContainer.childCount - 1) {
			_stageChangeButton [1].interactable = false;
		} else {
			_stageChangeButton[1].interactable = true;
		}
		//
		MenuController._instance.PlayButtonSound ();
	}

	void ControlTheIndicator()
	{
		//adjusting the indicator
		for (int i = 0; i < _stageIndicatorContainer.childCount; i++)
		{
			_stageIndicatorContainer.GetChild (i).GetChild (0).gameObject.SetActive (false);
		}
		_stageIndicatorContainer.GetChild (_stageCounter).GetChild (0).gameObject.SetActive (true);
	}
	#endregion stage change
}
