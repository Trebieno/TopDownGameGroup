using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{   
    [SerializeField] private List<Weapon> _allWeapons = new List<Weapon>();
    public List<Weapon> AllWeapons => _allWeapons;
    public Weapon Weapon => _allWeapons[_index];
    
    [SerializeField] private int _index = 0;

    private bool _isActiveFire => Input.GetButton("Fire1");
    private bool _isDrop => Input.GetButtonDown("Drop");

    [SerializeField] private Bullet _prefubBullet;
    [SerializeField] private Loot _prefabLoot;

    private void Start()
    {
        // Weapon.gameObject.SetActive(true);
    }    

    private void Update()
    {
        if(_isActiveFire)
            Shoot();

        if(_isDrop)
            Drop();
    }

    private void Shoot()
    {
        if(Weapon.IsMeleeCombat)
            Weapon.MeleeCompat();
        else
            Weapon.Fire(_prefubBullet);
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

    public void TakeWeapon(string type, int count)
    {
        Drop();
        _index = _allWeapons.FindIndex(x => x.Type == type);
        _allWeapons[_index].IsHaveGun = true;
        List<Weapon> allActiveGun = _allWeapons.FindAll(x => x.IsHaveGun);

        for (int i = 0; i < allActiveGun.Count; i++)
        {
            if(_allWeapons[_index] != allActiveGun[i])
                allActiveGun[i].IsHaveGun = false;
        }
        Weapon.AddBullet(count);
        Weapon.gameObject.SetActive(true);
    }
}
