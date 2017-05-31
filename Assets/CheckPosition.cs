using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class CheckPosition : MonoBehaviour {

    GameObject obj;
    private float timer = 0.0f;
    bool fin = false;
	// Use this for initialization
	void Start () {
        obj = GameObject.Find ("Yes");
        obj.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
        if (fin)
        {
            timer += Time.deltaTime;
            if (timer >= 3)
            {
                fin = false;
                obj.SetActive(false);
                string name = SceneManager.GetActiveScene().name;
                SceneManager.UnloadSceneAsync(name);
                char cl =  name.ToCharArray()[name.Length - 1];
                int level = Int32.Parse(cl.ToString());
                if (level == 2)
                {
                    SceneManager.LoadSceneAsync("ScoreAfterFin");
                    return;
                }
                ++level;
                name = name.Replace(name.ToCharArray()[name.Length - 1].ToString(), level.ToString());
                SceneManager.LoadSceneAsync(name);
            }
        }
	}

    bool checkOrder()
    {
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

        for (int i = 0; i < objs.Length - 1; ++i) 
        {
            string name1 = objs[i].name;
            string name2 = objs[i + 1].name;
            var pos1Y = objs[i].transform.position.y;
            var pos2Y = objs[i + 1].transform.position.y;
            var pos1X = objs[i].transform.position.x;
            var pos2X = objs[i + 1].transform.position.x;
            if (pos1X != pos2X || pos1Y != pos2Y)
                return false;
        }
        return true;
    }

    void OnMouseDown()
    {
        if (checkOrder())
        {
            obj.SetActive (true);
            fin = true;
        }
    }
}
