using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Vector2 hotspot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;
    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotspot, cursorMode);
    }
    void Update()
    {
        Debug.Log(gameObject.name);
    }
}
