using UnityEngine;

public abstract class PowerUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public  virtual void pickupBy(Player player){
		

	}

	public virtual void pickupBy(Ball ball){

	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		//Debug.Log ("triggered"+coll.gameObject.name);

		if (coll.gameObject.CompareTag("Player")) {
			pickupBy (coll.gameObject.GetComponent<Player> ());
		}
		else if (coll.gameObject.CompareTag("Ball")) {
			pickupBy (coll.gameObject.GetComponent<Ball> ());
		}

		GameManager.instance.powerupPicked (this);

		Destroy(this.gameObject);

	}
}
