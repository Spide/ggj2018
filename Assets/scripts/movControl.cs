using UnityEngine;

public class movControl : MonoBehaviour
{
	
	public float MaxSpeed;
	public float JumpVelocity = 2;
	public float DoubleJumpVelocity = 3;
	
	private bool _grounded;
	private bool _touchingWall;
	
	public Transform GroundCheck;
	public Transform WallJumpCheckLeft;
	public Transform WallJumpCheckRight;

	private float groundRadius =  0.1f;
	private float wallJumpRadius =  0.2f;
	public LayerMask WhatIsGround; 
	private Rigidbody2D _rb;

	private KeyCode _kUp;
	private KeyCode _kDown;

	public float FallMultiplier = 16f;
	public float JumpMultiplier = 8f;
	
	private float _hasJumped;
	private bool _hasWallJump;
	private bool _doubleJumped;

	private bool flip;
	
	void Awake () {
		_rb = GetComponent<Rigidbody2D> ();

		_kUp = KeyCode.UpArrow;
		_kDown = KeyCode.DownArrow;		
	}

	void Update()
	{
		_grounded = Physics2D.OverlapCircle (GroundCheck.position, groundRadius, WhatIsGround);
		_touchingWall = Physics2D.OverlapCircle (WallJumpCheckLeft.position, wallJumpRadius, WhatIsGround);
		if (!_touchingWall)
		{
			_touchingWall = Physics2D.OverlapCircle(WallJumpCheckRight.position, wallJumpRadius, WhatIsGround);
		}
		if (!_touchingWall || _grounded)
		{
			_hasWallJump = false;
		}
		if (_grounded || _touchingWall)
		{
			_doubleJumped = false;
		}
	}

	private void DoFlip()
	{
		flip = !flip;
		var ls = transform.localScale;
		ls.x = flip ? -1 : 1;
		transform.localScale = ls;
	}
	
	void FixedUpdate(){
		
		
		//HORIZONTAL MOVE
		
		var move = Input.GetAxisRaw("Horizontal");
		var targetX = 0f;
		if ( Mathf.Abs(move) > 0.05f )
		{
			targetX = Mathf.Lerp(_rb.velocity.x, move * MaxSpeed, 0.5f);
		}
		_rb.velocity = new Vector2(targetX, _rb.velocity.y);

		if (targetX < 0 && !flip || targetX > 0 && flip)
		{
			DoFlip();
		}
		
		
		// VERTICAL MOVE
			
		if (_rb.velocity.y < -0.05f)
		{
			_hasJumped = 1;
			_rb.velocity += Vector2.down *FallMultiplier * Time.deltaTime;
		}
		else if (_rb.velocity.y > 0.05f && Input.GetKey(_kUp))
		{
			_rb.velocity += Vector2.up*JumpMultiplier*Time.deltaTime;
		}

		//JUMP
		
		if ( _grounded  && _hasJumped > 0.3f && Input.GetKey(_kUp))
		{
			_hasJumped = 0;
			_rb.velocity = new Vector2 (_rb.velocity.x, JumpVelocity);
		}else if (!_doubleJumped && _hasJumped > 0.1f && Input.GetKeyDown(_kUp))
		{
			_hasJumped = 0;
			_doubleJumped = true;
			_rb.velocity = new Vector2 (_rb.velocity.x, DoubleJumpVelocity);
		}
		else if (_touchingWall && !_hasWallJump && Input.GetKeyDown(_kUp))
		{
			_hasWallJump = true;
			_rb.velocity = new Vector2(-_rb.velocity.x, JumpVelocity);
		}
		else
		{
			_hasJumped += Time.deltaTime ;
		}
		
	}

	public void boom(){
		GameManager.instance.onDead (transform.position);
	}

}
