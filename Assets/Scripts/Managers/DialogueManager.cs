using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public GameObject nextText;
    public CanvasGroup dialogueGroupe;
    public Queue<string> sentences;
    private string currentSentence;
    public float typingSpeed = 0.1f;
    private bool isTyping;
    private bool isInitialized = false;
    private bool skipTyping = false; // 스페이스바로 타이핑을 건너뛰는 플래그
    private PlayerController playerMove;

    private void Awake()
    {
        playerMove = FindObjectOfType<PlayerController>();
        GameObject dialogueObj = GameObject.Find("Dialogue");
        GameObject dialogueTextObj = GameObject.Find("Dialogue Text");

        nextText = GameObject.Find("Next Text");
        dialogueGroupe = dialogueObj.GetComponent<CanvasGroup>();
        dialogueText = dialogueTextObj.GetComponent<Text>();
    }

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void Ondialogue(string[] lines)
    {
        sentences.Clear();
        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }
        dialogueGroupe.alpha = 1;
        dialogueGroupe.blocksRaycasts = true;
        playerMove.enabled = false;
        isInitialized = true;
        isTyping = true;
        NextSentences();
    }

    public void NextSentences()
    {
        if (sentences.Count != 0)
        {
            currentSentence = sentences.Dequeue();
            isTyping = true;
            skipTyping = false; // 스페이스바로 타이핑을 건너뛰는 상태 초기화
            nextText.SetActive(false);
            StartCoroutine(Typing(currentSentence));
        }
        else
        {
            CloseDialogue();
        }
    }

    IEnumerator Typing(string line)
    {
        dialogueText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            // 스페이스바가 눌린 경우 전체 문장을 즉시 출력
            if (skipTyping)
            {
                dialogueText.text = line;
                break;
            }

            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    void Update()
    {
        if (!isInitialized)
        {
            return;
        }

        if (dialogueText.text.Equals(currentSentence))
        {
            nextText.SetActive(true);
            isTyping = false;
        }

        // 스페이스바 입력이 있을 때, 타이핑 중이라면 전체 문장을 즉시 출력
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                skipTyping = true; // 타이핑 중인 문장을 즉시 출력하도록 설정
            }
            else
            {
                NextSentences(); // 다음 문장을 출력
            }
        }
    }

    public void CloseDialogue()
    {
        dialogueGroupe.alpha = 0;
        dialogueGroupe.blocksRaycasts = false;
        dialogueText.text = "";
        nextText.SetActive(false);
        sentences.Clear();
        playerMove.enabled = true;
    }

    public bool IsDialogueFinished()
    {
        return sentences.Count == 0 && !isTyping;
    }
}