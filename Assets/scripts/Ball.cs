using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

	public float defaultBallSpeed = 5;
	public float speed;

	public Paddle topPaddle;
	public Paddle bottomPaddle;

	public float velocityDragMultiplier = 5;

	public float paddleDragMultiplier = 10;

	private bool shooted = false;




	// Use this for initialization
	void Start ()
	{
		speed = defaultBallSpeed;

		if (topPaddle == null)
			topPaddle = GameObject.Find ("PaddleTop").gameObject.GetComponent<Paddle>();
		
		if (bottomPaddle == null)
			bottomPaddle = GameObject.Find ("PaddleBottom").gameObject.GetComponent<Paddle>();


	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!shooted) {
			transform.position = bottomPaddle.BallSpawnPoint.position;

			if (Input.anyKeyDown) {
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
			resetBall ();
		}
		else if (coll.gameObject.tag == "Wall") {
			resetBall ();
		}else {
			coll.gameObject.SendMessage ("boom");
			this.gameObject.SetActive (false);
		}
	}

	private void paddleBounce(Paddle paddle){
		

		Debug.Log ("on hit Paddle position :" + paddle.transform.position + " BALL velocity:" + transform.GetComponent<Rigidbody2D>().velocity + " Fake paddle velocity:" + paddle.FakeVelocity);

		float X = 0;

		// calc angle
		X = -(paddle.transform.position - transform.position).x * paddleDragMultiplier;

		// add velocity of paddle
		X = X + (paddle.FakeVelocity.x * velocityDragMultiplier);

		float Y = speed;

		if (paddle.name == topPaddle.name) {
			Y = -speed;
		} 

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (X, Y).normalized * speed;
	}

	private void shoot (Paddle fromPaddle)
	{
		// add velocity of paddle
		float X = fromPaddle.FakeVelocity.x * velocityDragMultiplier;

		float Y = speed;

		if (fromPaddle.name == topPaddle.name) {
			Y = -speed;
		} 

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (X, Y).normalized * speed;
		shooted = true;

		Debug.Log ("shooted :" + fromPaddle.transform.position + " BALL velocity:" + transform.GetComponent<Rigidbody2D>().velocity + " Fake paddle velocity:" + fromPaddle.FakeVelocity);

	}

	private void resetBall ()
	{
		transform.position = bottomPaddle.BallSpawnPoint.position;
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0);
		shooted = false;
	}


}
