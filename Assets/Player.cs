using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int lives = 3;

	bool fieldEnabled = false;
	float fieldTimer = 0;

	public GameObject forceField;

	private BearAnim _anim;
	// Use this for initialization
	void Start ()
	{
		_anim = GetComponent<BearAnim>();
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
		forceField.GetComponent<SpriteRenderer>().enabled = true;
		forceField.GetComponent<CircleCollider2D>().enabled = true;
	}

	public void disableForceField(){
		fieldEnabled = false;
		forceField.GetComponent<SpriteRenderer>().enabled = false;
		forceField.GetComponent<CircleCollider2D>().enabled = false;
	}

	public void respawn(Vector2 spawnPoint){

		lives -= 1;

		transform.position = spawnPoint;
		_anim.state = BearAnimState.Idle;
	}
}
