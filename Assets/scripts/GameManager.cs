using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public float defaultCameraSize;

	public bool focus;
	public float timeToFocus;
	public Vector3 deadPosition;
	public Vector3 defaultPosition;

	public static GameManager instance;

	// Use this for initialization
	void Awake () {
		instance = this;
	}

	// Update is called once per frame
	void Update () {

		if (focus) {


			Vector3 result = Vector3.Lerp (defaultPosition, deadPosition, 1f - 0.01f);
			Camera.main.gameObject.transform.position = new Vector3(result.x, result.y, -10f);
			timeToFocus -= Time.deltaTime;

			float focused = Mathf.Lerp (defaultCameraSize, defaultCameraSize-0.2f, 1f - 0.1f);
			Camera.main.orthographicSize = focused;

			if (timeToFocus <= 0) {
				
				Camera.main.orthographicSize = defaultCameraSize;
				Camera.main.gameObject.transform.position = defaultPosition;
				Time.timeScale = 1f;

				focus = false; 
			} 
		}


	}

	public void onDead(Vector3 position){
		defaultCameraSize = Camera.main.orthographicSize;
		defaultPosition = Camera.main.gameObject.transform.position;
		deadPosition = position;

		//Camera.main.orthographicSize = defaultCameraSize - 0.2f;
		Time.timeScale = 0.5f;

		focus = true;
		timeToFocus = 0.5f;
	}
		


}
