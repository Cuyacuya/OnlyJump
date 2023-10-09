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

    void FixedUpdate()
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
            // 점프 직전에 가로 이동에 의한 영향을 없앰 
            rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);
        }

        // 20 넘어서 자동으로 점프되면 canJump는 false가 되었다가 다시 true로 돌아옴
        if (jumpValue > 20 && isGrounded)
        {
            float tempx = moveInput * walkSpeed;
            float tempy = jumpValue;
            rigidbody.velocity = new Vector2(tempx, tempy);
            Invoke("ResetJump", 0.2f);
        }

        if (Input.GetKeyUp("space"))
        {
            if(isGrounded)
            {
                float tempx = moveInput * walkSpeed;
                float tempy = jumpValue;
                rigidbody.velocity = new Vector2(tempx, tempy);

                

                // 점프 후 jumpValue 초기화
                jumpValue = 0f;
            }
            canJump = true; 
        }
    }

    private void ResetJump()
    {
        jumpValue = 0f;
        canJump = false;
    }
}
