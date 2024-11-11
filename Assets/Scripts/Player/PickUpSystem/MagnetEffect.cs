using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MagnetEffect : MonoBehaviour
{   //Player�� component ���
    public float moveSpeed = 15f;
    public float magnetRange = 3f; // ���� �������� ����
    private CircleCollider2D circleCollider;
    private PickUpSystem pickUpSystem;

    private void Start()
    {
        circleCollider = gameObject.AddComponent<CircleCollider2D>();
        circleCollider.radius = magnetRange;
        circleCollider.isTrigger = true;
        gameObject.layer = LayerMask.NameToLayer("Magnet");
        pickUpSystem = FindObjectOfType<PickUpSystem>();
    }
    public void SetMagnetRange(float newRange)
    {
        magnetRange = newRange;
        circleCollider.radius = magnetRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin") && collision.gameObject.layer == LayerMask.NameToLayer("Magnet"))
        {
            Item item = collision.GetComponent<Item>();
            StartCoroutine(AttractCoin(collision.gameObject)); 
            pickUpSystem.PickUpItem(item);
        }
    }
    private IEnumerator AttractCoin(GameObject coin)
    {
        while (coin != null && Vector3.Distance(coin.transform.position, transform.position) > 0.1f)
        {
            // �÷��̾� ������ ���� �̵�
            coin.transform.position = Vector3.MoveTowards(coin.transform.position, transform.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

    }
}
