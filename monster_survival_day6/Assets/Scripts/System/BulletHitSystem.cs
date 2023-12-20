using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitSystem
{
    private GameEvent gameEvent;
    private ObjectPool objectPool;
    private GameObject enemyPrefab;
    private List<BulletBaseComponent> bulletBaseComponentList = new List<BulletBaseComponent>();

    public BulletHitSystem(GameEvent gameEvent, ObjectPool objectPool, GameObject enemyPrefab)
    {
        this.gameEvent = gameEvent;
        this.objectPool = objectPool;
        this.enemyPrefab = enemyPrefab;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < bulletBaseComponentList.Count; i++)
        {
            BulletBaseComponent bulletBaseComponent = bulletBaseComponentList[i];
            if (!bulletBaseComponent.gameObject.activeSelf) continue;

            List<GameObject> enemyList = objectPool.GetObjectList(enemyPrefab);
            if (enemyList == null) continue;

            for (int j = 0; j < enemyList.Count; j++)
            {
                if (!enemyList[j].activeSelf) continue;
                if ((bulletBaseComponent.transform.position - enemyList[j].transform.position).magnitude > (bulletBaseComponent.transform.localScale.x / 2) + (enemyList[j].gameObject.transform.localScale.x / 2)) continue;
                objectPool.RemoveObject(bulletBaseComponent.gameObject);
                Debug.Log("Hit");
                DamageCommponent enemyDamage = enemyList[j].GetComponent<DamageCommponent>();
                enemyDamage.DamagePoint += bulletBaseComponent.AttackPoint;
                enemyDamage.IsDamage = true;
                continue;
            }
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        BulletBaseComponent bulletBaseComponent = gameObject.GetComponent<BulletBaseComponent>();

        if (bulletBaseComponent == null) return;

        bulletBaseComponentList.Add(bulletBaseComponent);

    }

    private void RemoveComponentList(GameObject gameObject)
    {
        BulletBaseComponent bulletBaseComponent = gameObject.GetComponent<BulletBaseComponent>();

        if (bulletBaseComponent == null) return;

        bulletBaseComponentList.Remove(bulletBaseComponent);

    }
}
