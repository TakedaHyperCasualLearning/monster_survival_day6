using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private GameEvent gameEvent;
    Dictionary<int, List<GameObject>> objectPool = new Dictionary<int, List<GameObject>>();
    private bool isNewGenerate;

    public ObjectPool(GameEvent gameEvent)
    {
        this.gameEvent = gameEvent;
    }

    public GameObject GetObject(GameObject prefab)
    {
        int key = prefab.GetHashCode();
        if (objectPool.ContainsKey(key))
        {
            List<GameObject> tempLis = objectPool[key];
            for (int i = 0; i < tempLis.Count; i++)
            {
                if (!tempLis[i].activeSelf)
                {
                    tempLis[i].SetActive(true);
                    return tempLis[i];
                }

                GameObject tempObject = GameObject.Instantiate(prefab);
                tempLis.Add(tempObject);
                gameEvent.AddComponentList(tempObject);
                isNewGenerate = true;
                return tempObject;
            }
        }

        List<GameObject> list = new List<GameObject>();
        objectPool.Add(key, list);
        GameObject gameObject = GameObject.Instantiate(prefab);
        list.Add(gameObject);
        gameEvent.AddComponentList(gameObject);
        isNewGenerate = true;
        return gameObject;
    }

    public void RemoveObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public List<GameObject> GetObjectList(GameObject prefab)
    {
        int key = prefab.GetHashCode();
        if (objectPool.ContainsKey(key))
        {
            return objectPool[key];
        }
        return null;
    }

    public bool IsNewGenerate { get => isNewGenerate; set => isNewGenerate = value; }
}
