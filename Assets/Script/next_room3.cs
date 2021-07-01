using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class next_room3 : MonoBehaviour
{
    public string stagename;
    // Use this for initialization
    public void next()
    {
        SceneManager.LoadScene(stagename);
    }
}
