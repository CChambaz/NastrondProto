﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDropSystem : MonoBehaviour,IDragHandler,IEndDragHandler
{

    public GameObject buildingSelected;
    

    public void OnDrag(PointerEventData eventData)
    {
        if(buildingSelected!=null)
        {

        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
