using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    [SerializeField] bool isGrounded = true;
    [SerializeField] bool canJump = true;

    float moveInput;
    [SerializeField] float jumpValue = 0f;
    [SerializeField] float walkSpeed = 6f;

    [SerializeField] PhysicsMaterial2D playerMat;
    [SerializeField] PhysicsMaterial2D playerBounce;

    BoxCollider2D boxCollider;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }


    private void Update()
    {
        isGrounded = IsGrounded();
        Jump();
    }

    private bool IsGrounded()
    {
        if(boxCollider.IsTouchingLayers(LayerMask.GetMask("Platform")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Jump()
    {
        moveInput = Input.GetAxis("Horizontal");

        if(jumpValue == 0f && isGrounded)
        {
            rigidbody.velocity = new Vector2(moveInput * walkSpeed, rigidbody.velocity.y);
        }

        // jumpValue�� 20�� �Ѿ����� �����̽��� �� ��� 
        if (jumpValue == 0.0f && !isGrounded)
        {
            canJump = true;
        }

        if (jumpValue> 0)
        {
            rigidbody.sharedMaterial = playerBounce;
        }
        else
        {
            rigidbody.sharedMaterial = playerMat;
        }

        

        if (Input.GetKey("space") && isGrounded && canJump)
        {
            jumpValue += 0.3f;
        }

        if (Input.GetKeyDown("space") && isGrounded && canJump)
        {
            // ���� ������ ���� �̵��� ���� ������ ���� 
            rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);
        }

        // 20 �Ѿ �ڵ����� �����Ǹ� canJump�� false�� �Ǿ��ٰ� �ٽ� true�� ���ƿ�
        if (jumpValue > 20 && isGrounded)
        {
            float tempx = moveInput * walkSpeed;
            float tempy = jumpValue;
            rigidbody.velocity = new Vector2(tempx, tempy);

            // TODO : invoke ������ 20�� �Ѿ �������������� space�ٸ� �ȶ��� �ֵ���
            // ������ canJump�� true�� �ٲ���µ�, 
            // invoke �� �Ǿ canJump�� false�� �Ǵ� ���� O 
            Invoke("ResetJump", 0.2f);
        }

        

        if (Input.GetKeyUp("space"))
        {
            if(isGrounded)
            {
                float tempx = moveInput * walkSpeed;
                float tempy = jumpValue;
                rigidbody.velocity = new Vector2(tempx, tempy);

                // ���� �� jumpValue �ʱ�ȭ
                jumpValue = 0f;
            }
            canJump = true; 
        }
    }

    private void ResetJump()
    {
        canJump = false;
        jumpValue = 0f;
    }
}
