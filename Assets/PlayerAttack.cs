using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] BoxCollider2D _boxCollider;

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Attack());
        }
    }
    //ƒNƒŠƒbƒN‚µ‚ÄUŒ‚
    IEnumerator Attack()
    {
        Debug.Log("UŒ‚");
        _boxCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        Debug.Log("UŒ‚I—¹");
        _boxCollider.enabled = false;
    }
}
