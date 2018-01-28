﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	bool fieldEnabled = false;
	float fieldTimer = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (fieldEnabled) {
			if(fieldTimer <=0){
				disableForceField ();
			}

			fieldTimer -= Time.deltaTime;
		}
			


	}

	public void boom(Ball byBall)
	{
		GetComponent<movControl> ().justDied ();

		if (GameManager.instance) {
			GameManager.instance.onDead (GetComponent<Player>(), byBall);
		}

	}

	public void enableForceField(){
		fieldEnabled = true;
		fieldTimer = 5;
		transform.Find ("ForceField").GetComponent<SpriteRenderer>().enabled = true;
		transform.Find ("ForceField").GetComponent<CircleCollider2D>().enabled = true;
	}

	public void disableForceField(){
		fieldEnabled = false;
		transform.Find ("ForceField").GetComponent<SpriteRenderer>().enabled = false;
		transform.Find ("ForceField").GetComponent<CircleCollider2D>().enabled = false;
	}

	public void respawn(Vector2 spawnPoint){
		transform.position = spawnPoint;
	}
}
