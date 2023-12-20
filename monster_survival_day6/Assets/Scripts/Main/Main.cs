using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject enemySpawner;
    [SerializeField] GameObject hitPointRoot;
    [SerializeField] GameObject CameraObject;
    [SerializeField] GameObject levelUpUI;
    [SerializeField] GameObject gameOverUI;
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
    private HitPointUISystem hitPointUISystem;
    private CameraMoveSystem cameraMoveSystem;
    private LevelUpSystem levelUpSystem;
    private LevelUpUISystem levelUpUISystem;
    private GameOverSystem gameOverSystem;


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

        bulletMoveSystem = new BulletMoveSystem(gameEvent, player);
        bulletHitSystem = new BulletHitSystem(gameEvent, objectPool, enemyPrefab);

        damageSystem = new DamageSystem(gameEvent, player);

        hitPointUISystem = new HitPointUISystem(gameEvent, hitPointRoot);

        cameraMoveSystem = new CameraMoveSystem(gameEvent, player);

        levelUpSystem = new LevelUpSystem(gameEvent);
        levelUpUISystem = new LevelUpUISystem(gameEvent, player);

        gameOverSystem = new GameOverSystem(gameEvent, player);

        gameEvent.AddComponentList?.Invoke(player);
        gameEvent.AddComponentList?.Invoke(enemySpawner);
        gameEvent.AddComponentList?.Invoke(CameraObject);
        gameEvent.AddComponentList?.Invoke(levelUpUI);
        gameEvent.AddComponentList?.Invoke(gameOverUI);
    }

    void Update()
    {
        levelUpSystem.OnUpdate();
        levelUpUISystem.OnUpdate();

        if (gameEvent.GetIsLevelUP()) return;
        playerInputSystem.OnUpdate();
        playerAttackSystem.OnUpdate();
        characterMoveSystem.OnUpdate();
        enemySpawnerSystem.OnUpdate();
        enemyHitSystem.OnUpdate();
        bulletMoveSystem.Update();
        bulletHitSystem.OnUpdate();
        damageSystem.OnUpdate();
        hitPointUISystem.OnUpdate();
        cameraMoveSystem.OnUpdate();
        gameOverSystem.OnUpdate();
    }
}
