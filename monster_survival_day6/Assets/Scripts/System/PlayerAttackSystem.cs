using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem
{
    private GameEvent gameEvent;
    private ObjectPool objectPool;
    private List<PlayerAttackComponent> playerAttackComponentList = new List<PlayerAttackComponent>();
    private List<CharacterBaseComponent> characterBaseComponentList = new List<CharacterBaseComponent>();
    private List<InputCommponent> inputCommponentList = new List<InputCommponent>();

    public PlayerAttackSystem(GameEvent gameEvent, ObjectPool objectPool)
    {
        this.gameEvent = gameEvent;
        this.objectPool = objectPool;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < playerAttackComponentList.Count; i++)
        {
            PlayerAttackComponent playerAttackComponent = playerAttackComponentList[i];
            if (!playerAttackComponent.gameObject.activeSelf) continue;

            if (playerAttackComponent.IntervalTimer < playerAttackComponent.AttackInterval)
            {
                playerAttackComponent.IntervalTimer += Time.deltaTime;
                continue;
            }

            if (!inputCommponentList[i].IsClick) continue;
            Attack(playerAttackComponent, characterBaseComponentList[i]);
        }
    }

    private void Attack(PlayerAttackComponent playerAttackComponent, CharacterBaseComponent characterBaseComponent)
    {
        playerAttackComponent.IntervalTimer = 0.0f;
        for (int i = 0; i < playerAttackComponent.Split; i++)
        {
            GameObject bullet = objectPool.GetObject(playerAttackComponent.BulletPrefab);
            bullet.transform.position = playerAttackComponent.gameObject.transform.position;
            Vector3 angle = new Vector3(0, 180 / (playerAttackComponent.Split + 1) * (i + 1) - 90, 0);
            Quaternion angleQuat = Quaternion.Euler(angle);
            bullet.GetComponent<BulletMoveComponenent>().Direction = angleQuat * playerAttackComponent.gameObject.transform.forward;
            bullet.GetComponent<BulletBaseComponent>().AttackPoint = characterBaseComponent.AttackPoint;
            bullet.SetActive(true);
            playerAttackComponent.IntervalTimer = 0.0f;
            if (!objectPool.IsNewGenerate) continue;
            gameEvent.AddComponentList?.Invoke(bullet);
            objectPool.IsNewGenerate = false;
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        PlayerAttackComponent playerAttackComponent = gameObject.GetComponent<PlayerAttackComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();
        InputCommponent inputCommponent = gameObject.GetComponent<InputCommponent>();

        if (playerAttackComponent == null) return;
        if (characterBaseComponent == null) return;
        if (inputCommponent == null) return;

        playerAttackComponentList.Add(playerAttackComponent);
        characterBaseComponentList.Add(characterBaseComponent);
        inputCommponentList.Add(inputCommponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        PlayerAttackComponent playerAttackComponent = gameObject.GetComponent<PlayerAttackComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();
        InputCommponent inputCommponent = gameObject.GetComponent<InputCommponent>();

        if (playerAttackComponent == null) return;
        if (characterBaseComponent == null) return;
        if (inputCommponent == null) return;

        playerAttackComponentList.Remove(playerAttackComponent);
        characterBaseComponentList.Remove(characterBaseComponent);
        inputCommponentList.Remove(inputCommponent);
    }
}
