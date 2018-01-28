using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameManager.instance.addPowerUpSpawnPoint (this.transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
