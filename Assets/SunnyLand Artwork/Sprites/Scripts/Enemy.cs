using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    Animator animator;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("Think", 4);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove,rigid.velocity.y);
        Vector2 frontVec = new Vector2 (rigid.position.x +nextMove*0.3f,rigid.position.y);
        Debug.DrawRay(frontVec,Vector3.down,new Color(0,1,0));
        RaycastHit2D rayhit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if(rayhit.collider == null)
        {
            nextMove *= -1;
            spriteRenderer.flipX = nextMove == 1;
            CancelInvoke();
            Invoke("Think", 4);
        }
    }
    void Think()
    {
        nextMove = Random.Range(-1,2);

        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
        animator.SetInteger("WalkSpeed", nextMove);
        if(nextMove!=0)
        {
            spriteRenderer.flipX = nextMove == 1;
        }
    }
}
