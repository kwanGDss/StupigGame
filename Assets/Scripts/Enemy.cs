using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float hp;

    private void Awake()
    {
        hp = 100.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Dead();
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void Dead()
    {
        if (hp < 0.0f)
        {
            Debug.Log("Dead");
        }
    }
    
    public void TakeDamage(float damage)
    {
        hp -= damage;
    }
}
