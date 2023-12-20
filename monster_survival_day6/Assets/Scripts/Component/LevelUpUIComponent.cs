using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUIComponent : MonoBehaviour
{
    [SerializeField] private GameObject levelUpUI;
    [SerializeField] List<Button> levelUpButtonList;

    public GameObject LevelUpUI { get => levelUpUI; set => levelUpUI = value; }
    public List<Button> LevelUpButtonList { get => levelUpButtonList; set => levelUpButtonList = value; }
}
