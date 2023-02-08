using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IUsable
{
    public Transform anchor; // дверная петля
	public float distance = 20; // если игрок уходит на большую дистанцию - скрипт отключается, для оптимизации
	public float openAngle = 120;
	public float closeAngle = 0;
	public float smooth = 2;

    private Transform _player;

    [SerializeField] private Transform _point1;
    [SerializeField] private Transform _point2;
    [SerializeField] private bool _isOpen;

    [SerializeField] private float _maxTimeLock = 0.4f;
    [SerializeField] private float _curTimeLock;

    private void Awake() 
	{
		openAngle = Mathf.Abs(openAngle);
		closeAngle = Mathf.Abs(closeAngle);
		if(_isOpen) anchor.localRotation = Quaternion.Euler(0, 0, openAngle);
	}

    private void Update() 
	{
        if(_isOpen)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, openAngle);
            anchor.localRotation = Quaternion.Lerp(anchor.localRotation, rotation, smooth * Time.deltaTime);
        }
        else
        {
            Quaternion rotation = Quaternion.Euler(0, 0, closeAngle);
            anchor.localRotation = Quaternion.Lerp(anchor.localRotation, rotation, smooth * Time.deltaTime);
        }
	}

    public void Use(Player player)
    {
        _isOpen = !_isOpen;
        _player = player.transform;
        if(Vector2.Distance(_player.position, _point1.position) < Vector2.Distance(_player.position, _point2.position))
        {
            // openAngle = -openAngle * -1;
        }
        else
        {
            openAngle = openAngle * -1;
        }
    }

}
