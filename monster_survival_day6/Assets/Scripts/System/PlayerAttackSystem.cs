using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem
{
    private GameEvent gameEvent;
    private List<PlayerAttackComponent> playerAttackComponentList = new List<PlayerAttackComponent>();
    private List<InputCommponent> inputCommponentList = new List<InputCommponent>();

    public PlayerAttackSystem(GameEvent gameEvent)
    {
        this.gameEvent = gameEvent;
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
            Attack(playerAttackComponent);
        }
    }

    private void Attack(PlayerAttackComponent playerAttackComponent)
    {
        playerAttackComponent.IntervalTimer = 0.0f;
        Debug.Log("Attack");
    }

    private void AddComponentList(GameObject gameObject)
    {
        PlayerAttackComponent playerAttackComponent = gameObject.GetComponent<PlayerAttackComponent>();
        InputCommponent inputCommponent = gameObject.GetComponent<InputCommponent>();

        if (playerAttackComponent == null || inputCommponent == null) return;

        playerAttackComponentList.Add(playerAttackComponent);
        inputCommponentList.Add(inputCommponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        PlayerAttackComponent playerAttackComponent = gameObject.GetComponent<PlayerAttackComponent>();
        InputCommponent inputCommponent = gameObject.GetComponent<InputCommponent>();

        if (playerAttackComponent == null || inputCommponent == null) return;

        playerAttackComponentList.Remove(playerAttackComponent);
        inputCommponentList.Remove(inputCommponent);
    }
}
