using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScDragable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] GameObject inGameObject;
    [SerializeField] Sprite inGameSprite;
    private Sprite mySprite;
    private Transform originalParent;
    private UnityEngine.UI.Image myImage;

    private void Start()
    {
        myImage = GetComponent<UnityEngine.UI.Image>();
        mySprite = myImage.sprite;
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ScBuildPreview.Instance.DragStart(inGameSprite);

        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        myImage.raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        ScBuildPreview.Instance.Draging();

        if (ScBuildPreview.Instance.GetCurentMask() != mask.onMap)
        {
            transform.position = Input.mousePosition;
            myImage.enabled = true;
        }
        else
            myImage.enabled = false;
            
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        ScBuildPreview.Instance.DragEnds(inGameObject);
        transform.SetParent(originalParent);
        transform.SetAsFirstSibling();
        transform.position = originalParent.position;
        myImage.raycastTarget = true;
        myImage.enabled = true;
    }
    
}
