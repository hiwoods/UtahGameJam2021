///
///This script manages general user input
///Eventually the control sets will be even more modular/controlled through the inspector
///It might make sense to make this an instanced script with each character controller later on
///
using System;
using System.Collections.Generic;
using UnityEngine;



public class InputManager : GenericSingletonClass<InputManager>
{
    #region Variables
    [Serializable]
    public class ControlSet
    {
        public string horizontalMove = "Horizontal";
        public string verticalMove = "Vertical";
        public string dashButton = "Dash";
        public bool dashIsHold = false;
    }

    [SerializeField] private List<ControlSet> controlSets = new List<ControlSet>();


    private float tempFloat;


    #region Delegates
    public delegate void UseHorizontalInput(float inputValue);
    public List<UseHorizontalInput> _useHorizontalInput;

    public delegate void UseVerticalInput(float inputValue);
    public List<UseVerticalInput> _useVerticalInput;

    public delegate void UseDashButton();
    public List<UseDashButton> _useDashButton;
    #endregion
    #endregion

    public override void Awake()
    {
        base.Awake();
        _useHorizontalInput = new List<UseHorizontalInput>();
        _useVerticalInput = new List<UseVerticalInput>(controlSets.Count + 1);
        _useDashButton = new List<UseDashButton>(controlSets.Count + 1);

        for (int i = 0; i < controlSets.Count; i++)
        {
            _useHorizontalInput.Add(delegate { });
            _useVerticalInput.Add(delegate { });

            _useDashButton.Add(delegate { });
        }
    }

    private void Update()
    {
        for(int i = 0; i < controlSets.Count; i++)
        {
            #region Axis Input Gather
            _useHorizontalInput[i](GatherAxisInput(controlSets[i].horizontalMove));
            _useVerticalInput[i](GatherAxisInput(controlSets[i].verticalMove));
            #endregion


            #region Button Input Gather
            if (controlSets[i].dashIsHold)
            {
                if(GatherButtonHeldInput(controlSets[i].dashButton))
                    _useDashButton[i]();
            }
            else
            {
                if (GatherButtonDownInput(controlSets[i].dashButton))
                    _useDashButton[i]();
            }
            #endregion
        }
    }


    #region Input Gather Functions
    private float GatherAxisInput(string axisName)
    {
        tempFloat = Input.GetAxis(axisName);
        return tempFloat;
    }

    private bool GatherButtonDownInput(string buttonName)
    {
        if (Input.GetButtonDown(buttonName))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool GatherButtonHeldInput(string buttonName)
    {
        if (Input.GetButton(buttonName))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion


    #region DebugMovement
    //if (Input.GetButtonDown(dashButton))
    //charCon.Dash();

    //Latch
    //if (Input.GetButton(latchButton))
    //    charCon.LatchHoldCheck();
    //if (Input.GetButtonUp(latchButton))
    //    charCon.ReleaseLatch();

    //if (Input.GetButtonDown(jumpButton))
    //charCon.Jump();


    //public void UpdateDebugMovement()
    //{
    //    if (Input.GetButtonDown("Fire3"))
    //    {
    //        clampedIn = true;
    //        walkSpeed = clampedWalkSpeed;
    //    }

    //    if (Input.GetButtonUp("Fire3"))
    //    {
    //        clampedIn = false;
    //        walkSpeed = stdWalkSpeed;
    //    }
    //}
    #endregion
}