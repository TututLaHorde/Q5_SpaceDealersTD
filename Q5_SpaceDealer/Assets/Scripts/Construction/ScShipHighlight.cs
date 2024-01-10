using UnityEngine;
using UnityEngine.EventSystems;

public class ScShipHighlight : MonoBehaviour
{
    [SerializeField] GameObject highligtArea;

    private void Start()
    {
        ScBuildPreview.Instance.dragBegins.AddListener(EnableHighlight);
        ScBuildPreview.Instance.dragEnded.AddListener(DisableHighlight);
    }

    private void EnableHighlight()
    {
        highligtArea.SetActive(true);
    }

    private void DisableHighlight() 
    {
        highligtArea.SetActive(false);
    }

}
