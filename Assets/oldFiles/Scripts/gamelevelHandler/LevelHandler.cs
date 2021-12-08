using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour 
{
	public GameObject _brustParticlePrefab;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (GameController._instance._canPlayGame) {
			if (Input.GetMouseButtonDown (0)) {
				Vector2 _mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				RaycastHit2D _hit = Physics2D.Raycast (_mousePos, Vector2.zero);
				if (_hit.collider != null) {
					if (_hit.collider.gameObject.CompareTag ("Destroyable")) {
						_hit.collider.gameObject.SetActive (false);
						AudioHandler._instance.PlayBlastSound ();
						FadeTheColorOfRope (_hit.collider.transform.parent.GetChild (1).transform);
						GameObject _brustEffect = Instantiate (_brustParticlePrefab)as GameObject;
						_brustEffect.transform.position = new Vector3 (_mousePos.x, _mousePos.y, 0f);
						_brustEffect.GetComponent<ParticleSystem> ().Play ();
					}
				}
			}
		}
	}

	void FadeTheColorOfRope(Transform _ropeContainer)
	{
		foreach (Transform t in _ropeContainer) 
		{
			t.GetComponent<Line> ()._startFading = true;
		}
	}
}
