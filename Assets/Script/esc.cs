using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class esc : MonoBehaviour {
    public string X;
    bool isStart = false,  shuttered = false, scenetrans = false;


    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) Manual();
        if (isStart)
        {
            
            if (!shuttered)
            {
                shuttered = GameObject.Find("st").GetComponent<Shutter>().EnableShutterWithValue();
            }
            else if (!scenetrans)
            {
                StartCoroutine(LoadSceneAsyncByName(X));
                scenetrans = true;
            }
        }
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

    public void Manual()
    {
        isStart = true;
    }

    public void Manual2()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(X);
    }
}
