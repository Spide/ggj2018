using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
	public float Speed;
	private GameObject[] clones;
	private float _w;
	// Use this for initialization
	void Start()
	{
		print("Making copies of "+gameObject.name);
		var sp = GetComponent<SpriteRenderer>();
		
		_w = sp.bounds.size.x;
		var clone1 = Instantiate(gameObject,transform.parent);
		var clone2 = Instantiate(gameObject,transform.parent);
		clone1.transform.transform.localPosition = Vector3.right*_w;
		clone2.transform.transform.localPosition = Vector3.right*_w*2f;
		Destroy(clone1.GetComponent<CloudMovement>());
		Destroy(clone2.GetComponent<CloudMovement>());
		
		clones = new []{gameObject, clone1, clone2};
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		var dv = Vector3.left * Speed * Time.deltaTime;
		for (var i = 0; i < clones.Length; i++)
		{
			clones[i].transform.localPosition += dv;
		}
		
		if (clones[0].transform.localPosition.x < -_w)
		{
			var d1 = clones[0];
			d1.transform.localPosition = Vector3.right*_w*2f;

			var d2 = clones[1];
			var d3 = clones[2];

			clones[0] = d2;
			clones[1] = d3;
			clones[2] = d1;
		}
	}
}
