using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    // Managers Singleton Pattern ????
    // Game?? ?????? ?????????? ???? ?????? InputManager, GameManager, DataManager ?? ?? Manager ?????? ???? Script?? ????
    private static Managers instance;
    private static Managers Instance { get { init(); return instance; } }

    // ?????? Manager ????
    InputManager input = new InputManager();
    DialogueManager dialogue = new DialogueManager(); // 인스턴스 없이 참조만 할 수 있도록 하기 위해 이렇게 선언함
    // GameManager gameManager = new GameManager();

    // ?????? Manager Instance ??
    public static InputManager Input { get { return Instance.input; } }
    public static DialogueManager Dialogue { get { return Instance.dialogue; } }
    // public static GameManager GameManager {get{return Instance.gameManager;}}
    
    
    void Start()
    {
        init();
        //dialogue = DialogueManager.instance; // DialogueManager의 인스턴스를 참조
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
        }

    }
}
