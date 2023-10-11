using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    Rigidbody2D rigidbody;

    [SerializeField] float jumpPower = 20f;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    }

    private void Jump()
    {
        rigidbody.AddForce(new Vector2(0.3f, 0.6f) * jumpPower, ForceMode2D.Impulse);
    }
}
