using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    private Coroutine lookLerp;
    public float turnTime;
    private Vector2 moveInputirection;
    public void OnMove(InputAction.CallbackContext obj)
    {
        if (!obj.performed)
        {
            return;
        }
        moveInputirection = obj.ReadValue<Vector2>();
        var lookDirection = new Vector3(moveInputirection.x, 0, moveInputirection.y);
        if (lookLerp != null)
        {
            StopCoroutine(lookLerp);
        }
        lookLerp = StartCoroutine(LookLerp(lookDirection));
        
    }
    private IEnumerator LookLerp(Vector3 lookDirection)
    {
        var startTurnTime = Time.time;
        var EndTurnTime = startTurnTime + turnTime;
        var startTurn = transform.rotation;
        var endTurn = Quaternion.LookRotation (lookDirection);
        while (startTurnTime < EndTurnTime)
        {
            var currentTurn = Time.time - startTurnTime;
            var Percentage = currentTurn / turnTime;
            transform.rotation = Quaternion.Lerp(startTurn, endTurn, Percentage);
            yield return null;
        }
    }
}
