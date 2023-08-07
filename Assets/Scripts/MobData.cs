using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobData : MonoBehaviour
{
    [SerializeField] private float _viewDistance;
    [SerializeField] private float _distanceToStopToEnemy;
    [SerializeField] private float _distanceToStopToPointToGo;

    // ���� �������� ������ �������� �������, �� �������� ������ ������������ � ���� ���������� � �� ������ ��������� ������ �������� �� ����
    public Character Aggressor;
    [SerializeField] public readonly float DistanceToCallBros = 50f;

    public float ViewDistance { get { return _viewDistance; } }
    public float DistanceToStopToEnemy { get { return _distanceToStopToEnemy; } }
    public float DistanceToStopToPointToGo { get { return _distanceToStopToPointToGo; } }
}
