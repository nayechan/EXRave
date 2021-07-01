using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollEventChecker : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool isReleased;

    // Start is called before the first frame update
    void Start()
    {
        isReleased = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        isReleased = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        isReleased = true;
    }
}
