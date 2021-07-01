using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour {
	public GameObject PauseUI;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnApplicationFocus(bool hasFocus)
    {
		Pause();
    }

    void OnApplicationPause(bool pauseStatus)
    {
		Pause();
    }

	

	public void Pause()
	{
		PauseUI.SetActive(true);
		Time.timeScale = 0;
		GetComponent<AudioSource>().Pause();
		//GetComponent<Touch001>().enabled = false;
	}

	public void Resume()
	{
		PauseUI.SetActive(false);
		Time.timeScale = 1;
        GetComponent<AudioSource>().Play();
    }
}
