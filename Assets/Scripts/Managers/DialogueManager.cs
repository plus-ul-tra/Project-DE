using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // 대화 내용을 표시할 UI 텍스트 컴포넌트
    public Text dialogueText;

    // "다음" 버튼 또는 텍스트를 나타내는 게임 오브젝트
    public GameObject nextText;

    // 대화창의 UI 요소를 제어하기 위한 CanvasGroup
    public CanvasGroup dialogueGroupe;

    // 대화 내용을 저장할 큐(Queue) 구조
    public Queue<string> sentences;

    // 현재 출력 중인 문장
    private string currentSentence;

    // 타이핑 속도 설정
    public float typingSpeed = 0.1f;

    // 현재 타이핑 중인지 여부를 나타내는 플래그
    private bool isTyping;
    private bool isInitialized = false;

    // 싱글톤 패턴을 위한 인스턴스 변수
    //public static DialogueManager instance;

    // 플레이어 컨트롤을 제어하는 스크립트 참조
    private PlayerController playerMove;

    // 싱글톤 인스턴스를 설정하는 메서드
    private void Awake()
    {
        playerMove = FindObjectOfType<PlayerController>();
        GameObject dialogueObj = GameObject.Find("Dialogue");
        GameObject dialogueTextObj = GameObject.Find("Dialogue Text");

        nextText = GameObject.Find("Next Text");
        dialogueGroupe = dialogueObj.GetComponent<CanvasGroup>();
        dialogueText =dialogueTextObj.GetComponent<Text>();
    }

    // 대화 큐 초기화
    void Start()
    {
        sentences = new Queue<string>();
    }

    // 대화 시작 메서드
    // lines 배열을 받아 큐에 저장하고 첫 번째 문장을 출력
    public void Ondialogue(string[] lines)
    {
        sentences.Clear(); // 이전 대화 내용을 지움
        foreach (string line in lines)
        {
            sentences.Enqueue(line); // 새 대화 내용을 큐에 추가
        }
        dialogueGroupe.alpha = 1; // 대화창을 보이도록 설정
        dialogueGroupe.blocksRaycasts = true; // 대화창이 사용자와 상호작용 가능하도록 설정
        playerMove.enabled = false; // 플레이어의 움직임을 비활성화
        isInitialized= true;
        NextSentences(); // 첫 번째 문장 출력
    }

    // 다음 문장을 출력하는 메서드
    public void NextSentences()
    {
        if (sentences.Count != 0) // 아직 출력할 문장이 남아 있는 경우
        {
            currentSentence = sentences.Dequeue(); // 큐에서 다음 문장을 꺼냄
            isTyping = true; // 타이핑 중으로 설정
            nextText.SetActive(false); // "다음" 텍스트 비활성화
            StartCoroutine(Typing(currentSentence)); // 타이핑 효과를 시작
        }
        else
        {
            CloseDialogue(); // 모든 문장이 출력되면 대화창을 닫음
        }
    }

    // 한 글자씩 타이핑 효과를 구현하는 코루틴 메서드
    IEnumerator Typing(string line)
    {
        dialogueText.text = ""; // 텍스트를 초기화
        foreach (char letter in line.ToCharArray()) // 문장을 한 글자씩 순회
        {
            dialogueText.text += letter; // 텍스트에 글자 추가
            yield return new WaitForSeconds(typingSpeed); // 타이핑 속도에 맞춰 대기
        }
    }

    // 매 프레임 호출되는 메서드
    void Update()
    {
        if (!isInitialized)
        {
            return;
        }

        // 대화가 모두 출력되면 "다음" 버튼 활성화
        if (dialogueText.text.Equals(currentSentence))
        {
            nextText.SetActive(true); // "다음" 텍스트 활성화
            isTyping = false; // 타이핑 완료
        }

        // Space 키 입력이 있고 타이핑 중이 아닐 때 다음 문장을 출력
        if (Input.GetKeyDown(KeyCode.Space) && !isTyping)
        {
            NextSentences();
        }
    }

    // 대화창을 닫는 메서드
    public void CloseDialogue()
    {
        dialogueGroupe.alpha = 0; // 대화창을 보이지 않게 설정
        dialogueGroupe.blocksRaycasts = false; // 대화창이 사용자와 상호작용하지 않도록 설정
        dialogueText.text = ""; // 대화 텍스트 초기화
        nextText.SetActive(false); // "다음" 텍스트 비활성화
        sentences.Clear(); // 남아 있는 문장을 모두 지움
        playerMove.enabled = true; // 플레이어의 움직임을 재활성화
    }
}