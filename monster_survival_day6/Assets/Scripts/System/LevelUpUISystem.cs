using System.Collections;
using System.Collections.Generic;
using TMPro;
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
                RandomButton(levelUpUIComponent);
                continue;
            }

            if (levelUpComponent.IsLevelUp) continue;
            levelUpUIComponent.gameObject.SetActive(false);
        }
    }

    public void OnClickLevelUpButton()
    {
        LevelUpComponent levelUPComponent = playerObject.gameObject.GetComponent<LevelUpComponent>();

        if (levelUPComponent == null) return;

        levelUPComponent.Level += 1;
        gameEvent.LevelUp?.Invoke();
        levelUPComponent.IsLevelUp = false;
    }

    private void RandomButton(LevelUpUIComponent levelUpUIComponent)
    {
        List<int> randomList = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            int temp = Random.Range(0, 4);
            if (randomList.Contains(temp))
            {
                i--;
                continue;
            }
            randomList.Add(temp);
        }
        levelUpUIComponent.RandomButton = randomList;

        levelUpUIComponent.LevelUpButtonList[0].onClick.RemoveAllListeners();
        levelUpUIComponent.LevelUpButtonList[1].onClick.RemoveAllListeners();
        levelUpUIComponent.LevelUpButtonList[2].onClick.RemoveAllListeners();

        for (int i = 0; i < 3; i++)
        {
            switch (randomList[i])
            {
                case 0:
                    levelUpUIComponent.LevelUpButtonList[i].onClick.AddListener(OnClickAttackButton);
                    levelUpUIComponent.LevelUpButtonList[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Attack";
                    break;
                case 1:
                    levelUpUIComponent.LevelUpButtonList[i].onClick.AddListener(OnClickHitPointButton);
                    levelUpUIComponent.LevelUpButtonList[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "HitPoint";
                    break;
                case 2:
                    levelUpUIComponent.LevelUpButtonList[i].onClick.AddListener(OnClickAttackSpeedButton);
                    levelUpUIComponent.LevelUpButtonList[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "AttackSpeed";
                    break;
                case 3:
                    levelUpUIComponent.LevelUpButtonList[i].onClick.AddListener(OnClickSplitButton);
                    levelUpUIComponent.LevelUpButtonList[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Split";
                    break;
            }
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

    public void OnClickSplitButton()
    {
        LevelUpComponent levelUPComponent = playerObject.gameObject.GetComponent<LevelUpComponent>();

        if (levelUPComponent == null) return;

        levelUPComponent.SplitLevel += 1;
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
