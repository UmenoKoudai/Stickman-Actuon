using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class EnemyController : MonoBehaviour
{
    [SerializeField]int _hp = 10;
    Animator _anim;
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(_hp <= 0)
        {
            _anim.Play("EnemyDestroy");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "DamageZone")
        {
            _hp--;
        }
    }

    public void EnemyDestoroy()
    {
        Destroy(gameObject);
    }
}
