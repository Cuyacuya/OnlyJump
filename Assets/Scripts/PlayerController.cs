using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    [SerializeField] bool isGrounded = true;

    [SerializeField] float jumpValue = 0f;

    float moveInput;
    [SerializeField] float walkSpeed = 6f;

    float xScreenHalfSize;
    float yScreenHalfSize;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        yScreenHalfSize = Camera.main.orthographicSize;
        xScreenHalfSize = yScreenHalfSize * Camera.main.aspect;
    }

    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        moveInput = Input.GetAxis("Horizontal");
        transform.localPosition = ClampPosition(new Vector2(transform.localPosition.x + moveInput * walkSpeed * Time.deltaTime, transform.localPosition.y));
    }

    // �÷��̾ ī�޶� ȭ�� ������ ������ �ʰ� �� 
    private Vector3 ClampPosition(Vector3 position)
    {
        return new Vector3(Mathf.Clamp(position.x, -xScreenHalfSize, xScreenHalfSize), position.y, position.z);
    }

    private void Jump()
    {
        if(Input.GetKeyDown("space") && isGrounded)
        {
            // ���� ������ ���� �̵��� ���� ������ ���� 
            rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);
        }
        if(Input.GetKey("space") && isGrounded)
        {
            jumpValue += 0.1f;
        }
        if(Input.GetKeyUp("space") && isGrounded)
        {
            if(jumpValue > 20f)
            {
                jumpValue = 20f;
            }
            float tempx = moveInput * walkSpeed;
            float tempy = jumpValue;
            rigidbody.velocity = new Vector2(tempx, tempy);

            jumpValue = 0f;
        }
    }

    // �ٴڿ� ������� �����ϴ� ó��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ٴڰ� ��Ұ�, �浹 ǥ���� ������ ���� ������
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
        }
    }

    // �ٴڿ��� ������� �����ϴ� ó��
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
