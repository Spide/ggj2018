
using UnityEngine;

public class StepsDust : MonoBehaviour
{

	public GameObject[] dusts;
	public Vector3 _offset;
	private int current;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void Add (Vector3 position)
	{
		var d = dusts[current];
		d.SetActive(true);
		d.transform.localPosition = position + _offset;
		var sc = Random.Range(0.4f, 0.9f);
		d.transform.localScale = new Vector3(sc,sc,sc);
		d.GetComponent<DustParticle>()._yStart = position.y + _offset.y;
		current = (current + 1) % dusts.Length;
	}
}
