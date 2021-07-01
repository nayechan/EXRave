using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSelect : MonoBehaviour
{
    public string scene;
    // Start is called before the first frame update
    void OnEnable()
    {
        SliderAction.OnDraged += Action;
    }


    void OnDisable()
    {
        SliderAction.OnDraged -= Action;
    }

    void Action()
    {
        GameObject.Find("MainManager").GetComponent<LoadNextScene_main>().OnStartMyScene(scene);
    }
}
