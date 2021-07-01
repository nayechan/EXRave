using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameExit : MonoBehaviour {
	void Update () {
		if (Input.GetKey(KeyCode.Escape)) _Quit();
	}

	void _Quit()
	{
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#elif UNITY_WEBPLAYER
			Application.OpenURL("about:blank");
		#else
			Application.Quit();
		#endif
	}
}