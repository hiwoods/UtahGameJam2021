/// <summary>
/// Globally accessible variables
/// </summary>
/// 
using UnityEngine;



public class GlobalBlackboard : GenericSingletonClass<GlobalBlackboard>
{
    public int playersOnIce = 3;

    public Vector3 standardSpawnPos;
    public Vector3 seagullMovePlane;
    public Vector2 spawnRange;
}