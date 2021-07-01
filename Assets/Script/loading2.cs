using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class loading2 : MonoBehaviour
{
    bool activated = false;
    // Start is called before the first frame update
    public void iStart()
    {
        GetComponent<Image>().color = new Color(0, 0, 0, 1);
        transform.Find("load-text").GetComponent<Text>().text = "Loading";
        activated = true;

    }
}
