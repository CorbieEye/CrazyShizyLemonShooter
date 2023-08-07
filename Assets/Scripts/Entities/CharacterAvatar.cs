using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAvatar : MonoBehaviour
{
    [SerializeField]
    private GameObject _model;
    [SerializeField]
    private Transform _leftHand, rightHand;
    [SerializeField]
    private Transform[] _bones;
    [SerializeField]
    private Animator _animator;


}
