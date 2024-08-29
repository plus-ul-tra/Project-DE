using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCsentence : MonoBehaviour
{
    
    // NPC가 말할 문장들을 저장하는 배열
    public string[] sentences;

    // 플레이어가 대화 범위 안에 있는지를 나타내는 플래그
    bool isprint = false;

    // 플레이어가 대화 범위 안으로 들어올 때 호출되는 메서드
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 객체가 "Player" 태그를 가진 경우
        if (collision.gameObject.tag == "Player")
        {
            // isprint를 true로 설정하여 대화가 가능하도록 함
            isprint = true;
        }
    }

    // 플레이어가 대화 범위를 벗어날 때 호출되는 메서드
    private void OnTriggerExit2D(Collider2D collision)
    {
        isprint = false;
        // 충돌한 객체가 "Player" 태그를 가진 경우
        if (collision.gameObject.tag == "Player")
        {
            // 대화창을 닫음
            Managers.Dialogue.CloseDialogue();
        }
    }

    // 매 프레임 호출되며, 플레이어 입력을 감지
    private void Update()
    {
        // 플레이어가 대화 범위 안에 있고 'G' 키를 눌렀을 때
        if (isprint == true && Input.GetKeyDown(KeyCode.G))
        {
            // NPC의 대사를 출력하도록 DialogManager에 명령
             Managers.Dialogue.Ondialogue(sentences);
            //DialogueManager.instance.Ondialogue(sentences);
        }
    }
}