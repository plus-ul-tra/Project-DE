using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    // Managers Singleton Pattern 적용
    // Game상 하나만 존재하도록 보장 그래서 InputManager, GameManager, DataManager 등 의 Manager 생성은 여기 Script에 진행
    private static Managers instance;
    private static Managers Instance { get { init(); return instance; } }

    // 사용할 Manager 생성
    InputManager input = new InputManager();
    // GameManager gameManager = new GameManager();

    // 사용할 Manager Instance 화
    public static InputManager Input { get { return Instance.input; } }
    // public static GameManager GameManager {get{return Instance.gameManager;}}
    
    
    void Start()
    {
        init();
    }

    
    void Update()
    {
        input.OnUpdate();
    }

    // 빈 오브젝트 만들어서 Managers Script에 연결
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
