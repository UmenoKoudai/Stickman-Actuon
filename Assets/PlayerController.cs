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
    [SerializeField] Vector2 _lineForWall = new Vector2(1f, 2f);
    [SerializeField] Vector2 _lineForRigth = Vector2.right;
    [SerializeField] Vector2 _lineForLeft = Vector2.left;
    [SerializeField] LayerMask _wallLayer = 0;
    [SerializeField] ParticleSystem _effect;
    [SerializeField] BoxCollider2D _boxCollider;
    SpriteRenderer _spriteRenderer;
    //Color _color;
    bool _wallJump = false;
    bool _isGround = true;
    Rigidbody2D _rb;
    float _timer;
    int _intarval = 3;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {
        _timer += Time.deltaTime;
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 start = this.transform.position;
        Debug.DrawLine(start, start + _lineForWall);
        RaycastHit2D hit = Physics2D.Linecast(start, start + _lineForWall, _wallLayer);
        //Debug.DrawLine(start, start + _lineForRigth);
        //Debug.DrawLine(start, start + _lineForLeft);
        //RaycastHit2D hitRigth = Physics2D.Linecast(start, start + _lineForRigth, _wallLayer);
        //RaycastHit2D hitLeft = Physics2D.Linecast(start, start + _lineForLeft, _wallLayer);
        _rb.drag = 0;
        FlipX(x);
        if( _isGround)
        {
            PlayerMove(x, y);
            if (_timer >= _intarval)
            {
                _lineForRigth = new Vector2(1f, 2f);
                _timer = 0;
            }
        }

        if (Input.GetButtonDown("Fire3"))
        {
            StartCoroutine(Dush());
        }
        //�ǂ̈ʒu(point)���擾���Ċp�x�����߂�A���̊p�x�̕����ɃW�����v����(������)
        //if (hitRigth.collider || hitLeft.collider)
        //{
        //    _rb.drag = 10;
        //    Debug.Log("�ǂɓ�������");
        //    var hitPosition = hitLeft.point;
        //    var playerUP = transform.up;
        //    if (Input.GetButtonDown("Jump"))
        //    {
        //        var jumpAngle = Mathf.Atan2(playerUP.y, hitPosition.x);
        //        Vector2 jumpVector = new Vector2(1 * Mathf.Cos(jumpAngle), 1 * Mathf.Sin(jumpAngle));
        //        _rb.velocity = jumpVector.normalized * _wallJumpPower;
        //    }
        //}
        if (hit.collider)
        {
            _rb.drag = 10;
            Debug.Log("�ǂɓ�������");
            if (Input.GetButtonDown("Jump"))
            {
                EffectPlay();
                if (_wallJump)
                {
                    _rb.velocity = new Vector2(1f, 2f).normalized * _wallJumpPower;
                    Debug.Log("�E�W�����v");
                    _lineForWall = new Vector2(1f, 2f);
                    FlipX(1f);
                    _wallJump = false;
                }
                else
                {
                    _rb.velocity = new Vector2(-1f, 2f).normalized * _wallJumpPower;
                    Debug.Log("���W�����v");
                    _lineForWall = new Vector2(-1f, 2f);
                    FlipX(-1f);
                    _wallJump = true;
                }
            }
        }

        //0.5�b�ԃ_�b�V������
        IEnumerator Dush()
        {
            Debug.Log("�_�b�V���J�n");
            _speed = _dushSpeed;
            EffectPlay();
            //_spriteRenderer.color.a = 0;
            yield return new WaitForSeconds(0.5f);
            Debug.Log("�_�b�V���I��");
            _speed = _defaltSpeed;
        }
    }

    //�v���C���[�̊�{����
    void PlayerMove(float X, float Y)
    {
        _rb.velocity = new Vector2(X, Y).normalized * _speed;
        //_rb.velocity = new Vector2(0f, Y).normalized * _speed;
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
        var Effect = _effect.GetComponent<ParticleSystem>();
        Effect.Play();
    }

    //�ڒn����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            _isGround = true;
            Debug.Log("�n�ʂɒ�����");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            _isGround = false;
            Debug.Log("�n�ʂ��痣�ꂽ");
        }
    }
}
