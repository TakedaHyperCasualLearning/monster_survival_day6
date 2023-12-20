using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem
{
    private GameEvent gameEvent;
    private List<DamageCommponent> damageCommponentList = new List<DamageCommponent>();
    private List<CharacterBaseComponent> characterBaseComponentList = new List<CharacterBaseComponent>();

    public DamageSystem(GameEvent gameEvent)
    {
        this.gameEvent = gameEvent;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < characterBaseComponentList.Count; i++)
        {
            CharacterBaseComponent characterBaseComponent = characterBaseComponentList[i];
            DamageCommponent damageCommponent = damageCommponentList[i];
            if (!characterBaseComponent.gameObject.activeSelf) continue;
            if (characterBaseComponent.HitPoint <= 0)
            {
                characterBaseComponent.gameObject.SetActive(false);
                continue;
            }
            if (!damageCommponent.IsDamage) continue;
            characterBaseComponent.HitPoint -= damageCommponent.DamagePoint;
            damageCommponent.IsDamage = false;
            damageCommponent.DamagePoint = 0;
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        DamageCommponent damageCommponent = gameObject.GetComponent<DamageCommponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();

        if (damageCommponent == null || characterBaseComponent == null) return;

        damageCommponentList.Add(damageCommponent);
        characterBaseComponentList.Add(characterBaseComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        DamageCommponent damageCommponent = gameObject.GetComponent<DamageCommponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();

        if (damageCommponent == null || characterBaseComponent == null) return;

        damageCommponentList.Remove(damageCommponent);
        characterBaseComponentList.Remove(characterBaseComponent);
    }
}
