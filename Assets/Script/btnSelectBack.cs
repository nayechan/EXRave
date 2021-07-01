using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnSelectBack : MonoBehaviour
{
    public chartManager chartManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void btnClicked()
    {
        switch (chartManager.currentDisplay)
        {
            case 0:
                chartManager.GetComponent<LoadNextScene_main>().OnStartMyScene("main");
                break;
            case 1:
                chartManager.ChangeDisplayMode(1);
                break;
            default:
                break;
        }
    }
}
