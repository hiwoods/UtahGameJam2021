using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopExplosion : MonoBehaviour
{
    [SerializeField] private float poopKillTime = 5f;
    [SerializeField] private float explosionForce = 60f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float upwardsPush = 10f;
    [SerializeField] private float disableTime = 1f;
    private Coroutine killRoutine;
    private Coroutine disableRoutine;
    private Rigidbody rb;


    private void OnTriggerEnter(Collider other)
    {
        //grab all collided stuff, add explosion force to them

        if (other.GetComponent<Rigidbody>() != null)
        {
            rb = other.GetComponent<Rigidbody>();
            rb.AddExplosionForce(explosionForce, this.transform.position, explosionRadius, upwardsPush);
        }



        if (killRoutine == null)
            killRoutine = StartCoroutine(KillPoopExplosion(poopKillTime));
        if (disableRoutine == null)
            disableRoutine = StartCoroutine(KillPoopExplosion(disableTime));
    }


    private IEnumerator KillPoopExplosion(float timewait)
    {
        yield return new WaitForSeconds(timewait);
    }
}
