using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Migician : MonoBehaviour
{

	public GameObject aura;
	
// Use this for initialization
	void Start () {
		
	}

	public void EndGameEnable()
	{
		StartCoroutine("EndAnim");
	}

	private IEnumerator EndAnim()
	{

		aura.transform.DOShakePosition(2.5f,0.1f,10,90,false,false);
		
		yield return new WaitForSeconds(2.8f);

		for (int i = 3; i < 15; i++)
		{
			aura.SetActive(false);
			yield return new WaitForSeconds(1.0f/i);	
			aura.SetActive(true);
		}
		
		Destroy(aura);

		Destroy(GetComponent<CircleCollider2D>());
	}
}
