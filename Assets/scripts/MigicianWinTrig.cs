using DG.Tweening;
using UnityEngine;

public class MigicianWinTrig : MonoBehaviour {
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			var t = Camera.main.transform.DOShakePosition(0.9f, 0.1f, 20, 90f, false, false);
			t.timeScale = 1.5f;
			GameManager.instance.EndGame("Bear", "You did it!");
		}
	}
	
}
