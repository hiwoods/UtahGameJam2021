///
///Shared Variable storage for the character controller
///
using UnityEngine;
using System;
using System.Collections.Generic;



public class LocalBlackboard : MonoBehaviour
{
    #region Modules
    public Reincarnation reincarnation;
    public Diable diable;
    public CharacterControllerSumo sumo;
    #endregion


    [Space(10)]
    public int controllerSet = 0;
    public Rigidbody rb;

    [HideInInspector]
    public bool movementEnabled = true;

    public int currentReincarnation = 0;

    [Serializable]
    public class CharacterInfo
    {
        public float moveSpeed = 5f;
        public float rotationSpeed = 10f;

        public float maxSpeed = 1f;
        [HideInInspector]
        public float sqrMaxSpeed = 60f;

        public float dashSpeed = 10f;
        public float dashCooldownTime = 1f;

        public int mass = 1;
        public bool useGravity = true;
        public bool lockYAxis = false;

        public GameObject charModel;
        public GameObject spawnVFX;
        public GameObject deathVFX;
        public GameObject hitWaterVFX;
    }

    public List<CharacterInfo> characterInfo = new List<CharacterInfo>();

    public Transform moverTransform;

    public float poopRechargeTime = 1f;
    public GameObject poopPrefab;
    public Vector3 poopSpawnOffset;

    private void Awake()
    {
        foreach(CharacterInfo c in characterInfo)
        {
            c.sqrMaxSpeed = c.maxSpeed * c.maxSpeed;
        }
    }




    #region Debug
    private void Update()
    {
        foreach (CharacterInfo c in characterInfo)
        {
            c.sqrMaxSpeed = c.maxSpeed * c.maxSpeed;
        }
    }
    #endregion
}