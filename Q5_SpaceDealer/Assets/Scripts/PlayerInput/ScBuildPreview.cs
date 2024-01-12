using UnityEngine;
using UnityEngine.Events;

public class ScBuildPreview : MonoBehaviour
{
    public static ScBuildPreview Instance;
    public UnityEvent dragBegins;
    public UnityEvent dragEnded;

    [SerializeField] GameObject dragable;
    [SerializeField] Camera mainCamera;
    [SerializeField] int rangeSubdivisionCount;

    private mask currentMask;
    private SpriteRenderer dragablesprite;
    private turretInfo newTurretInfos;
    private LineRenderer myLine;
    private float stepAngle;
    private float turretRange;

    private Vector3 mousePosOnScreen;
    private Vector3 mousePosInWorld;
    
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
        myLine = GetComponent<LineRenderer>();

        stepAngle = 2f * Mathf.PI / rangeSubdivisionCount;
        myLine.positionCount = rangeSubdivisionCount+1;
    }

    private void Update()
    {
        mousePosOnScreen = UnityEngine.Input.mousePosition;
        mousePosInWorld = mainCamera.ScreenToWorldPoint(mousePosOnScreen);
    }

    public void DragStart(turretInfo newBuildInfos)
    {
        newTurretInfos = newBuildInfos;
        dragablesprite.sprite = newTurretInfos.sprite;
        dragBegins.Invoke();
        myLine.positionCount = rangeSubdivisionCount + 1;
        turretRange = newTurretInfos.go.GetComponent<ScTurret>().GetRange();
    }
    public void Draging()
    {
        dragable.transform.position = mainCamera.ScreenToWorldPoint(mousePosOnScreen) + new Vector3(0, 0, 2);

        if (currentMask == mask.onMap)
        {
            if (myLine.positionCount == 0)
            {
                myLine.positionCount = rangeSubdivisionCount + 1;
            }
            DisplayTurretRange();
        }
        else
        {
            myLine.positionCount = 0;
        }
    }
    public void DragEnds()
    {
        RaycastHit2D result = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(mousePosOnScreen), Vector2.zero);


        if (result.collider != null)
        {
            if (result.transform.gameObject.layer == 7)
            {
                Vector3 spawnPoint = new Vector3(mainCamera.ScreenToWorldPoint(mousePosOnScreen).x, mainCamera.ScreenToWorldPoint(mousePosOnScreen).y, 0);
                GameObject tempo =  GameObject.Instantiate(newTurretInfos.go, spawnPoint, Quaternion.identity);
                
                ObstaclesManager.instance.m_obstacles.Add(tempo.GetComponentInChildren<ObstacleController>());
            }
        }

        dragEnded.Invoke();
        dragable.transform.position = new Vector3(-500,-500,0);
        myLine.positionCount = 0;
    }

    private void DisplayTurretRange()
    {
        for (int i=0; i<rangeSubdivisionCount+1;i++)
        {
            float x = turretRange * Mathf.Cos(stepAngle * i);
            float y = turretRange * Mathf.Sin(stepAngle * i);

            myLine.SetPosition(i, new Vector3(mousePosInWorld.x + x, mousePosInWorld.y + y,0) );
        }
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
