using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackComponent : MonoBehaviour
{
    [SerializeField] private float attackInterval;
    private float intervalTimer;

    public float AttackInterval { get => attackInterval; set => attackInterval = value; }
    public float IntervalTimer { get => intervalTimer; set => intervalTimer = value; }
}
