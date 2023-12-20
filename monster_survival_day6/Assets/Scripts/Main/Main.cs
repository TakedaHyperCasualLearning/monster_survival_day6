using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject enemySpawner;
    private GameEvent gameEvent;
    private ObjectPool objectPool;
    private CharacterMoveSystem characterMoveSystem;
    private PlayerInputSystem playerInputSystem;
    private PlayerAttackSystem playerAttackSystem;
    private EnemySpawnerSystem enemySpawnerSystem;
    private EnemyHitSystem enemyHitSystem;
    private BulletMoveSystem bulletMoveSystem;
    private BulletHitSystem bulletHitSystem;
    private DamageSystem damageSystem;

    void Start()
    {
        GameObject player = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        gameEvent = new GameEvent();
        objectPool = new ObjectPool(gameEvent);

        characterMoveSystem = new CharacterMoveSystem(gameEvent);
        playerInputSystem = new PlayerInputSystem(gameEvent);
        playerAttackSystem = new PlayerAttackSystem(gameEvent, objectPool);

        enemySpawnerSystem = new EnemySpawnerSystem(gameEvent, objectPool, player);
        enemyHitSystem = new EnemyHitSystem(gameEvent, player);

        bulletMoveSystem = new BulletMoveSystem(gameEvent);
        bulletHitSystem = new BulletHitSystem(gameEvent, objectPool, enemyPrefab);

        damageSystem = new DamageSystem(gameEvent);

        gameEvent.AddComponentList?.Invoke(player);
        gameEvent.AddComponentList?.Invoke(enemySpawner);
    }

    void Update()
    {
        playerInputSystem.OnUpdate();
        playerAttackSystem.OnUpdate();
        characterMoveSystem.OnUpdate();
        enemySpawnerSystem.OnUpdate();
        enemyHitSystem.OnUpdate();
        bulletMoveSystem.Update();
        bulletHitSystem.OnUpdate();
        damageSystem.OnUpdate();
    }
}
