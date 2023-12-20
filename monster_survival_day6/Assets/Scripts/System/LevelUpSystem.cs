using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpSystem
{
    private GameEvent gameEvent;
    private List<LevelUpComponent> levelUpComponentList = new List<LevelUpComponent>();
    private List<CharacterBaseComponent> characterBaseComponentList = new List<CharacterBaseComponent>();
    private List<PlayerAttackComponent> playerAttackComponentList = new List<PlayerAttackComponent>();

    public LevelUpSystem(GameEvent gameEvent)
    {
        this.gameEvent = gameEvent;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
        gameEvent.LevelUp += UpdateLevelUpStatus;
        gameEvent.GetIsLevelUP += IsGetLevelUP;
    }

    private void Initialize(LevelUpComponent levelUpComponent, CharacterBaseComponent characterBaseComponent, PlayerAttackComponent playerAttackComponent)
    {
        levelUpComponent.AttackBase = characterBaseComponent.AttackPoint;
        levelUpComponent.HitPointBase = characterBaseComponent.HitPointMax;
        levelUpComponent.SpeedBase = playerAttackComponent.AttackInterval;
        levelUpComponent.SplitBase = playerAttackComponent.Split;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < levelUpComponentList.Count; i++)
        {
            LevelUpComponent levelUpComponent = levelUpComponentList[i];
            if (!levelUpComponent.gameObject.activeSelf) continue;
            if (levelUpComponent.ExperiencePoint < levelUpComponent.ExperiencePointBorder) continue;

            levelUpComponent.ExperiencePoint -= levelUpComponent.ExperiencePointBorder;
            levelUpComponent.Level++;
            levelUpComponent.IsLevelUp = true;
        }
    }

    private void UpdateLevelUpStatus()
    {
        for (int i = 0; i < levelUpComponentList.Count; i++)
        {
            LevelUpComponent levelUpComponent = levelUpComponentList[i];
            if (!levelUpComponent.IsLevelUp) continue;

            if (levelUpComponent.AttackLevel != levelUpComponent.AttackLevelOld)
            {
                levelUpComponent.AttackLevelOld = levelUpComponent.AttackLevel;
                characterBaseComponentList[i].AttackPoint = levelUpComponent.AttackBase + levelUpComponent.AttackRiseValue * levelUpComponent.AttackLevel;
                Debug.Log("attack base" + levelUpComponent.AttackBase + "attack rise" + levelUpComponent.AttackRiseValue + "attack level" + levelUpComponent.AttackLevel);
            }
            if (levelUpComponent.SpeedLevel != levelUpComponent.SpeedLevelOld)
            {
                levelUpComponent.SpeedLevelOld = levelUpComponent.SpeedLevel;
                playerAttackComponentList[i].AttackInterval = levelUpComponent.SpeedBase - levelUpComponent.SpeedRiseValue * levelUpComponent.SpeedLevel;
            }
            if (levelUpComponent.HitPointLevel != levelUpComponent.HitPointLevelOld)
            {
                levelUpComponent.HitPointLevelOld = levelUpComponent.HitPointLevel;
                characterBaseComponentList[i].HitPoint = characterBaseComponentList[i].HitPoint + levelUpComponent.HitPointRiseValue;
                characterBaseComponentList[i].HitPointMax = levelUpComponent.HitPointBase + levelUpComponent.HitPointRiseValue * levelUpComponent.HitPointLevel;
            }
            if (levelUpComponent.SplitLevel != levelUpComponent.SplitLevelOld)
            {
                levelUpComponent.SplitLevelOld = levelUpComponent.SplitLevel;
                playerAttackComponentList[i].Split = levelUpComponent.SplitBase + levelUpComponent.SplitRiseValue * levelUpComponent.SplitLevel;
                Debug.Log("split base" + levelUpComponent.SplitBase + "split rise" + levelUpComponent.SplitRiseValue + "split level" + levelUpComponent.SplitLevel);
            }
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        LevelUpComponent levelUpComponent = gameObject.GetComponent<LevelUpComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();
        PlayerAttackComponent playerAttackComponent = gameObject.GetComponent<PlayerAttackComponent>();

        if (levelUpComponent == null || characterBaseComponent == null || playerAttackComponent == null) return;

        levelUpComponentList.Add(levelUpComponent);
        characterBaseComponentList.Add(characterBaseComponent);
        playerAttackComponentList.Add(playerAttackComponent);

        Initialize(levelUpComponent, characterBaseComponent, playerAttackComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        LevelUpComponent levelUpComponent = gameObject.GetComponent<LevelUpComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();
        PlayerAttackComponent playerAttackComponent = gameObject.GetComponent<PlayerAttackComponent>();

        if (levelUpComponent == null || characterBaseComponent == null || playerAttackComponent == null) return;

        levelUpComponentList.Remove(levelUpComponent);
        characterBaseComponentList.Remove(characterBaseComponent);
        playerAttackComponentList.Remove(playerAttackComponent);
    }

    public bool IsGetLevelUP() { return levelUpComponentList[0].IsLevelUp; }
}
