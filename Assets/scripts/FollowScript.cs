using UnityEngine;

public class FollowScript : MonoBehaviour
{
	public Transform follow;

	// Update is called once per frame
	void Update()
	{
		var lc = transform.localPosition;
		var tp = follow.position;
		lc.x = tp.x;
		lc.y = tp.y;
		lc.z = -1f;
		transform.localPosition = lc;
	}
}