using UnityEngine;

public class Loot : MonoBehaviour, ILootable
{
    [SerializeField] private int _bullet;
    public int Count => _bullet;

    [SerializeField] private string _type;
    public string Type => _type;

    [SerializeField] private float _speedForceLoot;
    public float Speed => _speedForceLoot;

    [SerializeField] private SpriteRenderer _skin;
    [SerializeField] private Rigidbody2D _rb2D;

    public void Initialization(int count, string type, Sprite sprite, Vector2 direction)
    {
        _bullet = count;
        _type = type;
        _skin.sprite = sprite;

        if(_rb2D == null)
            _rb2D = GetComponent<Rigidbody2D>();

        _rb2D.AddForce(direction * _speedForceLoot, ForceMode2D.Impulse);
    }

    public void TakeLoot(Player player)
    {
        var weapon = player.Shooting.AllWeapons.Find(x => x.Type == _type);
        player.Shooting.TakeWeapon(weapon.Type, _bullet);
        Destroy(gameObject);
    }
}
