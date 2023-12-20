using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCommponent : MonoBehaviour
{
    private int damagePoint;
    private bool isDamage;

    public int DamagePoint { get => damagePoint; set => damagePoint = value; }
    public bool IsDamage { get => isDamage; set => isDamage = value; }
}
