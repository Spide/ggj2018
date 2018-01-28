using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScreen : MonoBehaviour
{

	public SpriteRenderer logo;
	// Use this for initialization
	void Start ()
	{
		logo.DOFade(1, 1).SetDelay(0.5f);
	}
	
	// Update is called once per frame
	public void DOPLAY () {
		SceneManager.LoadScene("arkanoid");
		SceneManager.LoadScene("testscene",LoadSceneMode.Additive);
	}
}
