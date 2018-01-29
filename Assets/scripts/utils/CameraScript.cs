using UnityEngine;

public class CameraScript : MonoBehaviour {

	
	void Start ()
	{
		var c = GetComponent<Camera>();
		if ( Screen.width / (float) Screen.height < 16f / 9f)
		{
			c.orthographicSize = 6.4f * Screen.height / Screen.width;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
