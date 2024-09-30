using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public int maxBounce; // 최대 바운스 횟수
    public int goldAmount; // 돈 양

    public float xForce; // X축으로 가해질 힘 (멀리 날아가도록)
    public float yForce; // Y축으로 가해질 힘 (높이 날아가도록)
    public float gravity; // 중력 값

    private Vector2 direction; // 오브젝트가 날아가는 방향
    private int currentBounce = 0; // 현재 바운스 횟수
    private bool isGrounded = true; // 오브젝트가 땅에 닿았는지 여부

    private float maxHeight; // 오브젝트가 도달할 수 있는 최대 높이
    private float currentHeight; // 현재 오브젝트의 높이

    public Transform sprite; // 오브젝트의 스프라이트(이미지)를 참조
    public Transform shadow; // 그림자(지면 위치)를 참조

    void Start()
    {
        // Y축 힘 범위에서 랜덤한 높이로 현재 높이를 설정하고, 최대 높이로 초기화
        currentHeight = Random.Range(yForce - 1, yForce);
        maxHeight = currentHeight;

        // 랜덤한 방향으로 초기화
        Initialize(new Vector2(Random.Range(-xForce, xForce), Random.Range(-xForce, xForce)));
    }

    void Update()
    {
        if (!isGrounded) // 오브젝트가 땅에 닿지 않았을 때만 실행
        {
            // 중력에 의해 높이를 감소시킴
            currentHeight += -gravity * Time.deltaTime;

            // 스프라이트의 위치를 업데이트 (Y축으로 이동)
            sprite.position += new Vector3(0, currentHeight, 0) * Time.deltaTime;

            // 오브젝트 전체의 위치를 방향에 따라 업데이트
            transform.position += (Vector3)direction * Time.deltaTime;

            // 총 속도 계산 (높이 변화에 따라)
            float totalVelocity = Mathf.Abs(currentHeight) + Mathf.Abs(maxHeight);

            // 스프라이트의 그림자 크기를 속도 비율에 따라 조정
            float scaleXY = Mathf.Abs(currentHeight) / totalVelocity;
            shadow.localScale = Vector2.one * Mathf.Clamp(scaleXY, 0.5f, 1.0f);

            // 땅과의 충돌을 체크
            CheckGroundHit();
        }
    }

    // 오브젝트를 초기화하는 메서드
    void Initialize(Vector2 _direction)
    {
        isGrounded = false; // 오브젝트가 공중에 있다고 설정
        maxHeight /= 1.5f; // 최대 높이를 감소시켜 다음 바운스의 높이를 줄임
        direction = _direction; // 이동 방향 설정
        currentHeight = maxHeight; // 현재 높이를 최대 높이로 설정
        currentBounce++; // 바운스 횟수 증가
    }

    // 오브젝트가 땅에 닿았는지 확인하는 메서드
    void CheckGroundHit()
    {
        // 스프라이트가 그림자보다 낮으면 땅에 닿았다고 판단
        if (sprite.position.y < shadow.position.y)
        {
            // 스프라이트의 위치를 그림자의 위치로 설정하여 땅에 붙도록 함
            sprite.position = shadow.position;

            // 그림자의 크기를 원래대로 설정
            shadow.localScale = Vector2.one;

            if (currentBounce < maxBounce) // 아직 바운스 횟수가 남아있다면
            {
                Initialize(direction / 1.5f); // 방향을 줄여서 다시 바운스
            }
            else // 최대 바운스 횟수에 도달했다면
            {
                isGrounded = true; // 오브젝트를 땅에 고정
            }
        }
    }

    // 플레이어와 충돌했을 때 처리하는 메서드
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // 충돌한 객체가 "Player" 태그를 가지고 있다면
        {
            Destroy(gameObject); // 오브젝트(경험치)를 제거
        }
    }

    // BossExp 오브젝트를 특정 위치에서 생성하는 메서드
    public static void CreateBossItem(GameObject bossItemPrefab, Transform chestTransform)
    {
        Instantiate(bossItemPrefab, chestTransform.position, Quaternion.identity);
    }
}