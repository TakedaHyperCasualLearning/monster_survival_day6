using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpComponent : MonoBehaviour
{
    private int level;
    private int experiencePoint;
    [SerializeField] private int experiencePointBorder;
    private bool isLevelUp;

    private int attackLevel;
    private int attackLevelOld;
    [SerializeField] private int attackRiseValue;
    private int attackBase;

    private int hitPointLevel;
    private int hitPointLevelOld;
    [SerializeField] private int hitPointRiseValue;
    private int hitPointBase;

    private int speedLevel;
    private int speedLevelOld;
    [SerializeField] private float speedRiseValue;
    private float speedBase;

    private int splitLevel;
    private int splitLevelOld;
    [SerializeField] private int splitRiseValue;
    private int splitBase;


    public int Level { get => level; set => level = value; }
    public int ExperiencePoint { get => experiencePoint; set => experiencePoint = value; }
    public int ExperiencePointBorder { get => experiencePointBorder; set => experiencePointBorder = value; }
    public bool IsLevelUp { get => isLevelUp; set => isLevelUp = value; }
    public int AttackLevel { get => attackLevel; set => attackLevel = value; }
    public int AttackLevelOld { get => attackLevelOld; set => attackLevelOld = value; }
    public int AttackRiseValue { get => attackRiseValue; set => attackRiseValue = value; }
    public int AttackBase { get => attackBase; set => attackBase = value; }
    public int HitPointLevel { get => hitPointLevel; set => hitPointLevel = value; }
    public int HitPointLevelOld { get => hitPointLevelOld; set => hitPointLevelOld = value; }
    public int HitPointRiseValue { get => hitPointRiseValue; set => hitPointRiseValue = value; }
    public int HitPointBase { get => hitPointBase; set => hitPointBase = value; }
    public int SpeedLevel { get => speedLevel; set => speedLevel = value; }
    public int SpeedLevelOld { get => speedLevelOld; set => speedLevelOld = value; }
    public float SpeedRiseValue { get => speedRiseValue; set => speedRiseValue = value; }
    public float SpeedBase { get => speedBase; set => speedBase = value; }
    public int SplitLevel { get => splitLevel; set => splitLevel = value; }
    public int SplitLevelOld { get => splitLevelOld; set => splitLevelOld = value; }
    public int SplitRiseValue { get => splitRiseValue; set => splitRiseValue = value; }
    public int SplitBase { get => splitBase; set => splitBase = value; }

}
