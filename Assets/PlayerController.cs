using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>通常の移動スピード</summary>
    [SerializeField] float _defaltSpeed;
    /// <summary>ダッシュ時の移動スピード</summary>
    [SerializeField] float _dushSpeed;
    /// <summary>通常ジャンプの力</summary>
    [SerializeField] float _jumpPower;
    /// <summary>壁ジャンプの力</summary>
    [SerializeField] float _wallJumpPower;
    /// <summary>移動スピード</summary>
    [SerializeField] float _speed;
    [SerializeField] Vector2 _lineForWall = new Vector2(1f, 1f);
    [SerializeField] LayerMask _wallLayer = 0;
    [SerializeField] GameObject _effect;
    bool _wallJump = false;
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
        Vector2 start = this.transform.position;
        Debug.DrawLine(start, start + _lineForWall);
        RaycastHit2D hit = Physics2D.Linecast(start, start + _lineForWall, _wallLayer);

        FlipX(_x);
        if (Input.GetButtonDown("Fire3"))
        {
            StartCoroutine(Dush());
        }
        if (Input.GetButtonDown("Jump") && _isGround)
        {
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            EffectPlay();
            //_rb.velocity = new Vector2(0, _jumpPower);
        }
        if(hit.collider && Input.GetButtonDown("Jump"))
        {
            Debug.Log("壁に当たった");
            _lineForWall = Vector2.zero;
            if (_wallJump)
            {
                Debug.Log("右ジャンプ");
                _lineForWall = new Vector2(1f, 1f);
                _rb.AddForce(_lineForWall * _wallJumpPower, ForceMode2D.Impulse);
                FlipX(1f);
                EffectPlay();
                _wallJump = false;
            }
            else
            {
                Debug.Log("左ジャンプ");
                _lineForWall = new Vector2(-1f, 1f);
                _rb.AddForce(_lineForWall * _wallJumpPower, ForceMode2D.Impulse);
                FlipX(-1f);
                EffectPlay();
                _wallJump = true;
            }
        }
        
        //0.5秒間ダッシュする
        IEnumerator Dush()
        {
            Debug.Log("ダッシュ開始");
            _speed = _dushSpeed;
            EffectPlay();
            yield return new WaitForSeconds(0.5f);
            Debug.Log("ダッシュ終了");
            _speed = _defaltSpeed;
        }
    }

    //プレイヤーの向きを変える
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

    void EffectPlay()
    {
        Instantiate(_effect, transform.position, transform.rotation);
    }

    //接地判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
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
