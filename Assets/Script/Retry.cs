using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour {

	// Use this for initialization
	public void Manual()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("inGame");
	}
}
