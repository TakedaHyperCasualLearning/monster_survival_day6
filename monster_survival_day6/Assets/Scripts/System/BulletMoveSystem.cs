using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveSystem
{
    private GameEvent gameEvent;

    private List<BulletMoveComponenent> bulletMoveComponenentList = new List<BulletMoveComponenent>();

    public BulletMoveSystem(GameEvent gameEvent)
    {
        this.gameEvent = gameEvent;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void Update()
    {
        for (int i = 0; i < bulletMoveComponenentList.Count; i++)
        {
            BulletMoveComponenent bulletMoveComponenent = bulletMoveComponenentList[i];
            if (!bulletMoveComponenent.gameObject.activeSelf) continue;
            bulletMoveComponenent.transform.Translate(bulletMoveComponenent.Direction * bulletMoveComponenent.Speed * Time.deltaTime, Space.Self);
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        BulletMoveComponenent bulletMoveComponenent = gameObject.GetComponent<BulletMoveComponenent>();

        if (bulletMoveComponenent == null) return;

        bulletMoveComponenentList.Add(bulletMoveComponenent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        BulletMoveComponenent bulletMoveComponenent = gameObject.GetComponent<BulletMoveComponenent>();

        if (bulletMoveComponenent == null) return;

        bulletMoveComponenentList.Remove(bulletMoveComponenent);
    }
}
