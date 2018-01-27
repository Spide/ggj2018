using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void boom(Ball byBall)
	{
		GetComponent<movControl> ().justDied ();

		if (GameManager.instance) {
			GameManager.instance.onDead (GetComponent<Player>(), byBall);
		}

	}

	public void respawn(Vector2 spawnPoint){
		transform.position = spawnPoint;
	}
}
