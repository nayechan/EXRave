using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shutter : MonoBehaviour
{
    GameObject l, r;
    Vector3 lp, rp;
    float amount = 0;
    string bs;
    bool shouldOpened = false;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("st") != null)
            Destroy(gameObject);

        gameObject.name = "st";
        DontDestroyOnLoad(gameObject);

        l = transform.Find("shutterl").gameObject;
        r = transform.Find("shutterr").gameObject;

        lp = l.GetComponent<RectTransform>().anchoredPosition;
        rp = r.GetComponent<RectTransform>().anchoredPosition;


    }
    private void Update()
    {
        string cs = SceneManager.GetActiveScene().name;
        if (bs!=cs && shouldOpened)
        {
            shouldOpened = !DisableShutterWithValue();
        }
    }

    public bool EnableShutterWithValue()
    {

        amount += (Time.deltaTime * 2.0f);
        if (amount >= 1) amount = 1;

        l.GetComponent<RectTransform>().anchoredPosition = new Vector2(lp.x + (amount * 2880), lp.y);
        r.GetComponent<RectTransform>().anchoredPosition = new Vector2(rp.x - (amount * 2880), rp.y);
        
        if (amount == 1)
        {
            amount = 0;
            l.GetComponent<RectTransform>().anchoredPosition = new Vector2(-960, 0);
            r.GetComponent<RectTransform>().anchoredPosition = new Vector2(960, 0);
            lp = l.GetComponent<RectTransform>().anchoredPosition;
            rp = r.GetComponent<RectTransform>().anchoredPosition;
            bs = SceneManager.GetActiveScene().name;
            shouldOpened = true;
            return true;
        }
        else return false;
    }

    public bool DisableShutterWithValue()
    {

        Debug.Log("juu");
        amount += (Time.deltaTime * 2.0f);

        if (amount >= 1) amount = 1;
        l.GetComponent<RectTransform>().anchoredPosition = new Vector2(lp.x - (amount * 2880), lp.y);
        r.GetComponent<RectTransform>().anchoredPosition = new Vector2(rp.x + (amount * 2880), rp.y);


        if (amount == 1)
        {
            amount = 0;
            l.GetComponent<RectTransform>().anchoredPosition = new Vector2(-3840, 0);
            r.GetComponent<RectTransform>().anchoredPosition = new Vector2(3840, 0);
            lp = l.GetComponent<RectTransform>().anchoredPosition;
            rp = r.GetComponent<RectTransform>().anchoredPosition;
            return true;
        }
        else return false;
    }
}
