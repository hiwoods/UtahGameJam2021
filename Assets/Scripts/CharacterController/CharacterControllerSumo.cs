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



    #region Subscription Crap
    private void Subscribe()
    {
        InputManager.Instance._useHorizontalInput[localBlackboard.controllerSet] += GrabHorzAxis;
        InputManager.Instance._useVerticalInput[localBlackboard.controllerSet] += GrabVertAxis;
        InputManager.Instance._useDashButton[localBlackboard.controllerSet] += Dash;
    }
    private void UnSubscribe()
    {
        InputManager.Instance._useHorizontalInput[localBlackboard.controllerSet] -= GrabHorzAxis;
        InputManager.Instance._useVerticalInput[localBlackboard.controllerSet] -= GrabVertAxis;
    }
    #endregion



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



    private bool canDash = true;
    private void Dash()
    {
        if (!canDash)
            return;


        Vector3 moveDir = new Vector3(localBlackboard.moverTransform.eulerAngles.x, 0, localBlackboard.moverTransform.eulerAngles.z);

        localBlackboard.rb.AddForce(moveDir * localBlackboard.dashSpeed, ForceMode.VelocityChange);
        StartCoroutine(DashCooldown());
    }


    private IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(localBlackboard.dashCooldownTime);
        canDash = true;
    }


    private void ClampSpeed()
    {
        float tempMagnitude = localBlackboard.rb.velocity.sqrMagnitude;

        //(can still go faster but won't add standard run force in maxed out direction)
        if (tempMagnitude > localBlackboard.sqrMaxSpeed)
        {
            if (Mathf.Sign(moveInput.x) == Mathf.Sign(localBlackboard.rb.velocity.x))
            {
                moveInput.x = 0f;
            }

            if (Mathf.Sign(moveInput.y) == Mathf.Sign(localBlackboard.rb.velocity.z))
            {
                moveInput.y = 0f;
            }
        }
    }


    private void MovePlayer()
    {
        ClampSpeed();
        Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y);

        if(moveInput.x != 0 || moveInput.y != 0)
        {
            localBlackboard.rb.AddForce(moveDir * localBlackboard.moveSpeed);
            RotatePlayer();
        }
    }

    private void RotatePlayer()
    {
        Vector3 rotDir = new Vector3(moveInput.x, 0, moveInput.y);
        localBlackboard.moverTransform.rotation  = Quaternion.Slerp(localBlackboard.moverTransform.rotation, Quaternion.LookRotation(rotDir), Time.deltaTime * localBlackboard.rotationSpeed);
    }
    #endregion




    private void OnDestroy()
    {
        UnSubscribe();
    }
}
