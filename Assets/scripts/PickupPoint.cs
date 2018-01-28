using UnityEngine;
using System.Collections;

public class PickupPoint : MonoBehaviour
{
	public bool isActivated = false;
	public bool picking = false;

	public Sprite defaultSprite;
	public Sprite pickedSprite;

	private float timeToPick = 2f;
	// Use this for initialization
	void Awake ()
	{
		if(GameManager.instance){
			GameManager.instance.addPickupPoint (this);
		}
	}

	public void activatePoint(){
		timeToPick = 2f;
		//GetComponent<SpriteRenderer> ().enabled = true;
		transform.localScale = Vector2.one * 1.2f;

		transform.Find ("rune").GetComponent<Animator> ().SetBool ("hidden", false);

		isActivated = true;

		Debug.Log ("activated point");
	}

	public void startPicking(){
		if (isActivated) {
			picking = true;

			transform.Find ("rune").GetComponent<Animator> ().SetBool ("picking", true);
			Debug.Log ("start point");
		}
			
	}

	public void endPicking(){
		if (isActivated) {
			picking = false;
			transform.Find ("rune").GetComponent<Animator> ().SetBool ("picking", false);
		}
			
	}

	public void picked(){
		picking = false;
		GameManager.instance.finishedPickupPoint (this);

		transform.Find ("rune").GetComponent<Animator> ().SetTrigger ("picked");
		transform.Find ("rune").GetComponent<Animator> ().SetBool ("hidden", true);
	}

	public void disablePoint(){
		//GetComponent<SpriteRenderer> ().enabled = false;

		transform.localScale = Vector2.one;
		isActivated = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (picking) {
			timeToPick -= Time.deltaTime;

			if(timeToPick <= 0){
				picked ();
			
			}
		}
	}


	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.CompareTag("Player")) {
			startPicking ();
			coll.gameObject.GetComponent<movControl>().IsPicking = true;
		}
	}

	void OnTriggerExit2D (Collider2D coll)
	{
		if (coll.gameObject.CompareTag("Player")) {
			endPicking ();
			coll.gameObject.GetComponent<movControl>().IsPicking = false;
		}
	}
}

