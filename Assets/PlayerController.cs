using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>通常の移動スピード</summary>
    [SerializeField] float _defaltSpeed;
    /// <summary>ダッシュ時の移動スピード</summary>
    [SerializeField] float _dushSpeed;
    [SerializeField] float _jumpPower;
    [SerializeField] float _speed;
    bool _isGround = true;
    Rigidbody2D _rb;
    float _x;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _x = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(_x * _speed, 0);
        //プレイヤーの向きを変える
        FlipX(_x);
        if (Input.GetButtonDown("Fire3"))
        {
            StartCoroutine(Dush());
        }
        if (Input.GetButtonDown("Jump") && _isGround)
        {
            _rb.velocity = new Vector2(0, _jumpPower);
        }
        void FlipX(float horizontal)
        {
            if (horizontal > 0)
            {
                transform.localScale = new Vector2(1, transform.localScale.y);
            }
            else if (horizontal < 0)
            {
                transform.localScale = new Vector2(-1, transform.localScale.y);
            }
        }
        //0.5秒間ダッシュする
        IEnumerator Dush()
        {
            Debug.Log("ダッシュ開始");
            _speed = _dushSpeed;
            yield return new WaitForSeconds(0.5f);
            Debug.Log("ダッシュ終了");
            _speed = _defaltSpeed;
        }
    }

    //接地判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isGround = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _isGround = false;
    }
}
