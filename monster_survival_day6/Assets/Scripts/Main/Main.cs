using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemySpawner;
    private GameEvent gameEvent;
    private ObjectPool objectPool;
    private CharacterMoveSystem characterMoveSystem;
    private PlayerInputSystem playerInputSystem;
    private PlayerAttackSystem playerAttackSystem;
    private EnemySpawnerSystem enemySpawnerSystem;
    private BulletMoveSystem bulletMoveSystem;

    void Start()
    {
        GameObject player = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        gameEvent = new GameEvent();
        objectPool = new ObjectPool(gameEvent);

        characterMoveSystem = new CharacterMoveSystem(gameEvent);
        playerInputSystem = new PlayerInputSystem(gameEvent);
        playerAttackSystem = new PlayerAttackSystem(gameEvent, objectPool);

        enemySpawnerSystem = new EnemySpawnerSystem(gameEvent, objectPool, player);

        bulletMoveSystem = new BulletMoveSystem(gameEvent);

        gameEvent.AddComponentList?.Invoke(player);
        gameEvent.AddComponentList?.Invoke(enemySpawner);
    }

    void Update()
    {
        playerInputSystem.OnUpdate();
        playerAttackSystem.OnUpdate();
        characterMoveSystem.OnUpdate();
        enemySpawnerSystem.OnUpdate();
        bulletMoveSystem.Update();
    }
}
