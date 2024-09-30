using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers instance;
    private static Managers Instance { get { init(); return instance; } }

    InputManager input = new InputManager();
    DialogueManager dialogue;
    PoolManager pool;

    public static InputManager Input { get { return Instance.input; } }
    public static DialogueManager Dialogue { get { return Instance.dialogue; } }
    public static PoolManager Pool { get { return Instance.pool; } }

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
        if (instance == null)
        {
            GameObject newObj = GameObject.Find("Managers");

            if (newObj == null)
            {
                newObj = new GameObject { name = "Managers" };
                newObj.AddComponent<Managers>();
            }
            DontDestroyOnLoad(newObj);
            instance = newObj.GetComponent<Managers>();

            if (instance.dialogue == null)
            {
                GameObject dialogueObj = new GameObject("DialogueManager");
                dialogueObj.transform.parent = newObj.transform;
                instance.dialogue = dialogueObj.AddComponent<DialogueManager>();
            }

            if (instance.pool == null)
            {
                GameObject poolObj = new GameObject("PoolManager");
                poolObj.transform.parent = newObj.transform;
                instance.pool = poolObj.AddComponent<PoolManager>();
            }
        }
    }
}

