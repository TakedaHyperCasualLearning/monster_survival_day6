using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EnemySpawnerSystem
{
    private GameEvent gameEvent;
    private ObjectPool objectPool;
    private GameObject player;
    private Vector3 screenSize = new Vector3(0, 0, 0);
    private List<EnemySpawnerComponent> enemySpawnerComponentList = new List<EnemySpawnerComponent>();

    public EnemySpawnerSystem(GameEvent gameEvent, ObjectPool objectPool, GameObject player)
    {
        this.gameEvent = gameEvent;
        this.objectPool = objectPool;
        this.player = player;

        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;

        screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10.0f));
    }

    public void OnUpdate()
    {
        for (int i = 0; i < enemySpawnerComponentList.Count; i++)
        {
            EnemySpawnerComponent enemySpawnerComponent = enemySpawnerComponentList[i];
            if (!enemySpawnerComponent.gameObject.activeSelf) continue;

            if (enemySpawnerComponent.IntervalTimer < enemySpawnerComponent.SpawnInterval)
            {
                enemySpawnerComponent.IntervalTimer += Time.deltaTime;
                continue;
            }

            Spawn(enemySpawnerComponent);
        }

        List<GameObject> enemyList = objectPool.GetObjectList(enemySpawnerComponentList[0].EnemyPrefab);
        if (enemyList == null) return;
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (!enemyList[i].activeSelf) continue;
            enemyList[i].GetComponent<CharacterMoveComponent>().TargetPosition = player.transform.position;
        }
    }

    private void Spawn(EnemySpawnerComponent enemySpawnerComponent)
    {
        GameObject enemy = objectPool.GetObject(enemySpawnerComponent.EnemyPrefab);
        Vector3 spawnPosition = new Vector3(Random.Range(screenSize.x, screenSize.x + enemySpawnerComponent.PositionOffset.x), 0.0f, Random.Range(screenSize.y, screenSize.y + enemySpawnerComponent.PositionOffset.z));
        spawnPosition *= Random.Range(0, 2) == 0 ? 1 : -1;
        enemy.transform.position = player.transform.position + spawnPosition;
        enemySpawnerComponent.IntervalTimer = 0.0f;
        enemy.SetActive(true);
        if (!objectPool.IsNewGenerate)
        {
            enemy.GetComponent<HitPointUIComponent>().HitPointUI.gameObject.SetActive(true);
            enemy.GetComponent<CharacterBaseComponent>().HitPoint = enemy.GetComponent<CharacterBaseComponent>().HitPointMax;
            return;
        }
        gameEvent.AddComponentList?.Invoke(enemy);
        objectPool.IsNewGenerate = false;
    }

    private void AddComponentList(GameObject gameObject)
    {
        EnemySpawnerComponent enemySpawnerComponent = gameObject.GetComponent<EnemySpawnerComponent>();

        if (enemySpawnerComponent == null) return;

        enemySpawnerComponentList.Add(enemySpawnerComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        EnemySpawnerComponent enemySpawnerComponent = gameObject.GetComponent<EnemySpawnerComponent>();

        if (enemySpawnerComponent == null) return;

        enemySpawnerComponentList.Remove(enemySpawnerComponent);
    }
}
