using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public Action<GameObject> AddComponentList;
    public Action<GameObject> RemoveComponentList;
    public Action LevelUp;
    public Action<GameObject> ReleaseObject;
    public Func<bool> GetIsLevelUP;
}
