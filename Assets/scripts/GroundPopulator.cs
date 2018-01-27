using UnityEngine;

[ExecuteInEditMode]
public class GroundPopulator : MonoBehaviour
{

	public GameObject pr;
	// Use this for initialization
	void Update ()
	{

		if (transform.childCount > 0) return; 
		for (var i = 0; i < 30; i++)
		{
			var p = Instantiate(pr, transform);
			p.transform.localPosition = new Vector3(0,0.42f*(i-12f));
		}
	}
	
}
