using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterPattern : MonoBehaviour
{
    private int nextPattern = 0;

    private static int NONE = 0;
    private static int RUSH = 1;
    private static int TAKEDOWN = 2;

    private Rigidbody2D rigid;
    private Vector2 rushDirection;

    [SerializeField] private float rushSpeed = 15.0f; // 돌진 속도
    [SerializeField] private float rushTime = 2.0f;   // 돌진 시간
    [SerializeField] private float rushCooldown = 5.0f; // 돌진 후 쿨타임

    [SerializeField] private float takedownHeight = 3.0f; // 내려찍기 높이
    [SerializeField] private float takedownRiseTime = 0.2f; // 떠오르는 시간
    [SerializeField] private float takedownHoldTime = 1.0f; // 공중에 머무는 시간
    [SerializeField] private float takedownCooldown = 5.0f; // 내려찍기 후 쿨타임

    private bool isRushing = false;  // 돌진 중인지 여부
    private bool playerInArea = false;  // 플레이어가 영역에 있는지 여부

    // 트리거 영역 (보스의 공격 범위)
    [SerializeField] private Collider2D rushArea;

    // 줄어드는 오브젝트의 트리거 영역 (특정 콜라이더)
    [SerializeField] private Collider2D shrinkCollider; // 특정 콜라이더 추가

    // 오브젝트가 사라지는데 걸리는 시간
    [SerializeField] private float shrinkDuration = 2.0f;  // 이 값을 Unity Inspector에서 설정 가능

    // Enemy 스크립트에 대한 참조
    private Enemy enemyScript;

    // 스킬 인디케이터 변수
    [SerializeField] private GameObject skillIndicatorPrefab; // 스킬 인디케이터 프리팹
    [SerializeField] private float preparationTime = 1.0f; // 돌진 시작 전 준비 시간

    // 스킬 인디케이터 인스턴스에 대한 참조
    private GameObject skillIndicatorInstance;

    // 보스의 스프라이트 렌더러와 원래 색상 저장을 위한 변수
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        // 이 게임 오브젝트에 첨부된 Enemy 스크립트 가져오기
        enemyScript = GetComponent<Enemy>();

        // 스프라이트 렌더러 가져오기 및 원래 색상 저장
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }

        StartCoroutine(PatternLoop());  // 보스 패턴 루프 시작
    }

    // 보스의 패턴 루프를 관리
    IEnumerator PatternLoop()
    {
        while (true)
        {
            // 플레이어가 영역에 있을 때만 패턴 실행
            if (playerInArea)
            {
                if (nextPattern == RUSH)
                {
                    yield return StartCoroutine(rush());  // 돌진 패턴 실행
                }
                else if (nextPattern == TAKEDOWN)
                {
                    yield return StartCoroutine(takedown());  // 내려찍기 패턴 실행
                }
            }

            yield return new WaitForSeconds(1.0f);  // 패턴 간 지연 시간
            nextPattern = Random.Range(NONE, TAKEDOWN + 1);  // NONE, RUSH 또는 TAKEDOWN 패턴 선택
        }
    }

    // 돌진 패턴 코루틴
    IEnumerator rush()
    {
        isRushing = true;

        // Enemy 스크립트 비활성화
        if (enemyScript != null)
        {
            enemyScript.enabled = false;
        }

        // 보스의 색상을 빨간색으로 변경
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
        }

        // 플레이어를 찾아 방향 설정
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector2 playerPosition = player.transform.position;
            Vector2 bossPosition = transform.position;
            rushDirection = (playerPosition - bossPosition).normalized;  // 플레이어를 향한 방향

            // 스킬 인디케이터 생성
            if (skillIndicatorPrefab != null)
            {
                skillIndicatorInstance = Instantiate(skillIndicatorPrefab, transform.position, Quaternion.identity);

                // 스킬 인디케이터를 플레이어 쪽으로 회전
                Vector3 direction = (playerPosition - bossPosition).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                skillIndicatorInstance.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }

        // 돌진 시작 전 준비 시간 대기
        yield return new WaitForSeconds(preparationTime);

        float timer = 0.0f;
        while (timer < rushTime)
        {
            // 돌진 방향으로 이동
            Vector2 nextPosition = rushDirection * rushSpeed * Time.deltaTime;
            rigid.MovePosition(rigid.position + nextPosition);

            // 타이머 증가
            timer += Time.deltaTime;
            yield return null;
        }

        // 스킬 인디케이터 삭제
        if (skillIndicatorInstance != null)
        {
            Destroy(skillIndicatorInstance);
        }

        isRushing = false;

        // 보스의 색상을 원래 색상으로 복원
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }

        // 돌진 후 Enemy 스크립트 다시 활성화
        if (enemyScript != null)
        {
            enemyScript.enabled = true;
        }

        yield return new WaitForSeconds(rushCooldown);  // 쿨타임 대기
    }

    // 내려찍기 패턴 코루틴
    IEnumerator takedown()
    {
        // Enemy 스크립트 비활성화
        if (enemyScript != null)
        {
            enemyScript.enabled = false;
        }

        // 보스의 색상을 파란색으로 변경
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.blue;
        }

        // 플레이어를 찾아 동일한 x축으로 이동
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector2 playerPosition = player.transform.position;
            Vector2 targetPosition = new Vector2(playerPosition.x, playerPosition.y + takedownHeight);

            // 떠오르는 동안 이동
            float elapsedTime = 0.0f;
            Vector2 startPosition = transform.position;
            while (elapsedTime < takedownRiseTime)
            {
                transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / takedownRiseTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 공중에 머무는 동안 리지드바디의 속도를 0으로 설정
            rigid.velocity = Vector2.zero; // 속도 0으로 설정하여 움직이지 않게 만듦
            rigid.isKinematic = true;      // 물리 효과를 받지 않도록 설정 (필요한 경우)

            // 잠시 공중에 머무름
            yield return new WaitForSeconds(takedownHoldTime);

            // 내려찍기 (y축으로 플레이어 위치로 이동)
            elapsedTime = 0.0f;
            Vector2 finalPosition = new Vector2(playerPosition.x, playerPosition.y);
            rigid.isKinematic = false;  // 다시 물리 효과를 받도록 설정
            while (elapsedTime < takedownRiseTime)
            {
                transform.position = Vector2.Lerp(targetPosition, finalPosition, elapsedTime / takedownRiseTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        // 내려찍기 후 색상을 원래대로 복원
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }

        // Enemy 스크립트 다시 활성화
        if (enemyScript != null)
        {
            enemyScript.enabled = true;
        }

        yield return new WaitForSeconds(takedownCooldown);  // 쿨타임 대기
    }

    // 플레이어가 영역에 들어오면 호출되는 메서드
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInArea = true;  // 플레이어가 영역에 들어옴
        }
        else if (collision.CompareTag("Enemy"))
        {
            // 특정 Collider인 shrinkCollider 안에서만 작아지도록 처리하고, 돌진 중일 때는 흡수하지 않음
            if (!isRushing && collision.IsTouching(shrinkCollider))
            {
                StartCoroutine(ShrinkAndDestroy(collision.gameObject));
            }
        }
    }

    // 플레이어가 영역에서 나가면 호출되는 메서드
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInArea = false;  // 플레이어가 영역을 벗어남
        }
    }

    // Enemy 태그가 붙은 오브젝트가 점점 작아지면서 사라지는 코루틴
    IEnumerator ShrinkAndDestroy(GameObject target)
    {
        Vector3 originalScale = target.transform.localScale;
        float elapsedTime = 0.0f;

        while (elapsedTime < shrinkDuration)
        {
            float t = elapsedTime / shrinkDuration;
            target.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);  // 오브젝트의 크기를 점점 줄임
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 오브젝트를 완전히 제거
        Destroy(target);
    }
}