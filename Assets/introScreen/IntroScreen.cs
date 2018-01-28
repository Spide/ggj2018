using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScreen : MonoBehaviour
{

	public GameObject logo;
	// Use this for initialization
	void Start () {
		logo.DOTween
	}
	
	// Update is called once per frame
	public void DOPLAY () {
		SceneManager.LoadScene("arkanoid");
		SceneManager.LoadScene("testscene",LoadSceneMode.Additive);
	}
}
