using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBaseComponent : MonoBehaviour
{
    [SerializeField] private int attackPoint;

    public int AttackPoint { get => attackPoint; set => attackPoint = value; }
}
