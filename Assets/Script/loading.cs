using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class loading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().color = new Color(0, 0, 0, 1);
        transform.Find("icon").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        transform.Find("Text").GetComponent<Text>().text = "Loading";

    }

    // Update is called once per frame
    void Update()
    {
        transform.Find("icon").Rotate(new Vector3(0, 0, 1), -180*Time.deltaTime);
        if(GameObject.Find("video_bg").GetComponent<VideoPlayer>().isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
