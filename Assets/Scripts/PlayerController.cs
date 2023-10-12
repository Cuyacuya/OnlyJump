using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

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

    CircleCollider2D circleCollider;

    SpriteRenderer spriteRenderer;

    Animator animator;
    public bool IsRun;

    private PlayableDirector pd;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        pd = GetComponent<PlayableDirector>();
    }


    private void Update()
    {
        isGrounded = IsGrounded();
        Jump();

        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
        if (rigidbody.velocity.y == 0)
        {
            animator.SetBool("IsJump", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Ending")
        {
            other.gameObject.SetActive(false);
            SceneManager.LoadScene(2);
            

        }
    }


    private bool IsGrounded()
    {
        if(circleCollider.IsTouchingLayers(LayerMask.GetMask("Platform")))
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
        if (rigidbody.velocity.x == 0)
        {
            animator.SetBool("IsRun", false);
        }
        else
            animator.SetBool("IsRun", true);
         
        
        if (jumpValue == 0f && isGrounded)
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
            jumpValue += 0.2f;
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
            Invoke("ResetJump", 0.5f);
        }

        

        if (Input.GetKeyUp("space"))
        {
            if(isGrounded)
            {
                float tempx = moveInput * walkSpeed;
                float tempy = jumpValue;
                rigidbody.velocity = new Vector2(tempx, tempy);
                animator.SetBool("IsJump", true);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
           OnDamaged(collision.transform.position);
        }
    }
    void OnDamaged(Vector2 targetpos)
    {
        gameObject.layer = 7;


        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        int dirc = transform.position.x - targetpos.x > 0 ? 10 : -10;
        rigidbody.AddForce(new Vector2(dirc, 10)*5, ForceMode2D.Impulse);

        Invoke("OffDamaged", 3);
    }
    void OffDamaged()
    {
        gameObject.layer = 6;

        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
