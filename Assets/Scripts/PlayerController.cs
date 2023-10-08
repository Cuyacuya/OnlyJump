using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    bool isGrounded = true;
    
    float duration = 0f;
    float horizontalInput, verticalInput;

    [SerializeField] float jumpForce = 10f;
    [SerializeField] float moveSpeed = 10f;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if(Input.GetKey("space") && isGrounded)
        {
            duration += Time.deltaTime;

            // ���� ������ �ӵ��� ���������� ����(0,0)�� ����
            rigidbody.velocity = Vector2.zero;

        }
        if(Input.GetKeyUp("space") && isGrounded)
        {
            rigidbody.velocity = Vector3.right * horizontalInput + Vector3.up * verticalInput;
            rigidbody.AddForce(new Vector2(0, jumpForce * duration));
            duration = 0f;
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
