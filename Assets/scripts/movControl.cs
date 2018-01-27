using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class movControl : MonoBehaviour
{
	
	public float MaxSpeed;
	public float JumpVelocity = 2;
	
	public bool _grounded;
	public Transform GroundCheck;

	private float groundRadius =  0.3f;
	public LayerMask WhatIsGround; 
	private Rigidbody2D _rb;

	private KeyCode _kUp;
	private KeyCode _kDown;

	public float FallMultiplier = 8f;
	private float hasJumped;
	void Awake () {
		_rb = GetComponent<Rigidbody2D> ();

		_kUp = KeyCode.UpArrow;
		_kDown = KeyCode.DownArrow;		
	}

	void Update()
	{
		_grounded = Physics2D.OverlapCircle (GroundCheck.position, groundRadius, WhatIsGround);
	}
	
	void FixedUpdate(){
		
		
		var move = Input.GetAxis ("Horizontal");
		if (move > 0) {
			_rb.velocity = new Vector2 (move * MaxSpeed, _rb.velocity.y);
		} else if (move < 0) {
			_rb.velocity = new Vector2 (move * MaxSpeed, _rb.velocity.y);
		}
		
		if (_rb.velocity.y < -0.05f)
		{
			print("fall");
			hasJumped = 1;
			_rb.velocity += Vector2.down*FallMultiplier*Time.deltaTime;
			//_rb.velocity += Vector2.up * Physics2D.gravity.y * (FallMultiplier-1)*Time.deltaTime;
		}
		else if (_rb.velocity.y > 0.05f && Input.GetKey(_kUp))
		{
			_rb.velocity += Vector2.up*FallMultiplier*Time.deltaTime;
			print("rise");
			//_rb.velocity += Vector2.up * Physics2D.gravity.y * (LowJumpMultiplier-1)*Time.deltaTime;
		}

		if (_grounded && hasJumped > 0.4f && Input.GetKey(_kUp))
		{
			print("FASDFASD");
			hasJumped = 0;
			_rb.velocity = new Vector2 (_rb.velocity.x, JumpVelocity);
		}
		else
		{
			hasJumped += Time.deltaTime ;
		}
		
	}

}
