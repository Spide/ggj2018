﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public float defaultCameraSize;

	public bool focus;
	public float timeToFocus;
	public Vector3 deadPosition;
	public Vector3 defaultPosition;

	public static GameManager instance;

	private List<Player> players;

	private Dictionary<Player, Ball> playersDead;

	private List<Transform> spawnPoints;

	private List<PickupPoint> pickupPoints;

	private PickupPoint actualPickupPoint;

	// Use this for initialization
	void Awake () {

		players = new List<Player> ();

		playersDead = new Dictionary<Player, Ball> ();

		spawnPoints = new List<Transform> ();

		pickupPoints = new List<PickupPoint> ();

		instance = this;


	}

	public void addSpawnPoint(Transform point){
		spawnPoints.Add (point);
	}


	public void addPickupPoint(PickupPoint point){
		pickupPoints.Add (point);
	}


	// Update is called once per frame
	void Update () {

		if (focus) {


			//Vector3 result = Vector3.Lerp (defaultPosition, deadPosition, 1f - 0.01f);
			//Camera.main.gameObject.transform.position = new Vector3(result.x, result.y, -10f);
			timeToFocus -= Time.deltaTime;

			//float focused = Mathf.Lerp (defaultCameraSize, defaultCameraSize-0.2f, 1f - 0.1f);
			//Camera.main.orthographicSize = focused;

			if (timeToFocus <= 0) {

				foreach (Player p in playersDead.Keys) {
					
					p.respawn (spawnPoints [Random.Range (0, spawnPoints.Count - 1)].position);
					playersDead [p].resetBall ();
				}

				playersDead.Clear ();
					
				
				//Camera.main.orthographicSize = defaultCameraSize;
				//Camera.main.gameObject.transform.position = defaultPosition;
				Time.timeScale = 1f;

				focus = false; 
			} 
		}


	}



	public void onDead(Player player, Ball byBall){
		//defaultCameraSize = Camera.main.orthographicSize;
		//defaultPosition = Camera.main.gameObject.transform.position;
		//deadPosition = player.transform.position;

		playersDead.Add (player, byBall);

		//Camera.main.orthographicSize = defaultCameraSize - 0.2f;
		Time.timeScale = 0.5f;

		focus = true;
		timeToFocus = 0.5f;
	}
		


}
