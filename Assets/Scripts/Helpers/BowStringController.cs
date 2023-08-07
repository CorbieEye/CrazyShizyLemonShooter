using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowStringController : MonoBehaviour
{
    //Set all private serialized fields from inspector
    [SerializeField]
    private Transform _leftShoulder, _rightShoulder, _arrowCenter;
    [SerializeField]
    private LineRenderer _bowstring;
    [SerializeField]
    private Material _material;

    private Vector3[] _points;

    public void Start()
    {
        _points = new Vector3[3];
        _bowstring.positionCount = 3;
        _bowstring.material = _material;
    }

    public void Update()
    {
        _points[0] = _leftShoulder.position;
        _points[1] = _arrowCenter.position;
        _points[2] = _rightShoulder.position;
        _bowstring.SetPositions(_points);
    }
}
