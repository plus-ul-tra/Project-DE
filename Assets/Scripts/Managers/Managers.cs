using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    // Managers Singleton Pattern 
    
    private static Managers instance;
    private static Managers Instance { get { init(); return instance; } }

    // ?????? Manager ????
    InputManager input = new InputManager();
    DialogueManager dialogue; 
    // GameManager gameManager = new GameManager();

    // ?????? Manager Instance ??
    public static InputManager Input { get { return Instance.input; } }
    public static DialogueManager Dialogue { get { return Instance.dialogue; } }
    // public static GameManager GameManager {get{return Instance.gameManager;}}
    
    
    void Start()
    {
        init();


    }

    
    void Update()
    {
        input.OnUpdate();
    }

    // ?? ???????? ???????? Managers Script?? ????
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
            if (instance.dialogue == null)
            {
                GameObject dialogueObj = new GameObject("DialogueManager");
                dialogueObj.transform.parent = newObj.transform;  // Managers의 자식으로 설정
                instance.dialogue = dialogueObj.AddComponent<DialogueManager>();
            }
        }

    }
}
