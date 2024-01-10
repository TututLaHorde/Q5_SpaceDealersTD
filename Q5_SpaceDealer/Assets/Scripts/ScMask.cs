using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScMask : MonoBehaviour, IPointerEnterHandler
{
    public mask myMask;


    public void OnPointerEnter(PointerEventData eventData)
    {
        ScBuildPreview.Instance.SetCurentMask(myMask);
    }
}
