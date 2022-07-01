using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _playerMove;
    Vector2 _playerPower;
    public float _moveSpeed = 5f;
    float _horizontal;
    float _time;
    // Start is called before the first frame update
    void Start()
    {
        _playerMove = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        //float y = Input.GetAxis("Vertical");

        //_playerMove.velocity = new Vector2(x + _moveSpeed, y + _moveSpeed);
        FlipX(x);
        if (Input.GetKey(KeyCode.A))
        {
            _playerMove.velocity = new Vector2(-_moveSpeed, 0);
            //StartCoroutine(Click());
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _playerMove.velocity = new Vector2(_moveSpeed, 0);
            //StartCoroutine(Click());
        }
        if (_horizontal < 0)
        {
            _playerMove.velocity = Vector2.zero;
        }
        void FlipX(float horizontal)
        {
            if(horizontal > 0)
            {
                transform.localScale = new Vector2(1, transform.localScale.y);
            }
            else if(horizontal < 0)
            {
                transform.localScale = new Vector2(-1, transform.localScale.y);
            }
        }
    }

    //IEnumerator Click()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        _playerMove.velocity = new Vector2(-_moveSpeed * 10, 0);
    //    }
    //    else if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        _playerMove.velocity = new Vector2(_moveSpeed * 10, 0);
    //    }
    //    yield return new WaitForSecondsRealtime(0.5f);
    }
