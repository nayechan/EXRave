using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blinking_Text : MonoBehaviour
{
    int sw = 1;
    float color = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        color += sw*Time.deltaTime/(1.5f);
        if(color>=1)
        {
            sw *= -1;
            color = 1;
        }
        if(color<0)
        {
            sw *= -1;
            color = 0;
        }
        GetComponent<Text>().color = new Color(1,1,1,color);
    }
}
