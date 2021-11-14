using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diable : MonoBehaviour
{
    [SerializeField] private GameObject HitWaterEffects;
    [SerializeField] private GameObject DieEffects;
    [SerializeField] private GameObject ChooseModels;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        HitWater();
    }

    private void HitWater()
    {
        //disable char controller
        //call reincarnation
        //play animation
        HitWaterEffects.SetActive(true);
    }

    private void Die()
    {
        //disable model
        DieEffects.SetActive(true);
        ChooseModels.SetActive(false);
    }
}