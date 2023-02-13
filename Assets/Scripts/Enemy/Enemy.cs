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
    [SerializeField] private LayerMask _playerMask;

    private NavMeshAgent _agent;
    private Transform _player;

    private void Start()
    {
        _index = Random.Range(0, _allWeapons.Count - 1);
        Weapon.IsHaveGun = true;
        Weapon.gameObject.SetActive(true);
        _agent = GetComponent<NavMeshAgent>();   
    }   

    private void FixedUpdate()
    {
        if(_player != null)
            _agent.SetDestination(_player.position);
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

        Loot loot = Instantiate(_prefabLoot, transform.position, new Quaternion(0, 0, transform.rotation.z, 0));
        loot.Initialization(Weapon.Bullets, Weapon.Type, Weapon.Sprite, transform.up);
        Weapon.IsHaveGun = false;
        Weapon.ResetBullet();
        Weapon.gameObject.SetActive(false);
    }
}
