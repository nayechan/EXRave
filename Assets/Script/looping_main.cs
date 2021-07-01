using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class looping_main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos;
        pos = GetComponent<RectTransform>().localPosition;
        pos.x -= (Time.deltaTime * 150.0f);
        if (pos.x < -1280) pos.x = 1280;
        GetComponent<RectTransform>().localPosition = pos;
    }
}
