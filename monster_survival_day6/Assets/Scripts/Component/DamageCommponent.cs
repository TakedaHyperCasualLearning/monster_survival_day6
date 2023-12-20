using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCommponent : MonoBehaviour
{
    private int damagePoint;
    private bool isDamage;
    private bool isDamageEffect;
    [SerializeField] private Renderer selfRenderer;
    [SerializeField] private int damageEffectCountMax;
    private int damageEffectCount;
    private float damageEffectTimer = 0.0f;
    [SerializeField] private float damageEffectTime;

    public int DamagePoint { get => damagePoint; set => damagePoint = value; }
    public bool IsDamage { get => isDamage; set => isDamage = value; }
    public bool IsDamageEffect { get => isDamageEffect; set => isDamageEffect = value; }
    public Renderer SelfRenderer { get => selfRenderer; set => selfRenderer = value; }
    public int DamageEffectCountMax { get => damageEffectCountMax; set => damageEffectCountMax = value; }
    public int DamageEffectCount { get => damageEffectCount; set => damageEffectCount = value; }
    public float DamageEffectTimer { get => damageEffectTimer; set => damageEffectTimer = value; }
    public float DamageEffectTime { get => damageEffectTime; set => damageEffectTime = value; }
}
