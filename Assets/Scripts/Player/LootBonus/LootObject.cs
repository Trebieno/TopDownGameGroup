using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LootObject : MonoBehaviour
{
    public LootObjectDATA LootInfo;
    public static List<LootObject> Loots = new List<LootObject>();

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _radius;
    [SerializeField] private float _count;
    [SerializeField] private Rigidbody2D _rb2d;
    
    private Collider2D[] _colliders = new Collider2D[1];
    private void Start() 
    {
        _spriteRenderer.sprite = LootInfo.Sprite;
        _count = LootInfo.Count;
        Loots.Add(this);
    }


    private void FixedUpdate() 
    {
        Physics2D.OverlapCircleNonAlloc(transform.position, _radius, _colliders, _mask);

        if(_colliders[0] != null)
        {
            Vector2 direction = _colliders[0].transform.position - transform.position;
            
            _rb2d.velocity = direction;
        }
    }


    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
