using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpUISystem
{
    private GameEvent gameEvent;
    private GameObject playerObject;
    private List<LevelUpUIComponent> levelUpUIComponentList = new List<LevelUpUIComponent>();

    public LevelUpUISystem(GameEvent gameEvent, GameObject player)
    {
        this.gameEvent = gameEvent;
        this.playerObject = player;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    private void Initialize(LevelUpUIComponent levelUpUIComponent)
    {
        levelUpUIComponent.LevelUpButtonList[0].onClick.AddListener(OnClickAttackButton);
        levelUpUIComponent.LevelUpButtonList[1].onClick.AddListener(OnClickHitPointButton);
        levelUpUIComponent.LevelUpButtonList[2].onClick.AddListener(OnClickAttackSpeedButton);
    }

    public void OnUpdate()
    {
        for (int i = 0; i < levelUpUIComponentList.Count; i++)
        {
            LevelUpUIComponent levelUpUIComponent = levelUpUIComponentList[i];
            LevelUpComponent levelUpComponent = playerObject.GetComponent<LevelUpComponent>();

            if (levelUpComponent.IsLevelUp && !levelUpUIComponent.gameObject.activeSelf)
            {
                levelUpUIComponent.gameObject.SetActive(true);
                continue;
            }

            if (levelUpComponent.IsLevelUp) continue;
            levelUpUIComponent.gameObject.SetActive(false);
        }
    }


    public void OnClickAttackButton()
    {
        LevelUpComponent levelUPComponent = playerObject.gameObject.GetComponent<LevelUpComponent>();

        if (levelUPComponent == null) return;

        levelUPComponent.AttackLevel += 1;
        gameEvent.LevelUp?.Invoke();
        levelUPComponent.IsLevelUp = false;
    }

    public void OnClickHitPointButton()
    {
        LevelUpComponent levelUPComponent = playerObject.gameObject.GetComponent<LevelUpComponent>();

        if (levelUPComponent == null) return;

        levelUPComponent.HitPointLevel += 1;
        gameEvent.LevelUp?.Invoke();
        levelUPComponent.IsLevelUp = false;
    }

    public void OnClickAttackSpeedButton()
    {
        LevelUpComponent levelUPComponent = playerObject.gameObject.GetComponent<LevelUpComponent>();

        if (levelUPComponent == null) return;

        levelUPComponent.SpeedLevel += 1;
        gameEvent.LevelUp?.Invoke();
        levelUPComponent.IsLevelUp = false;
    }

    private void AddComponentList(GameObject gameObject)
    {
        LevelUpUIComponent levelUpUIComponent = gameObject.GetComponent<LevelUpUIComponent>();

        if (levelUpUIComponent == null) return;

        levelUpUIComponentList.Add(levelUpUIComponent);

        Initialize(levelUpUIComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        LevelUpUIComponent levelUpUIComponent = gameObject.GetComponent<LevelUpUIComponent>();

        if (levelUpUIComponent == null) return;

        levelUpUIComponentList.Remove(levelUpUIComponent);
    }
}
