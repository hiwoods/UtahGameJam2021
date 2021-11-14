using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reincarnation : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject NewLifeEffect;
    [SerializeField] private Vector2 Centerpoint;
    [SerializeField] private Vector2 Birthplace;
    [SerializeField] private Vector2 Rangex;
    [SerializeField] private Vector2 Rangey;

    public void ChangeCharacters()
    {
        // Swap char Controller Model
        Player.SetActive(true);
        // Enable char controller
    }

    public void Area()
    {
        Birthplace = new Vector2(Centerpoint.x + Random.Range(Rangex.x, -Rangex.x), Centerpoint.y + Random.Range(Rangey.y, -Rangey.y));
        NewLifeEffect.SetActive(true);
    }
}
