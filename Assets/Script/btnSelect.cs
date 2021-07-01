using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnSelect : MonoBehaviour
{
    public chartManager chartManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void btnClicked()
    {
        switch(chartManager.currentDisplay)
        {
            case 0:
                chartManager.ChangeDisplayMode(0);
                break;
            case 1:
                //start chart
                chartManager.startChart();
                break;
            default:
                break;
        }
    }
}
