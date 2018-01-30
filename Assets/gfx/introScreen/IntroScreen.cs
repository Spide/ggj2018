using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScreen : MonoBehaviour
{
	public SpriteRenderer black;

	public SpriteRenderer logo;

	// Use this for initialization
	void Start()
	{
		logo.DOFade(1, 1).SetDelay(0.5f);
	}

	// Update is called once per frame
	public void DOPLAY()
	{
		black.gameObject.SetActive(true);
		black.color = new Color(0f, 0f, 0f, 0f);
		StartCoroutine("DoSwitch");
		black.DOFade(1f, 0.4f);
	}

	private IEnumerator DoSwitch()
	{
		yield return new WaitForSeconds(0.4f);
		SceneManager.LoadScene("arkanoid");
		SceneManager.LoadScene("testscene", LoadSceneMode.Additive);
	}

	// Update is called once per frame
	public void DOStory()
	{
		black.gameObject.SetActive(true);
		black.color = new Color(0f, 0f, 0f, 0f);
		StartCoroutine("DoSwitchStory");
		black.DOFade(1f, 0.4f);
	}

	private IEnumerator DoSwitchStory()
	{
		yield return new WaitForSeconds(0.4f);
		SceneManager.LoadSceneAsync("story",LoadSceneMode.Additive);
		yield return new WaitForSeconds(0.4f);
		black.gameObject.SetActive(false);
	}

	public void EXIT()
	{
		Application.Quit();
	}
}
