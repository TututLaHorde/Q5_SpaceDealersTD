using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScBuildPreview : MonoBehaviour
{
    public static ScBuildPreview Instance;
    public UnityEvent dragBegins;
    public UnityEvent dragEnded;

    [SerializeField] GameObject dragable;
    [SerializeField] Camera mainCamera;

    private mask currentMask;
    private SpriteRenderer dragablesprite;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
    }
    private void Start()
    {
        dragablesprite = dragable.GetComponent<SpriteRenderer>();
        currentMask = mask.ui;
    }

    public void DragStart(Sprite dragableNewSprite)
    {
        dragablesprite.sprite = dragableNewSprite;
        dragBegins.Invoke();
    }
    public void Draging()
    {
        dragable.transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 2);
    }
    public void DragEnds(GameObject arsenalPiece)
    {
        RaycastHit2D result = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);


        if (result.collider != null)
        {
            if (result.transform.gameObject.layer == 7)
            {
                Debug.Log(result.transform.name);
                GameObject.Instantiate(arsenalPiece, mainCamera.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 2), Quaternion.identity);
            }
        }

        dragEnded.Invoke();
        dragable.transform.position = new Vector3(-500,-500,0);
    }

    public void SetCurentMask(mask newMask)
    {
        currentMask = newMask;
    }
    public mask GetCurentMask()
    {
        return currentMask;
    }
}

public enum mask
{
    onMap,
    onShop,
    ui
}
