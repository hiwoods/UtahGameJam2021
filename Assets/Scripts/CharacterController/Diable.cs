///
///Kill the players
///
using System.Collections;
using UnityEngine;



public class Diable : MonoBehaviour
{
    private LocalBlackboard localBlackboard;

    public void Setup(LocalBlackboard _localBlackboard)
    {
        localBlackboard = _localBlackboard;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
            HitWater();
    }

    private void HitWater()
    {
        if (localBlackboard.movementEnabled)
        {
            localBlackboard.movementEnabled = false;
            localBlackboard.characterInfo[localBlackboard.currentReincarnation].hitWaterVFX.SetActive(true);
            //play animation

            StartCoroutine(DeathDelay());
        }
    }


    //temp solution until the shark thing is added
    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(2f);
        Die();
    }

    private void Die()
    {
        localBlackboard.characterInfo[localBlackboard.currentReincarnation].deathVFX.SetActive(true);
        localBlackboard.characterInfo[localBlackboard.currentReincarnation].charModel.SetActive(false);
        localBlackboard.reincarnation.Reincarnate();
    }
}