using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitPointUIComponent : MonoBehaviour
{
    [SerializeField] private GameObject hitPointUIPrefab;
    private GameObject hitPointUIParent;
    private TextMeshPro hitPointUI;
    [SerializeField] Vector3 positionOffset;

    public GameObject HitPointUIPrefab { get => hitPointUIPrefab; set => hitPointUIPrefab = value; }
    public GameObject HitPointUIParent { get => hitPointUIParent; set => hitPointUIParent = value; }
    public TextMeshPro HitPointUI { get => hitPointUI; set => hitPointUI = value; }
    public Vector3 PositionOffset { get => positionOffset; set => positionOffset = value; }
}
