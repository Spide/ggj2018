using System.Collections.Generic;
using UnityEngine;

public class BearAnim : MonoBehaviour
{
	private BearAnimState _state;

	private float _animTime = 0f;

	private Sprite[] _currentAnim;
	private int _currentFrame = 0;

	public BearAnimState state
	{
		get { return _state; }
		set
		{
			if (_state == value)
			{
				return;
			}
			_state = value;
			_currentAnim = dict[_state];
			sp.sprite = _currentAnim[0];
			_animTime = 0;
			_currentFrame = 0;
		}
	}

	private SpriteRenderer sp;

	[SerializeField] private Sprite[] idleAnim;
	[SerializeField] private Sprite[] runAnim;

	[SerializeField] private float idleFPS = 3;
	[SerializeField] private float runFPS = 10;

	private Dictionary<BearAnimState, Sprite[]> dict;

	// Use this for initialization
	void Start()
	{
		sp = GetComponent<SpriteRenderer>();
		dict = new Dictionary<BearAnimState, Sprite[]>
		{
			{BearAnimState.Idle, idleAnim},
			{BearAnimState.Running, runAnim}
		};

		state = BearAnimState.Idle;
	}

	// Update is called once per frame
	void Update()
	{
		if (_currentAnim == null || _currentAnim.Length <= 1)
		{
			return;
		}

		_animTime += Time.deltaTime;

		var fps = 0f;
		switch (_state)
		{
			case BearAnimState.Idle:
				fps = idleFPS;
				break;
			case BearAnimState.Running:
				fps = runFPS;
				break;
			default:
				fps = 0;
				break;
		}


		if (_animTime >= (_currentFrame + 1f) / fps)
		{
			_currentFrame++;
			if (_currentFrame > _currentAnim.Length - 1)
			{
				_animTime -= _currentFrame / fps;
				_currentFrame = 0;
			}
			sp.sprite = _currentAnim[_currentFrame];
		}
	}
}