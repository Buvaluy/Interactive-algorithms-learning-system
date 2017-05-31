using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragForHard : MonoBehaviour {

    private bool dragging = false;
    private Color hoverColor = Color.white;

    private Vector3 screenSpace;
    private Vector3 offset;
    private Color originalColor;
    private Renderer renderer;

    // Use this for initialization
    void Start () 
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
    }

    // Update is called once per frame
    void Update () 
    {
        if (dragging)
        {
            var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
            curPosition.z = -3;
            transform.position = curPosition;
        }
    }

    void OnMouseEnter()
    {
        renderer.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        renderer.material.color = originalColor;
    }

    void OnMouseDown()
    {
        dragging = true;

        screenSpace = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
    }

    void OnMouseUp()
    {
        dragging = false;
        var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
        var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
        curPosition.z = 1;
        transform.position = curPosition;
    }
}
