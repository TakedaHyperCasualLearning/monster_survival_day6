using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HitPointUISystem
{
    private GameEvent gameEvent;
    private GameObject hitPointUIRoot;
    private List<HitPointUIComponent> hitPointUIComponentList = new List<HitPointUIComponent>();
    private List<CharacterBaseComponent> characterBaseComponentList = new List<CharacterBaseComponent>();

    public HitPointUISystem(GameEvent gameEvent, GameObject hitPointUIRoot)
    {
        this.gameEvent = gameEvent;
        this.hitPointUIRoot = hitPointUIRoot;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    private void Initialize(HitPointUIComponent hitPointUIComponent)
    {
        GameObject tempObject = GameObject.Instantiate(hitPointUIComponent.HitPointUIPrefab);
        tempObject.transform.SetParent(hitPointUIRoot.transform);
        hitPointUIComponent.HitPointUI = tempObject.GetComponent<TextMeshPro>();
    }

    public void OnUpdate()
    {
        for (int i = 0; i < hitPointUIComponentList.Count; i++)
        {
            HitPointUIComponent hitPointUIComponent = hitPointUIComponentList[i];
            CharacterBaseComponent characterBaseComponent = characterBaseComponentList[i];
            if (!hitPointUIComponent.gameObject.activeSelf)
            {
                hitPointUIComponent.HitPointUI.gameObject.SetActive(false);
                continue;
            }

            hitPointUIComponent.HitPointUI.gameObject.SetActive(true);
            hitPointUIComponent.HitPointUI.text = "HP:" + characterBaseComponent.HitPoint.ToString() + "/" + characterBaseComponent.HitPointMax.ToString();
            hitPointUIComponent.HitPointUI.transform.position = characterBaseComponent.gameObject.transform.position + hitPointUIComponent.PositionOffset;
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        HitPointUIComponent hitPointUIComponent = gameObject.GetComponent<HitPointUIComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();

        if (hitPointUIComponent == null || characterBaseComponent == null) return;

        hitPointUIComponentList.Add(hitPointUIComponent);
        characterBaseComponentList.Add(characterBaseComponent);

        Initialize(hitPointUIComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        HitPointUIComponent hitPointUIComponent = gameObject.GetComponent<HitPointUIComponent>();
        CharacterBaseComponent characterBaseComponent = gameObject.GetComponent<CharacterBaseComponent>();

        if (hitPointUIComponent == null || characterBaseComponent == null) return;

        hitPointUIComponentList.Remove(hitPointUIComponent);
        characterBaseComponentList.Remove(characterBaseComponent);
    }
}

