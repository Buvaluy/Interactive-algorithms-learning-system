using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DragAndHighlight : MonoBehaviour 
{

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

        dragging = false;
        var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
        var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
        curPosition.z = 1;
        transform.position = curPosition;

        GameObject[] objs = GameObject.FindGameObjectsWithTag ("pazl");

        // Сортировка массива пузырьком
        for (int i = 0; i < objs.Length - 1; i++) {
            for (int j = 0; j < objs.Length - i - 1; j++) {
                if (Int32.Parse(objs[j].name) > Int32.Parse(objs[j + 1].name)) {
                    var temp = objs[j];
                    objs[j] = objs[j + 1];
                    objs[j + 1] = temp;
                }
            }
        }

        var curName = name;
        var curX = transform.position.x;
        var curY = transform.position.y;
        int numInArray = 0;
        for (; numInArray < objs.Length; ++numInArray)
        {
            if (curName == objs[numInArray].name)
                break;
        }
        if (numInArray != 0 && numInArray != objs.Length - 1)
        {
            var upperX = objs[numInArray - 1].transform.position.x;
            var upperY = objs[numInArray - 1].transform.position.y;

            var lowerX = objs[numInArray + 1].transform.position.x;
            var lowerY = objs[numInArray + 1].transform.position.y;

            var withLowerX = Math.Abs(lowerX - curX);
            var withLowerY = Math.Abs(lowerY - curY);

            var withUpperX = Math.Abs(upperX - curX);
            var withUpperY = Math.Abs(upperY - curY);

            if (withLowerX < 0.2 && withLowerY < 0.2 && withUpperX < 0.2 && withUpperY < 0.2)
            {
                if (withLowerX < withUpperX && withLowerY < withUpperY)
                {
                    var tempPosX = objs[numInArray + 1].transform.position.x;
                    var tempPosY = objs[numInArray + 1].transform.position.y;
                    var tempPosZ = objs[numInArray + 1].transform.position.z;
                    transform.position = new Vector3(tempPosX, tempPosY, tempPosZ); //.x = tempPosX;
                }
                else
                {
                    var tempPosX = objs[numInArray - 1].transform.position.x;
                    var tempPosY = objs[numInArray - 1].transform.position.y;
                    var tempPosZ = objs[numInArray - 1].transform.position.z;
                    transform.position = new Vector3(tempPosX, tempPosY, tempPosZ); //.x = tempPosX;
                }
            }
            else if (withLowerX < 0.2 && withLowerY < 0.2)
            {
                var tempPosX = objs[numInArray + 1].transform.position.x;
                var tempPosY = objs[numInArray + 1].transform.position.y;
                var tempPosZ = objs[numInArray + 1].transform.position.z;
                transform.position = new Vector3(tempPosX, tempPosY, tempPosZ); //.x = tempPosX;
            }
            else if(withUpperX < 0.2 && withUpperY < 0.2)
            {
                var tempPosX = objs[numInArray - 1].transform.position.x;
                var tempPosY = objs[numInArray - 1].transform.position.y;
                var tempPosZ = objs[numInArray - 1].transform.position.z;
                transform.position = new Vector3(tempPosX, tempPosY, tempPosZ); //.x = tempPosX;
            }


        }
        else if(numInArray == 0)
        {
            var lowerX = objs[numInArray + 1].transform.position.x;
            var lowerY = objs[numInArray + 1].transform.position.y;
            if (Math.Abs(lowerX - curX) < 0.3 && Math.Abs(lowerY - curY) < 0.2)
            {
                var tempPosX = objs[numInArray + 1].transform.position.x;
                var tempPosY = objs[numInArray + 1].transform.position.y;
                var tempPosZ = objs[numInArray + 1].transform.position.z;
                transform.position = new Vector3(tempPosX, tempPosY, tempPosZ); //.x = tempPosX;
            }
        }
        else
        {
            var upperX = objs[numInArray - 1].transform.position.x;
            var upperY = objs[numInArray - 1].transform.position.y;
            if (Math.Abs(upperX - curX) < 0.3 && Math.Abs(upperY - curY) < 0.2)
            {
                var tempPosX = objs[numInArray - 1].transform.position.x;
                var tempPosY = objs[numInArray - 1].transform.position.y;
                var tempPosZ = objs[numInArray - 1].transform.position.z;
                transform.position = new Vector3(tempPosX, tempPosY, tempPosZ); //.x = tempPosX;
            }
        }

    }
}
