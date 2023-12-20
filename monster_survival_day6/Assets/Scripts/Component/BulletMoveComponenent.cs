using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveComponenent : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 direction;

    public float Speed { get => speed; set => speed = value; }
    public Vector3 Direction { get => direction; set => direction = value; }
}
