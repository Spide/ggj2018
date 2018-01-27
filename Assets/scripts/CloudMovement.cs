using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{

	private GameObject[] clones;
	// Use this for initialization
	void Start()
	{

		var sp = GetComponent<SpriteRenderer>();
		var clone1 = Instantiate(gameObject);
		//clone1.
		var clone2 = Instantiate(gameObject);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
