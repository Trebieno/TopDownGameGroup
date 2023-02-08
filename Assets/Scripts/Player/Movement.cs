using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private bool _isActiveRun => Input.GetButton("Run");
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
        
        if(_isActiveRun)
            movePose = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * _runSpeed;
        else
            movePose = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * _speed;

        _rb.velocity = movePose; 


    }

    private void FixedUpdate() 
    {
        Vector2 lookDir = _mousePosition - _rb.position;
        float angle = Mathf.Atan2(lookDir.y,lookDir.x) * Mathf.Rad2Deg - 90;
        _rb.rotation = angle;
    }
}
