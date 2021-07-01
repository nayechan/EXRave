using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderAction : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector3 pos_init;
    public bool orientation; // false = hor, true = ver
    public float maxDragAmount;
    public float actionRatio;

    public delegate void dragAction();
    public static event dragAction OnDraged;

    public Text caption;

    // Start is called before the first frame update
    void Start()
    {
        pos_init = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = Input.mousePosition;
        float ratio;
        if (!orientation)
        {
            pos.y = transform.position.y;
            if (pos.x < pos_init.x) pos.x = pos_init.x;
            if (pos.x > pos_init.x + maxDragAmount) pos.x = pos_init.x + maxDragAmount;

            ratio = (pos.x - pos_init.x) / maxDragAmount / actionRatio;
        }
        else
        {
            pos.x = transform.position.x;
            if (pos.y < pos_init.y) pos.y = pos_init.y;
            if (pos.y > pos_init.y + maxDragAmount) pos.y = pos_init.y + maxDragAmount;

            ratio = (pos.y - pos_init.y) / maxDragAmount / actionRatio;
        }
        ratio = 1 - ratio;
        if (ratio > 1) ratio = 1; if (ratio < 0) ratio = 0;
        transform.position = pos;
        caption.color = new Color(1, 1, 1, ratio);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
       
        if (!orientation)
        {
            if (transform.position.x > pos_init.x + (maxDragAmount * actionRatio))
            {
                GetComponent<LoadSelect>().enabled = true;
                OnDraged();
            }
        }
        else
        {
            if (transform.position.y > pos_init.y + (maxDragAmount * actionRatio))
            {
                GetComponent<LoadSelect>().enabled = true;
                OnDraged();
            }
        }
        transform.position = pos_init;

        caption.color = new Color(1, 1, 1, 1);
    }
}
