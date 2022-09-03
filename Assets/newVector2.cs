using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class newVector2 : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] float _stop = 0.5f;
    Vector2 _playerPosition;
    Vector3 _enemyPosition;
    Rigidbody2D _rb;
    float _speed = 0.01f;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _enemyPosition = transform.position;
    }

    void Update()
    {
        _playerPosition = _player.transform.position;
        float distance = Vector2.Distance(transform.position, _playerPosition);
        if(distance <= _stop)
        {
            Vector3 dir = (_player.transform.position - this.transform.position).normalized * _speed;
            transform.Translate(dir);
        }
        else
        {
            Vector3 set = (_enemyPosition - this.transform.position).normalized * _speed;
            transform.Translate(set);
        }
    }
}
