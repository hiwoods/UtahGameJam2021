using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            KillThePlayer(other.gameObject);
    }

    private void KillThePlayer(GameObject die)
    {
        Destroy(die);
    }
}
