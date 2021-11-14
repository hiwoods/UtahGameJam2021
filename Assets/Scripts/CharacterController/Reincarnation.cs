///
///Handles reincarnating the players
///
using UnityEngine;



public class Reincarnation : MonoBehaviour
{
    private LocalBlackboard localBlackboard;
    private RigidbodyConstraints oldConstraints;

    public void Setup(LocalBlackboard _localBlackboard)
    {
        localBlackboard = _localBlackboard;
        oldConstraints = localBlackboard.rb.constraints;
    }



    public void Reincarnate()
    {
        ChangeCharacters();
        Respawn();
    }



    private void ChangeCharacters()
    {
        if (localBlackboard.currentReincarnation == 0)
        {
            localBlackboard.currentReincarnation++;
        }
        else if(localBlackboard.currentReincarnation == 1)
        {
            localBlackboard.currentReincarnation++;
            MainLogic.Instance.RemovePlayerFromIce();
        }
        else
        {
            localBlackboard.currentReincarnation = 0;
            MainLogic.Instance.AddPlayerOnIce();
        }



        localBlackboard.rb.useGravity = localBlackboard.characterInfo[localBlackboard.currentReincarnation].useGravity;

        if (localBlackboard.characterInfo[localBlackboard.currentReincarnation].lockYAxis)
            localBlackboard.rb.constraints = RigidbodyConstraints.FreezePositionY;
        else
            localBlackboard.rb.constraints = oldConstraints;

        localBlackboard.rb.mass = localBlackboard.characterInfo[localBlackboard.currentReincarnation].mass;
        localBlackboard.characterInfo[localBlackboard.currentReincarnation].charModel.SetActive(true);

        localBlackboard.movementEnabled = true;
    }



    private void Respawn()
    {
        localBlackboard.rb.velocity = Vector3.zero;
        localBlackboard.rb.angularVelocity = Vector3.zero;

        //set position
        Vector2 randPos = new Vector2(Random.Range(GlobalBlackboard.Instance.spawnRange.x, -GlobalBlackboard.Instance.spawnRange.x), Random.Range(GlobalBlackboard.Instance.spawnRange.y, -GlobalBlackboard.Instance.spawnRange.y));
        Vector3 birthplace;

        if (localBlackboard.currentReincarnation != 2)
            birthplace = GlobalBlackboard.Instance.standardSpawnPos + new Vector3(randPos.x, 0, randPos.y);
        else
            birthplace = GlobalBlackboard.Instance.seagullMovePlane + new Vector3(randPos.x, 0, randPos.y);

        localBlackboard.moverTransform.position = birthplace;


        //play some VFX
        localBlackboard.characterInfo[localBlackboard.currentReincarnation].spawnVFX.SetActive(true);
    }
}