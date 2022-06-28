using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _playerMove;
    Vector2 _playerPower;
    int _moveSpeed = 1;
    float _horizontal;
    // Start is called before the first frame update
    void Start()
    {
        _playerMove = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _playerMove.AddForce(_playerPower * _moveSpeed, ForceMode2D.Force);
        if (Input.GetKeyDown(KeyCode.A))
        {
            _playerPower = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _playerPower = Vector2.right;
        }
        if (_horizontal < 0)
        {
            _playerMove.velocity = Vector2.zero;
        }
    }
}
