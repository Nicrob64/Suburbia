using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TransitionSO", order = 1)]
public class TransitionSO : ScriptableObject
{
    public enum TransitionType { X, Y, Z, ROTATION, ALL, RX, RY, RZ}

    public TransitionType type;
    public Vector3 fromPos;
    public Vector3 toPos;
    public Vector3 fromRot;
    public Vector3 toRot;
    public float duration;
    public Easings.Functions easeType;
}
