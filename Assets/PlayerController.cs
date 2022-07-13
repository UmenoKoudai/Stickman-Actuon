using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>�ʏ�̈ړ��X�s�[�h</summary>
    [SerializeField] float _defaltSpeed;
    /// <summary>�_�b�V�����̈ړ��X�s�[�h</summary>
    [SerializeField] float _dushSpeed;
    [SerializeField] float _jumpPower;
    [SerializeField] float _speed;
    [SerializeField] Vector2 _lineForWall = new Vector2(1f, 1f);
    [SerializeField] LayerMask _wallLayer = 0;
    bool _wallJump = true;
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
            //_rb.velocity = new Vector2(0, _jumpPower);
        }
        if(hit.collider && Input.GetButtonDown("Jump"))
        {
            _rb.velocity = _lineForWall;
            if(_wallJump)
            {
                _lineForWall = new Vector2(1f, 1f);
                _wallJump = false;
            }
            else
            {
                _lineForWall = new Vector2(-1f, 1f);
                _wallJump = true;
            }
        }
        
        //0.5�b�ԃ_�b�V������
        IEnumerator Dush()
        {
            Debug.Log("�_�b�V���J�n");
            _speed = _dushSpeed;
            yield return new WaitForSeconds(0.5f);
            Debug.Log("�_�b�V���I��");
            _speed = _defaltSpeed;
        }
    }

    //�v���C���[�̌�����ς���
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

    //�ڒn����
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
