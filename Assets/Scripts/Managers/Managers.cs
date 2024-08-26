using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers instance;
    private static Managers Instance { get { init(); return instance; } }   

    InputManager input = new InputManager();

    public static InputManager Input { get { return Instance.input; } }
    
    void Start()
    {
        init();
    }

    
    void Update()
    {
        input.OnUpdate();
    }

    static void init()
    {
        if(instance == null)
        {
            GameObject newObj = GameObject.Find("Managers");

            if(newObj == null)
            {
                newObj = new GameObject { name = "Managers" };
                newObj.AddComponent<Managers>();
            }
            DontDestroyOnLoad(newObj);
            instance = newObj.GetComponent<Managers>();
        }

    }
}
