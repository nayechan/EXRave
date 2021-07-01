using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class next_room2 : MonoBehaviour
{
    public string stagename;
    // Use this for initialization
    public void next()
    {
        GameObject.Find("Canvas").transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Loading";
        SceneManager.LoadScene(stagename);
    }
}
