using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveSystem
{
    private GameEvent gameEvent;
    private GameObject playerObject;
    private List<BulletMoveComponenent> bulletMoveComponenentList = new List<BulletMoveComponenent>();

    public BulletMoveSystem(GameEvent gameEvent, GameObject player)
    {
        this.gameEvent = gameEvent;
        this.playerObject = player;
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

            if (Vector3.Distance(bulletMoveComponenent.transform.position, playerObject.transform.position) > 8.0f)
            {
                gameEvent.ReleaseObject(bulletMoveComponenent.gameObject);
                continue;
            }
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
