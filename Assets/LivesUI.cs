using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour {

	public Text self;

	public Player player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		self.text = "Lives: " + player.lives;
	}
}
