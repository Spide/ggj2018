using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void pickupBy(Player player){
		
	}

	public void pickupBy(Ball ball){

		Instantiate (ball.gameObject);
		
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Player") {
			pickupBy (coll.gameObject.GetComponent<Player> ());
		}
		else if (coll.gameObject.tag == "Ball") {
			pickupBy (coll.gameObject.GetComponent<Ball> ());
		}

	}
}
