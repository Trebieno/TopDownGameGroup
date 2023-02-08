using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    [SerializeField] private Rigidbody2D _rb2D;
    [SerializeField] private float _timeDestroy;
    public Rigidbody2D Rb2D => _rb2D;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
            other.GetComponent<IAttackable>().TakeDamage(Damage);


        if(!other.CompareTag("Player") )
            Destroy(gameObject);

    }

    public void AddForce(Vector2 force)
    {
        _rb2D.AddForce(force, ForceMode2D.Force);
        Destroy(gameObject, _timeDestroy);
    }

}
