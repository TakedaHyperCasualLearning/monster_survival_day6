using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveComponent : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 direction;
    [SerializeField] private bool isChase;
    [SerializeField] private bool isLookAt;
    private Vector3 targetPosition;

    public float Speed { get => speed; set => speed = value; }
    public Vector3 Direction { get => direction; set => direction = value; }
    public bool IsChase { get => isChase; set => isChase = value; }
    public bool IsLookAt { get => isLookAt; set => isLookAt = value; }
    public Vector3 TargetPosition { get => targetPosition; set => targetPosition = value; }
}
