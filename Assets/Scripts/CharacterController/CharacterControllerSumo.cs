using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerSumo : MonoBehaviour
{
    [SerializeField] private LocalBlackboard localBlackboard;


    private void Subscribe()
    {
        InputManager.Instance._useHorizontalInput[localBlackboard.controllerSet] += GrabHorzAxis;
        InputManager.Instance._useVerticalInput[localBlackboard.controllerSet] += GrabVertAxis;
    }







    #region Movement
    private Vector2 moveInput;


    private void GrabHorzAxis(float horzAxis)
    {
        moveInput.x = horzAxis;
    }

    private void GrabVertAxis(float vertAxis)
    {
        moveInput.y = vertAxis;
    }

    private void MovePlayer()
    {
        if(moveInput.x != 0 && moveInput.y != 0)
        {

        }
    }
    #endregion
}
