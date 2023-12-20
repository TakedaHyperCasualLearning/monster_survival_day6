using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerComponent : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] private float spawnInterval;
    private float intervalTimer;
    [SerializeField] private Vector3 positionOffset;

    public GameObject EnemyPrefab { get => enemyPrefab; set => enemyPrefab = value; }
    public float SpawnInterval { get => spawnInterval; set => spawnInterval = value; }
    public float IntervalTimer { get => intervalTimer; set => intervalTimer = value; }
    public Vector3 PositionOffset { get => positionOffset; set => positionOffset = value; }
}
