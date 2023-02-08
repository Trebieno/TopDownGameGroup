using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        _index = Random.Range(0, _allWeapons.Count - 1);
        Weapon.IsHaveGun = true;
        Weapon.gameObject.SetActive(true);
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
        loot.Initialization(Weapon.Bullets, Weapon.ReserveBullets, Weapon.Type, Weapon.Sprite, transform.up);
        Weapon.IsHaveGun = false;
        Weapon.ResetBullet();
        Weapon.gameObject.SetActive(false);
    }
}
