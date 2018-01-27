using UnityEngine;

public class Levitating : MonoBehaviour
{
	[SerializeField]
	private int pixelsPerUnit;
	
	public float _amountY = 0.05f;
	public float _amountX = 0.05f;
	public float _speedX = 0.35f;
	public float _speedY = 0.38f;

	private float _offset = 0;

	private float _sin1 = 0;
	private float _sin2 = 0;
	private float _sin3 = 0;
	private float _swayTime = 0;

	private Vector3 _startPos;
	
	private Transform parent;

	// Use this for initialization
	void Start()
	{
		_startPos = transform.localPosition;
		_sin1 = Random.Range(0f, 1f) * _speedY * 0.75f + _speedY * 0.25f;
		_sin2 = Random.Range(0f, 1f) * _speedX * 0.25f + _speedX * 0.75f;
		_sin3 = Random.Range(0f, 1f) / 2 + 0.3f;
		_offset = Random.Range(0f, 1f) * 3f;

		parent = transform.parent;
	}

	// Update is called once per frame
	void Update()
	{
		_swayTime += Time.deltaTime;
		var p = transform.localPosition;

		p.x = _startPos.x + _amountX * Mathf.Sin(_sin2 * _swayTime + _offset) * Mathf.Sin(_sin3 * _swayTime);
		p.y = _startPos.y + _amountY * Mathf.Sin(_sin1 * _swayTime + _offset);

		transform.localPosition = p;
		
/*
		p.x = Mathf.Round(p.x+parent.position.x * pixelsPerUnit) / pixelsPerUnit - parent.position.x;
		p.y = Mathf.Round(p.y+parent.position.y * pixelsPerUnit) / pixelsPerUnit - parent.position.y;
*/
		transform.localPosition = p;
	}
}