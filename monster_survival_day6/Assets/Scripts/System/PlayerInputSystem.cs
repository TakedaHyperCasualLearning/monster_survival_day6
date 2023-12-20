using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputSystem : MonoBehaviour
{

    private GameEvent gameEvent;
    private List<InputCommponent> inputCommponentList = new List<InputCommponent>();
    private List<CharacterMoveComponent> characterMoveComponentList = new List<CharacterMoveComponent>();

    public PlayerInputSystem(GameEvent gameEvent)
    {
        this.gameEvent = gameEvent;
        gameEvent.AddComponentList += AddComponentList;
        gameEvent.RemoveComponentList += RemoveComponentList;
    }

    public void OnUpdate()
    {
        for (int i = 0; i < inputCommponentList.Count; i++)
        {
            InputCommponent inputCommponent = inputCommponentList[i];
            CharacterMoveComponent characterMoveComponent = characterMoveComponentList[i];

            if (!inputCommponent.gameObject.activeSelf) return;
            MoveInput(characterMoveComponent);
            LookAtInput(characterMoveComponent);
            AttackInput(inputCommponent);
        }
    }

    private void MoveInput(CharacterMoveComponent characterMoveComponent)
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) direction += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) direction += Vector3.back;
        if (Input.GetKey(KeyCode.A)) direction += Vector3.left;
        if (Input.GetKey(KeyCode.D)) direction += Vector3.right;

        characterMoveComponent.Direction = direction;
    }

    private void LookAtInput(CharacterMoveComponent characterMoveComponent)
    {
        Vector3 playerPoint = Camera.main.WorldToScreenPoint(characterMoveComponent.gameObject.transform.position);
        Vector3 rotationDirection = Input.mousePosition - playerPoint;
        rotationDirection = rotationDirection.normalized;
        rotationDirection.z = 0.0f;
        characterMoveComponent.TargetPosition = Camera.main.ScreenToWorldPoint(playerPoint + rotationDirection);
    }

    private void AttackInput(InputCommponent inputCommponent)
    {
        if (Input.GetMouseButton(0))
        {
            inputCommponent.IsClick = true;
            return;
        }

        inputCommponent.IsClick = false;
    }

    private void AddComponentList(GameObject gameObject)
    {
        InputCommponent inputCommponent = gameObject.GetComponent<InputCommponent>();
        CharacterMoveComponent characterMoveComponent = gameObject.GetComponent<CharacterMoveComponent>();

        if (inputCommponent == null || characterMoveComponent == null) return;

        inputCommponentList.Add(inputCommponent);
        characterMoveComponentList.Add(characterMoveComponent);
    }


    private void RemoveComponentList(GameObject gameObject)
    {
        InputCommponent inputCommponent = gameObject.GetComponent<InputCommponent>();
        CharacterMoveComponent characterMoveComponent = gameObject.GetComponent<CharacterMoveComponent>();

        if (inputCommponent == null || characterMoveComponent == null) return;

        inputCommponentList.Remove(inputCommponent);
        characterMoveComponentList.Remove(characterMoveComponent);
    }
}
