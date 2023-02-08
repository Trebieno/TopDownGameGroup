using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Pool))]
public class Spawn : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;
    [SerializeField] private GameObject _npcPrefub;

    [SerializeField] private float _maxdelay;
    [SerializeField] private float _curDelay;

    [SerializeField] private float _curTime;
    [SerializeField] private int _countNpc;
    
    private bool _isStart;

    public void StartSpawn(int count)
    {
        _isStart = true;
        _countNpc = count;
        _curTime = Random.Range(0, 60);
    }

    private void Update() 
    {
        if(_curDelay > 0)
            _curDelay -= Time.deltaTime;

        if(_curDelay <= 0 && _countNpc > 0)
        {
            Instantiate(_npcPrefub, _points[Random.Range(0, _points.Count)].position, Quaternion.identity);
            _curDelay = _maxdelay;
            _countNpc--;

            if(_countNpc - 1 <= 0)
                _isStart = false;
        }


        if(_curTime > 0 && !_isStart)
            _curTime -= Time.deltaTime;

        if(_curTime <= 0 && !_isStart)
        {
            StartSpawn(Random.Range(1, 5));
        }
    }
}
