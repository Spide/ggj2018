using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteButton : MonoBehaviour
{

	public Sprite StateUp;
	public Sprite StateDown;

	public UnityEvent  action;

	private SpriteRenderer _ren;
	// Use this for initialization
	void Start ()
	{
		_ren = GetComponent<SpriteRenderer>();
		_ren.sprite = StateUp;

		var col = GetComponent<BoxCollider2D>();
		if (col == null)
		{
			gameObject.AddComponent<BoxCollider2D>();
		}
	}

	private void OnMouseEnter()
	{
		_ren.sprite = StateDown;
	}
	
	private void OnMouseExit()
	{
		_ren.sprite = StateUp;
	}

	// Update is called once per frame
	private void OnMouseDown()
	{
		_ren.sprite = StateDown;
		var c = _ren.color;
		c.a = 0.6f;
		_ren.color = c;
	}

	private void OnMouseUpAsButton()
	{
		_ren.sprite = StateUp;
		if (action != null)
		{
			action.Invoke();
		}
	}

	private void OnMouseUp()
	{
		_ren.sprite = StateUp;
		var c = _ren.color;
		c.a = 1f;
		_ren.color = c;
	}
}
