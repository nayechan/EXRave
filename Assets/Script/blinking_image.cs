using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blinking_image : MonoBehaviour
{
    int sw = 1;
    float color = 0.75f;
    public bool is_enabled = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(is_enabled)
        {
            color += sw * Time.deltaTime / (2.5f);
            if (color > 0.75f)
            {
                sw *= -1;
                color = 0.75f;
            }
            if (color < 0.25f)
            {
                sw *= -1;
                color = 0.25f;
            }
            GetComponent<Image>().color = new Color(1, 1, 1, color);
        }
        else
        {
            color = 0.75f;
            GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        
    }
}
