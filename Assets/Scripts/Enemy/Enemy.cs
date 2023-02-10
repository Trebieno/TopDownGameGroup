using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IAttackable
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _curHealth;

    [SerializeField] private List<Weapon> _allWeapons = new List<Weapon>();
    public List<Weapon> AllWeapons => _allWeapons;
    public Weapon Weapon => _allWeapons[_index];
    
    [SerializeField] private int _index = 0;
    [SerializeField] private Bullet _prefubBullet;
    [SerializeField] private Loot _prefabLoot;

    [Range(0, 360)] public float ViewAngle = 90f;
    public float ViewDistance = 15f;
    public float DetectionDistance = 3f;
    public Transform EnemyEye;
    public Transform Target;

    private NavMeshAgent _agent;
    [SerializeField] private float _rotationSpeed;
    private Transform _agentTransform;

    [SerializeField] private LayerMask _playerMask;

    private void Start()
    {
        _index = Random.Range(0, _allWeapons.Count - 1);
        Weapon.IsHaveGun = true;
        Weapon.gameObject.SetActive(true);
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agentTransform = _agent.transform;
    }

    private void Update()
    {       
        float distanceToPlayer = Vector3.Distance(Target.transform.position, _agent.transform.position);
        if (distanceToPlayer <= DetectionDistance || IsInView())
        {
            RotateToTarget();
            MoveToTarget();
        }
        DrawViewState();
    }

    private bool IsInView() // true если цель видна
    {
        float realAngle = Vector3.Angle(EnemyEye.forward, Target.position - EnemyEye.position);
        RaycastHit2D hit = Physics2D.Raycast(EnemyEye.transform.position, Target.position - EnemyEye.position, ViewDistance, _playerMask);

        if (hit)
        {
            if ( realAngle < ViewAngle / 2f && Vector3.Distance(EnemyEye.position, Target.position) <= ViewDistance && hit.transform == Target.transform)
            {
                return true;
            }
        }
        return false;
    }

    private void RotateToTarget() // поворачивает в стороно цели со скоростью rotationSpeed
    {
        Vector3 lookVector = Target.position - _agentTransform.position;
        lookVector.z = 0;
        if (lookVector == Vector3.zero) return;
        _agentTransform.rotation = Quaternion.RotateTowards
            (
                _agentTransform.rotation,
                Quaternion.LookRotation(lookVector , Vector3.up),
                _rotationSpeed * Time.deltaTime
            );
    }

    private void MoveToTarget() // устанвливает точку движения к цели
    {
        _agent.SetDestination(Target.position);
    }
    
    
    private void DrawViewState() 
    {       
        Vector3 left = EnemyEye.position + Quaternion.Euler(new Vector3(0, 0, ViewAngle / 2f)) * (EnemyEye.forward * ViewDistance);
        Vector3 right = EnemyEye.position + Quaternion.Euler(-new Vector3(0, 0,ViewAngle / 2f)) * (EnemyEye.forward * ViewDistance);     
        Debug.DrawLine(EnemyEye.position, left, Color.yellow);
        Debug.DrawLine(EnemyEye.position, right, Color.yellow);       
    }

    public void TakeDamage(float damage)
    {
        _curHealth -= damage;
        Debug.Log("Ударен");
        if(_curHealth <= 0)
        {
            Drop();
            _curHealth = _maxHealth;
        }
    }

    private void Drop()
    {
        if(!Weapon.IsHaveGun)
            return;

        Loot loot = Instantiate(_prefabLoot, transform.position, transform.rotation);
        loot.Initialization(Weapon.Bullets, Weapon.Type, Weapon.Sprite, transform.up);
        Weapon.IsHaveGun = false;
        Weapon.ResetBullet();
        Weapon.gameObject.SetActive(false);
    }
}
