using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour {

	private Text self;

	private Player player;
	// Use this for initialization
	void Start ()
	{
		self = GetComponent<Text>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		self.text = "Lives: " + player.lives;
	}
}
