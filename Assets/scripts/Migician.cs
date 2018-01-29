using System.Collections;
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
		if (aura == null)
		{
			yield break;
		}
		
		var t = Camera.main.transform.DOShakePosition(0.9f, 0.1f,20,90f,false,false);
		t.timeScale = 1.5f;
		
		yield return new WaitForSeconds(1f);
		
		t = aura.transform.DOShakePosition(1f, 0.1f,20,90f,false,false);
		t.timeScale = 1.5f;
		
		yield return new WaitForSeconds(1.1f);

		
		aura.SetActive(false);
		yield return new WaitForSeconds(0.3f);
		aura.SetActive(true);
		yield return new WaitForSeconds(0.2f);
		aura.SetActive(false);		
		yield return new WaitForSeconds(0.08f);
		aura.SetActive(true);
		yield return new WaitForSeconds(0.08f);

		Destroy(aura);

		Destroy(GetComponent<CircleCollider2D>());
	}
}
