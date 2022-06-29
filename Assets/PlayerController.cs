using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _playerMove;
    Vector2 _playerPower;
    public float _moveSpeed = 5f;
    float _horizontal;
    // Start is called before the first frame update
    void Start()
    {
        _playerMove = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _playerMove.velocity = new Vector2(-_moveSpeed, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _playerMove.velocity = new Vector2(_moveSpeed,0);
        }
        if (_horizontal < 0)
        {
            _playerMove.velocity = Vector2.zero;
        }
    }
}
