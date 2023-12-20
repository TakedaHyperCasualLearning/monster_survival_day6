using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitSystem : MonoBehaviour
{
    private GameEvent gameEvent;
    private GameObject playerObject;
    private List<DamageCommponent> damageCommponentList = new List<DamageCommponent>();
    private List<CharacterBaseComponent> characterBaseComponentList = new List<CharacterBaseComponent>();
    private float intervalTimer = 0.0f;
    private float interval = 0.5f;

    public EnemyHitSystem(GameEvent gameEvent, GameObject gameObject)
    {
        this.gameEvent = gameEvent;
        this.playerObject = gameObject;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void OnUpdate()
    {
        if (intervalTimer < interval)
        {
            intervalTimer += Time.deltaTime;
            return;
        }

        for (int i = 0; i < damageCommponentList.Count; i++)
        {
            CharacterBaseComponent characterBaseComponent = characterBaseComponentList[i];
            DamageCommponent damageCommponent = damageCommponentList[i];
            if (!damageCommponent.gameObject.activeSelf) continue;
            if (damageCommponent.gameObject == playerObject) continue;

            if ((characterBaseComponent.transform.position - playerObject.transform.position).magnitude > (characterBaseComponent.transform.localScale.x / 2) + (playerObject.transform.localScale.x / 2)) continue;
            DamageCommponent playerDamage = playerObject.GetComponent<DamageCommponent>();
            playerDamage.DamagePoint += characterBaseComponent.AttackPoint;
            playerDamage.IsDamage = true;
            intervalTimer = 0.0f;
            continue;
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
