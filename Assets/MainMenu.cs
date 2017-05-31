using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    private bool dragging = false;
    private bool bStartCoord;
    private Color hoverColor = Color.white;

    public static string nickName;
    public InputField inputField;

    private Vector3 startPos;
    private Vector3 screenSpace;
    private Vector3 offset;
    private Color originalColor;
    private Renderer renderer;

    // Use this for initialization
    void Start () 
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
        if (SceneManager.GetActiveScene().name == "ScoreAfterFin")
        {
            Text []objText = FindObjectsOfType<Text>();// .Find("Text1");
            objText[0].text = nickName;
        }
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
            if (bStartCoord)
            {
                bStartCoord = false;
                startPos = curPosition;
            }
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
        bStartCoord = true;
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

        if (curPosition.x == startPos.x && curPosition.y == startPos.y)
        {
            string name = gameObject.name;
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.UnloadSceneAsync(sceneName);
            if (name == "hardPazzle")
            {
                SceneManager.LoadSceneAsync("hardLvl1");
            }
            else if (name == "simplePuzzle")
            {
                SceneManager.LoadSceneAsync("lowLvl1");
            }
            else if (name == "score")
            {
                SceneManager.LoadSceneAsync("Score");
            }
            else if (name == "return")
            {
                Application.Quit();
            }
            else if (name == "returnMainMenu")
            {
                SceneManager.LoadSceneAsync("main");
            }
            else if (name == "ok")
            {
                
                nickName = inputField.text;
                SceneManager.LoadSceneAsync("main");
            }
            else// if (name == "diplomWork")
            {

            }
        }
    }
}
