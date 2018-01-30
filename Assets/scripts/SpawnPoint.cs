using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameManager.instance.addSpawnPoint (transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
