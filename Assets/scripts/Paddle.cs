using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {


	public Vector3 FakeVelocity {
		get {
			return  transform.position - lastPosition;
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
	}

	// Update is called once per frame
	void Update () {



		this.transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y);


		if (vFckr >= 3) {
			lastPosition = transform.position;

			vFckr = 0;
		}

		vFckr++;
	}




}
