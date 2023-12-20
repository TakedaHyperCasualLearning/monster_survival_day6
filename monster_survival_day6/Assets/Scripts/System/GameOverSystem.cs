using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSystem : MonoBehaviour
{
    private GameEvent gameEvent;
    private GameObject playerObject;
    private List<GameOverComponent> gameOverComponentList = new List<GameOverComponent>();

    public GameOverSystem(GameEvent gameEvent, GameObject playerObject)
    {
        this.gameEvent = gameEvent;
        this.playerObject = playerObject;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < gameOverComponentList.Count; i++)
        {
            GameOverComponent gameOverComponent = gameOverComponentList[i];

            if (playerObject.GetComponent<CharacterBaseComponent>().HitPoint > 0) return;

            gameOverComponent.gameObject.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
            }
        }
    }

    private void AddComponentList(GameObject gameObject)
    {
        GameOverComponent gameOverComponent = gameObject.GetComponent<GameOverComponent>();
        if (gameOverComponent == null) return;
        gameOverComponentList.Add(gameOverComponent);
    }

    private void RemoveComponentList(GameObject gameObject)
    {
        GameOverComponent gameOverComponent = gameObject.GetComponent<GameOverComponent>();
        if (gameOverComponent == null) return;
        gameOverComponentList.Remove(gameOverComponent);
    }

}
