using System.Runtime.CompilerServices;
using UnityEngine;

public class curserUi : MonoBehaviour
{
    private RectTransform curserTr;
    private Canvas parent;
    private RectTransform canvasTr;
    private Camera canvasCamera;

    private void Awake()
    {
        curserTr = GetComponent<RectTransform>();
        parent = GetComponentInParent<Canvas>();
        canvasTr = parent.GetComponent<RectTransform>();
        canvasCamera = parent.worldCamera;
            Cursor.visible = false;
    }

    private void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTr, mousePos, canvasCamera, out localPoint))
        {
            curserTr.anchoredPosition = localPoint;
        }
    }
}
