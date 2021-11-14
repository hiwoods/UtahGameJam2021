using System.Collections;
using UnityEngine;

public class PoopController : MonoBehaviour
{
    [SerializeField] private float poopKillTime = 5f;
    [SerializeField] private float explosionForce = 60f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float upwardsPush = 10f;
    private Coroutine killRoutine;
    private Rigidbody rb;


    private void OnTriggerEnter(Collider other)
    {
        //grab all collided stuff, add explosion force to them

        if(other.GetComponent<Rigidbody>() != null)
        {
            rb = other.GetComponent<Rigidbody>();
            rb.AddExplosionForce(explosionForce, this.transform.position, explosionRadius, upwardsPush);
        }



        if(killRoutine == null)
            killRoutine = StartCoroutine(KillPoopExplosion());
    }


    private IEnumerator KillPoopExplosion()
    {
        yield return new WaitForSeconds(poopKillTime);
    }
}
