using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackComponent : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float attackInterval;
    [SerializeField] private int split;
    private float intervalTimer;

    public GameObject BulletPrefab { get => bulletPrefab; set => bulletPrefab = value; }
    public float AttackInterval { get => attackInterval; set => attackInterval = value; }
    public int Split { get => split; set => split = value; }
    public float IntervalTimer { get => intervalTimer; set => intervalTimer = value; }
}
