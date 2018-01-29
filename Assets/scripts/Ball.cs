using System.Collections;
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

	private bool _shooted = false;
	private bool _readyToShoot = true;

	private Vector3 _spawnPoint = Vector3.up*0.219f; 
	
	public bool IsKillingAble{
		get { 
			return _shooted;
		}
	}

	private Rigidbody2D _rb;

	private CircleCollider2D _colider;
	private SpriteRenderer _sprite;
	private Transform _realParent;
	
	// Use this for initialization
	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
		_colider = GetComponent<CircleCollider2D>();
		_sprite = GetComponent<SpriteRenderer>();
		_realParent = transform.parent;
	}

	private void Start()
	{
		speed = defaultBallSpeed;

		if (topPaddle == null)
			topPaddle = GameObject.Find ("PaddleTop").gameObject.GetComponent<Paddle>();
		
		if (bottomPaddle == null)
			bottomPaddle = GameObject.Find ("PaddleBottom").gameObject.GetComponent<Paddle>();

		resetBall();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!_shooted) {
			if (_readyToShoot && Input.GetMouseButtonUp(0)) {
				shoot (bottomPaddle);
			}
		}
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (!_shooted) return;
		
		if (coll.gameObject.CompareTag("Paddle")) {
			Paddle paddle = coll.gameObject.GetComponent<Paddle> ();
			paddleBounce (paddle);
		} else if (coll.gameObject.CompareTag("BallDeadZone")) {
			if (isBonus) {
				Destroy (this.gameObject);
			} else {
				requestRestartBall ();
			}
		}
		else if (coll.gameObject.CompareTag("Wall")) {

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
		transform.SetParent(_realParent);
		_colider.enabled = true;
		_readyToShoot = false;
		// add velocity of paddle
		float X = fromPaddle.FakeVelocity * velocityDragMultiplier;

		float Y = speed;

		if (fromPaddle.name == topPaddle.name) {
			Y = -speed;
		} 

		_rb.velocity = new Vector2 (X, Y).normalized * speed;
		_shooted = true;

		Debug.Log ("shooted :" + fromPaddle.transform.position + " BALL velocity:" + _rb.velocity + " Fake paddle velocity:" + fromPaddle.FakeVelocity);

	}

	public void requestRestartBall(){

		if (isBonus) {
			Destroy (this.gameObject);
			return;
		}
		_colider.enabled = false;		
		transform.SetParent(bottomPaddle.transform);
		transform.localPosition = _spawnPoint;
		
		_rb.velocity = Vector2.zero;
		_shooted = false;
		_readyToShoot = false;

		StartCoroutine("pendingRestart");
	}

	IEnumerator pendingRestart()
	{
		var c = _sprite.color;
		c.a = 0.0f;
		_sprite.color = c;
		const int numSteps = 3;
		for (int i = 0; i < numSteps; i++)
		{
			yield return new WaitForSeconds(3.0f/numSteps);
			c.a += 1.0f/numSteps;
			_sprite.color = c;
		}
		c.a = 1.0f;
		_sprite.color = c;
		resetBall ();
	}

	public void resetBall ()
	{
		transform.SetParent(bottomPaddle.transform);
		transform.localPosition = _spawnPoint;
		
		_rb.velocity = Vector2.zero;
		_shooted = false;
		_readyToShoot = true;

		_colider.enabled = false;
	}


}
