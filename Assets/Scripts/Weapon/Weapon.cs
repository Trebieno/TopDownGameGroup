using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Bullets")]
    [SerializeField] private int _maxBullet;
    [SerializeField] private int _curBullet;
    [SerializeField] private int _reserveBullets;

    [Header("Damage")]
    [SerializeField] private float _damage;

    [Header("Cooldown reload")]
    [SerializeField] private float _maxTimaReload;
    [SerializeField] private float _curTimaReload;

    [Header("Cooldown shoot")]
    [SerializeField] private float _maxDelayShoot = 0.1f;
    [SerializeField] private float _curDelayShoot = 0.1f;

    [Header("Melee")]
    [SerializeField] private bool _isMeleeCombat;
    [SerializeField] private float _radiusMeleeCombat;
    [SerializeField] private LayerMask _meleeMask;

    [Header("Other")]
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _speedBullet;
    [SerializeField] private string _type;
    [SerializeField] private SpriteRenderer _skin;

    public bool IsHaveGun;

    public int Bullets => _curBullet;
    public int ReserveBullets => _reserveBullets;
    public string Type => _type;
    public Sprite Sprite => _skin.sprite;
    public bool IsMeleeCombat => _isMeleeCombat;


    public void Fire (Bullet bullet)
    {
        if(_curBullet <= 0 || _curDelayShoot > 0 || _isMeleeCombat)
            return;
        
        bullet = Instantiate(bullet, _firePoint.position, _firePoint.rotation);
        bullet.AddForce(_firePoint.transform.up * _speedBullet);
        bullet.Damage = _damage;

        _curBullet--;
        
        _curDelayShoot = _maxDelayShoot;

        if(_curBullet - 1 <= 0 && _reserveBullets > 0)
            _curTimaReload = _maxTimaReload;
    }

    public void MeleeCompat()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(_firePoint.position, _radiusMeleeCombat, _meleeMask);
        if(_curDelayShoot <= 0)
        {
            if(collider != null)
                for (int i = 0; i < collider.Length; i++)
                    collider[i].GetComponent<IAttackable>().TakeDamage(_damage);

            _curDelayShoot = _maxDelayShoot;
        }
                
    }

    public void AddBullet(int count)
    {
        _curBullet += count;
        if(_curBullet > _maxBullet)
            _curBullet = _maxBullet;
    }

    public void ResetBullet()
    {
        _curBullet = 0;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_firePoint.position, _radiusMeleeCombat);
    }

    private void Update()
    {
        if (_curDelayShoot > 0)
            _curDelayShoot -= Time.deltaTime;

        if(_curTimaReload > 0)
        {
            _curTimaReload -= Time.deltaTime;
            if(_curTimaReload <= 0)
                _curBullet = _maxBullet;
        }
    }
}
