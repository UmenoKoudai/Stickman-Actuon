using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody2D _rb = default;
    [SerializeField]int _moveSpeed;
    [SerializeField]int _jumpSpeed;
    bool _isGround = true;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float X = Input.GetAxisRaw("Horizontal");
        float Y = Input.GetAxisRaw("Vertical");
        if(Mathf.Abs(X) >= 1)
        {
            _rb.velocity = new Vector2(X, 0f).normalized * _moveSpeed;
        }
        if(_isGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _rb.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            _isGround = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            _isGround = false;
        }
    }
}
