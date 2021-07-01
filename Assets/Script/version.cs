using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class version : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = "version "+Application.version;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
