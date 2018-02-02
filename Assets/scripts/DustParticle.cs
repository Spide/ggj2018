using UnityEngine;

public class DustParticle : MonoBehaviour
{

	public Vector3 v;

	public float _yStart;

	private SpriteRenderer sp;
	private void OnEnable()
	{
		v = Vector3.up*(1f+Random.Range(0.12f,0.6f));
		sp = GetComponent<SpriteRenderer>();
		var c = sp.color;
		c.a = Random.Range(0.6f, 1f);
		sp.color = c;
	}

	// Use this for initialization

	// Update is called once per frame
	void Update ()
	{
		v.y -= 6f * Time.deltaTime;
		transform.localPosition += v*Time.deltaTime;

		var c = sp.color;
		c.a -= 1.8f*Time.deltaTime;
		sp.color = c;
		
		if ( c.a <= 0 || transform.localPosition.y < _yStart)		
		{
			gameObject.SetActive(false);
		}
	}
}
