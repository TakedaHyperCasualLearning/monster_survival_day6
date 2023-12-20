using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverComponent : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;

    public GameObject GameOverUI { get => gameOverUI; set => gameOverUI = value; }
}
