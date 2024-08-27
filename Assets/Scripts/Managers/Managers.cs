using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    // Managers Singleton Pattern ����
    // Game�� �ϳ��� �����ϵ��� ���� �׷��� InputManager, GameManager, DataManager �� �� Manager ������ ���� Script�� ����
    private static Managers instance;
    private static Managers Instance { get { init(); return instance; } }

    // ����� Manager ����
    InputManager input = new InputManager();
    // GameManager gameManager = new GameManager();

    // ����� Manager Instance ȭ
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

    // �� ������Ʈ ���� Managers Script�� ����
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
