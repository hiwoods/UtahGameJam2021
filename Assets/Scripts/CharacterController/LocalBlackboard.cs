///
///
///
using UnityEngine;



public class LocalBlackboard : MonoBehaviour
{
    public int controllerSet = 0;
    public Rigidbody rb;

    public float moveSpeed = 5f;
    public Transform moverTransform;
    public float rotationSpeed = 10f;

    public float maxSpeed = 1f;
    [HideInInspector]
    public float sqrMaxSpeed = 60f;

    public float dashSpeed = 10f;
    public float dashCooldownTime = 1f;

    private void Awake()
    {
        sqrMaxSpeed = maxSpeed * maxSpeed;
    }

    #region Debug
    //private void Update()
    //{
    //    sqrMaxSpeed = maxSpeed * maxSpeed;
    //}
    #endregion
}
