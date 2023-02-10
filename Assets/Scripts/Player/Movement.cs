using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;

    [Header("Run")]
    [SerializeField] private float _maxSpeedTime = 5;
    [SerializeField] private float _curSpeedTime;

    [SerializeField] private float _maxCooldownTime = 0.5f;
    [SerializeField] private float _curCooldownTime;

    [SerializeField] private float _smooth;
    [SerializeField] private float _runSpeed;
    [SerializeField] private bool _isActiveRun => Input.GetButton("Run");
    private bool _isRun = false;
    private bool _isSpeedTimeFull;

    private Rigidbody2D _rb;
    private Vector2 _mousePosition;
    private Camera _camera;

    private void Start() 
    {
        _rb = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }

    private void Update()
    {
        _mousePosition = _camera.ScreenToWorldPoint( Input.mousePosition);
        _rb.velocity = new Vector2();
        Vector3 movePose;
        
        if(_isActiveRun && _curSpeedTime > 0)
        {
            movePose = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * _runSpeed;
            _curSpeedTime -= Time.deltaTime;
            _isRun = true;
        }

        else
        {
            movePose = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * _speed;
            _isRun = false;
        }

        if(_curSpeedTime <= 0 && _curCooldownTime <= 0)
        {
            _curCooldownTime = _maxCooldownTime;
        }

        if(_curCooldownTime > 0)
        {
            _curCooldownTime -= Time.deltaTime;
        }

        if(_curCooldownTime <= 0 && _curSpeedTime < _maxSpeedTime && !_isRun)
        {
            _curSpeedTime += Time.deltaTime;
        }
        _rb.velocity = movePose; 


    }

    private void FixedUpdate() 
    {
        Vector2 lookDir = _mousePosition - _rb.position;
        float angle = Mathf.Atan2(lookDir.y,lookDir.x) * Mathf.Rad2Deg - 90;
        _rb.rotation = angle;
    }
}
