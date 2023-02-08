using UnityEngine;

public class Loot : MonoBehaviour, ILootable
{
    [SerializeField] private int _bullet;
    public int Bullet => _bullet;

    [SerializeField] private int _reserveBullet;
    public int ReserveBullet => _reserveBullet;

    [SerializeField] private string _type;
    public string Type => _type;

    [SerializeField] private float _speed;
    public float Speed => _speed;

    [SerializeField] private SpriteRenderer _skin;
    [SerializeField] private Rigidbody2D _rb2D;

    public void Initialization(int bullet, int reserveBullet, string type, Sprite sprite, Vector2 direction)
    {
        _bullet = bullet;
        _reserveBullet = reserveBullet;
        _type = type;
        _skin.sprite = sprite;

        if(_rb2D == null)
            _rb2D = GetComponent<Rigidbody2D>();

        _rb2D.AddForce(direction * _speed, ForceMode2D.Impulse);
    }

    public void TakeLoot(Player player)
    {
        var weapon = player.Shooting.AllWeapons.Find(x => x.Type == _type);
        player.Shooting.TakeWeapon(weapon.Type, _bullet);
        Destroy(gameObject);
    }
}
