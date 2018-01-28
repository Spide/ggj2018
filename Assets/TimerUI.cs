using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour {

	public Text self;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		self.text = "Time: " + ( (int)  GameManager.instance.timeToEnd ) +"s";
	}
}
