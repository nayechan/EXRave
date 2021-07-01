using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadNextScene_main : MonoBehaviour
{
    bool isStart = false, inited = false, shuttered = false, scenetrans = false;
    public string scene;


    private void Update()
    {
        if (isStart)
        {
            if (!inited)
            {
                inited = true;
                //GameObject.Find("audio").GetComponent<AudioSource>().Stop();
            }
            else if (!shuttered)
            {
                shuttered = GameObject.Find("st").GetComponent<Shutter>().EnableShutterWithValue();
            }
            else if (!scenetrans)
            {
                StartCoroutine(LoadSceneAsyncByName(scene));
                scenetrans = true;
            }
        }
    }

    public void OnStartMyScene(string scene)
    {
        this.scene = scene;
        isStart = true;
    }

    public static IEnumerator LoadSceneAsyncByName(string nextLevel)
    {
        AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(nextLevel);

        while (!async.isDone)
        {
            if (async.progress > 0.88)
            {
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}

