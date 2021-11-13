using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerSumo : MonoBehaviour
{
    [SerializeField] private LocalBlackboard localBlackboard;

    private void Start()
    {
        Subscribe();
    }
    private void Subscribe()
    {
        InputManager.Instance._useHorizontalInput[localBlackboard.controllerSet] += GrabHorzAxis;
        InputManager.Instance._useVerticalInput[localBlackboard.controllerSet] += GrabVertAxis;
    }
    private void UnSubscribe()
    {
        InputManager.Instance._useHorizontalInput[localBlackboard.controllerSet] -= GrabHorzAxis;
        InputManager.Instance._useVerticalInput[localBlackboard.controllerSet] -= GrabVertAxis;
    }




    private void Update()
    {
        MovePlayer();
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
        Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y);


        if(moveInput.x != 0 || moveInput.y != 0)
        {
            localBlackboard.rb.AddForce(moveDir * localBlackboard.moveSpeed);
        }
    }
    #endregion
}
