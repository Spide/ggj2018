﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	public bool isBonus;

	public float defaultBallSpeed = 5;
	public float speed;

	public Paddle topPaddle;
	public Paddle bottomPaddle;

	public float velocityDragMultiplier = 5;

	public float paddleDragMultiplier = 10;

	private bool shooted = false;
	private bool readyToShoot = true;


	private Rigidbody2D _rb;

	// Use this for initialization
	void Start ()
	{
		speed = defaultBallSpeed;

		if (topPaddle == null)
			topPaddle = GameObject.Find ("PaddleTop").gameObject.GetComponent<Paddle>();
		
		if (bottomPaddle == null)
			bottomPaddle = GameObject.Find ("PaddleBottom").gameObject.GetComponent<Paddle>();

		_rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!shooted) {
			transform.position = bottomPaddle.BallSpawnPoint.position;

			if (readyToShoot && Input.GetMouseButtonUp(0)) {
				shoot (bottomPaddle);
			}
		}
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Paddle") {
			Paddle paddle = coll.gameObject.GetComponent<Paddle> ();
			paddleBounce (paddle);
		} else if (coll.gameObject.tag == "BallDeadZone") {
			if (isBonus) {
				Destroy (this.gameObject);
			} else {
				requestRestartBall ();
			}
		}
		else if (coll.gameObject.tag == "Wall") {

		}else if (coll.gameObject.name == "ForceField") {
			if (isBonus) {
				Destroy (this.gameObject);
			} else {
				requestRestartBall ();
			}
		}else {
			coll.gameObject.SendMessage ("boom", this, SendMessageOptions.DontRequireReceiver);
			//this.gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			//this.resetBall ();
		}
	}

	private void paddleBounce(Paddle paddle){
		

		//Debug.Log ("on hit Paddle position :" + paddle.transform.position + " BALL velocity:" + transform.GetComponent<Rigidbody2D>().velocity + " Fake paddle velocity:" + paddle.FakeVelocity);

		var v = _rb.velocity;
		
		float X = 0;

		// calc angle
		if (Mathf.Abs((paddle.transform.position - transform.position).x) > 1.0f)
		{
			X = -(paddle.transform.position - transform.position).x * paddleDragMultiplier;
		}
		else
		{
			// add velocity of paddle
			X = v.x + (paddle.FakeVelocity * velocityDragMultiplier);
		}

		float Y = speed;

		if (paddle.name == topPaddle.name) {
			Y = -speed;
		} 

		_rb.velocity = new Vector2 (X, Y).normalized * speed;
	}

	private void shoot (Paddle fromPaddle)
	{
		// add velocity of paddle
		float X = fromPaddle.FakeVelocity * velocityDragMultiplier;

		float Y = speed;

		if (fromPaddle.name == topPaddle.name) {
			Y = -speed;
		} 

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (X, Y).normalized * speed;
		shooted = true;

		//Debug.Log ("shooted :" + fromPaddle.transform.position + " BALL velocity:" + transform.GetComponent<Rigidbody2D>().velocity + " Fake paddle velocity:" + fromPaddle.FakeVelocity);

	}

	Color actualColor;

	public void requestRestartBall(){

		if (isBonus) {
			Destroy (this.gameObject);
			return;
		}
			

		actualColor = GetComponent<SpriteRenderer> ().color;
		
		GetComponent<SpriteRenderer> ().color = new Color (actualColor.r, actualColor.g, actualColor.a, 0.3f);
		transform.position = bottomPaddle.BallSpawnPoint.position;
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0);
		shooted = false;
		readyToShoot = false;

		StartCoroutine("pendingRestart");
	}

	IEnumerator pendingRestart() {
		yield return new WaitForSeconds(3);

		resetBall ();
	}

	public void resetBall ()
	{
		actualColor = GetComponent<SpriteRenderer> ().color;

		GetComponent<SpriteRenderer> ().color = new Color (actualColor.r, actualColor.g, actualColor.a, 1f);
		transform.position = bottomPaddle.BallSpawnPoint.position;
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0);
		shooted = false;
		readyToShoot = true;
	}


}
