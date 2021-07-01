using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class freeplay_startwithfile : MonoBehaviour {

    public string filename;
    public string diff;

    public void _start()
    {
        if (filename != "select" && diff!="diff")
        {
            GameObject.Find("TitleText").GetComponent<Text>().text = "LOADING";
            PlayerPrefs.SetString("TargetChart",filename);
            PlayerPrefs.SetString("TargetDiff",diff);
            PlayerPrefs.Save();
            SceneManager.LoadScene("inGame");
        }
        else if (filename == "select")
        {
            GameObject.Find("TitleText").GetComponent<Text>().text = "PLEASE SELECT SONG";

        }
        else if (diff == "diff")
        {
            
        }
    }
}
