///
///Simple script for disabling something x amount of time after it has been enabled
///
using System.Collections;
using UnityEngine;



public class DisableAfterTime : MonoBehaviour
{
    [SerializeField] private float disableTime = 5f;



    private void Start()
    {
        StartCoroutine(WaitTillDisable());
    }

    private IEnumerator WaitTillDisable()
    {
        yield return new WaitForSeconds(disableTime);
        this.gameObject.SetActive(false);
    }
}