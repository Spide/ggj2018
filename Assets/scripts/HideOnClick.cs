using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideOnClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	private void OnMouseUp()
	{
		gameObject.SetActive(false);
	}

	public void Hide(){
		gameObject.SetActive(false);
	}

	public void Activate()
	{
		gameObject.SetActive(true);
		if (gameObject.GetComponent<Image>())
		{
			
		}
	}
}
