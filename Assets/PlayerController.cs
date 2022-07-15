using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>�ʏ�̈ړ��X�s�[�h</summary>
    [SerializeField] float _defaltSpeed;
    /// <summary>�_�b�V�����̈ړ��X�s�[�h</summary>
    [SerializeField] float _dushSpeed;
    /// <summary>�ʏ�W�����v�̗�</summary>
    [SerializeField] float _jumpPower;
    /// <summary>�ǃW�����v�̗�</summary>
    [SerializeField] float _wallJumpPower;
    /// <summary>�ړ��X�s�[�h</summary>
    [SerializeField] float _speed;
    [SerializeField] Vector2 _lineForWall = Vector2.right;
    [SerializeField] LayerMask _wallLayer = 0;
    [SerializeField] GameObject _effect;
    bool _wallJump = false;
    bool _isGround = true;
    Rigidbody2D _rb;
    float _x;
    float _timer;
    int _intarval = 3;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _timer += Time.deltaTime;
        _x = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(_x * _speed, 0);
        Vector2 start = this.transform.position;
        Debug.DrawLine(start, start + _lineForWall);
        RaycastHit2D hit = Physics2D.Linecast(start, start + _lineForWall, _wallLayer);
        FlipX(_x);
        if(_timer >= _intarval)
        {
            _lineForWall = Vector2.right;
            //_rb.isKinematic = false;
            _timer = 0f;
        }
        if (Input.GetButtonDown("Fire3"))
        {
            StartCoroutine(Dush());
        }
        if (Input.GetButtonDown("Jump") && _isGround && !hit.collider)
        {
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            //_rb.velocity = new Vector2(0, _jumpPower);
        }

        if(hit.collider && Input.GetButtonDown("Jump"))
        {
            Debug.Log("�ǂɓ�������");
            //_lineForWall = Vector2.zero;
            if (_wallJump)
            {
                //_rb.isKinematic = true;
                Debug.Log("�E�W�����v");
                _lineForWall = Vector2.left;
                _rb.AddForce(new Vector2(-1f, 2f).normalized * _wallJumpPower, ForceMode2D.Impulse);
                FlipX(1f);
                _wallJump = false;
            }
            else
            {
                //_rb.isKinematic = true;
                Debug.Log("���W�����v");
                _lineForWall = Vector2.right;
                _rb.AddForce(new Vector2(1f, 2f).normalized * _wallJumpPower, ForceMode2D.Impulse);
                FlipX(-1f);
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

    void EffectPlay()
    {
        Instantiate(_effect, transform.position, transform.rotation);
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
