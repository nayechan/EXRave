using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        SliderAction.OnDraged += Action;
    }


    void OnDisable()
    {
        SliderAction.OnDraged -= Action;
    }

    void Action()
    {
        Debug.Log("asdf");
    }
}
