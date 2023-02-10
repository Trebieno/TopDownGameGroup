using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour, IAttackable
{
    public float Health => _curHealth;
    
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _curHealth;
    [SerializeField] private float _radiusUse;

    [Space(10)]
    [SerializeField] private Transform _usePoint;
    [SerializeField] private LayerMask _useMask;
    [SerializeField] private LayerMask _lootingMask;

    private bool _isActiveUse => Input.GetButtonDown("Use");
    private bool _isActiveLooting => Input.GetButtonDown("Looting");

    private Movement _movement;
    public Movement Movement => _movement;

    private Shooting _shooting;
    public Shooting Shooting => _shooting;

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _shooting = GetComponent<Shooting>();
    }

    private void Update()
    {
        if(_isActiveUse)
        {
            Collider2D collider = Physics2D.OverlapCircle(_usePoint.position, _radiusUse, _useMask);
            if(collider != null)
                collider.GetComponent<IUsable>().Use(this);
        }

        if(_isActiveLooting)
        {
            Collider2D collider = Physics2D.OverlapCircle(_usePoint.position, _radiusUse, _lootingMask);
            if(collider != null)
                collider.GetComponent<ILootable>().TakeLoot(this);
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_usePoint.position, _radiusUse);
    }

    public void TakeDamage(float damage)
    {
        _curHealth -= damage;
        if(_curHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
