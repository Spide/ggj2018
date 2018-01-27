using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

	private float[] lastPositions;
	private int lastPositionIndex;
	
	public float FakeVelocity {
		get
		{
			var vp = (lastPositionIndex + 3) % 4;
			var dist = 0f;
			for (var i = 0; i < lastPositions.Length-1; i++)
			{
				var d1 = lastPositions[vp];
				vp = (vp + 3) % 4;
				var d2 = lastPositions[vp];
				dist += d1 - d2;
			}

			return dist;
		}
	}

	private int vFckr = 0;

	private Vector3 lastPosition;

	public Transform ballSpawnPoint;

	public Transform BallSpawnPoint {
		get {
			return ballSpawnPoint;
		}
	}

	// Use this for initialization
	void Awake () {
		lastPosition = transform.position;

		ballSpawnPoint = transform.Find ("BallSpawnPoint");

		lastPositions = new[] {0f, 0f, 0f, 0f};
	}

	// Update is called once per frame
	void Update () {

		this.transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y);
		
		lastPositions[lastPositionIndex] = transform.position.x;
		lastPositionIndex = (lastPositionIndex+1) % 4;
	}

}
