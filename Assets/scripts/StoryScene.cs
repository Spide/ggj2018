using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryScene : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		StartCoroutine("CountToEnd");
	}
	
	private IEnumerator CountToEnd()
	{
		yield return new WaitForSeconds(7 * 2.3f);
		SceneManager.UnloadSceneAsync("story");
	}
}
