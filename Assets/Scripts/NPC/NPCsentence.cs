using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCsentence : MonoBehaviour
{
    // NPC가 말할 문장들을 저장하는 배열
    public string[] sentences;

    // 플레이어가 대화 범위 안에 있는지를 나타내는 플래그
    bool isprint = false;

    // 대화가 진행 중인지 여부를 나타내는 플래그
    bool isDialogueActive = false;

    // 플레이어가 대화 범위 안으로 들어올 때 호출되는 메서드
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 객체가 "Player" 태그를 가진 경우
        if (collision.gameObject.tag == "Player")
        {
            // 플레이어가 대화 범위 안에 들어왔으므로 isprint를 true로 설정
            isprint = true;
        }
    }

    // 플레이어가 대화 범위를 벗어날 때 호출되는 메서드
    private void OnTriggerExit2D(Collider2D collision)
    {
        // 플레이어가 대화 범위를 벗어나면 isprint를 false로 설정
        isprint = false;

        // 충돌한 객체가 "Player" 태그를 가진 경우
        if (collision.gameObject.tag == "Player")
        {
            // 대화가 진행 중일 때 대화창을 닫음
            Managers.Dialogue.CloseDialogue();
        }
    }

    // 매 프레임 호출되며, 플레이어 입력을 감지
    private void Update()
    {
        // 플레이어가 대화 범위 안에 있고 'G' 키를 눌렀으며 대화가 진행 중이지 않을 때
        if (isprint && Input.GetKeyDown(KeyCode.G) && !isDialogueActive)
        {
            // 대화가 진행 중이지 않다면 대화를 시작하도록 DialogManager에 명령
            isDialogueActive = true;
            Managers.Dialogue.Ondialogue(sentences);
        }

        // 대화가 진행 중일 때 대화가 끝났다고 판단되는 경우
        if (isDialogueActive && Managers.Dialogue.IsDialogueFinished())
        {
            // 대화가 끝났으므로 isDialogueActive를 false로 설정
            isDialogueActive = false;
        }
    }
}