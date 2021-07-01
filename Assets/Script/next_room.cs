using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class next_room : MonoBehaviour {

	// Use this for initialization
	public void next () {
        Debug.Log("Click");
        SceneManager.LoadScene("select_original");
    }
}
