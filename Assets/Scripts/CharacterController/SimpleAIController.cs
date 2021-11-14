using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CharacterController
{
    public class SimpleAIController : MonoBehaviour
    {
        public CharacterControllerSumo CharacterControllerSumo;
        public LocalBlackboard LocalBlackboard;
        public Diable Diable;

        public List<GameObject> otherPlayers;

        private void Start()
        {
            CharacterControllerSumo = GetComponent<CharacterControllerSumo>();
            LocalBlackboard = GetComponent<LocalBlackboard>();
            Diable = GetComponent<Diable>();

            Debug.Log(gameObject.tag + " is now an AI");

            otherPlayers = new List<GameObject>();

            for (int i = 1; i <=4; i++)
            {
                string tag = $"Player{i}";

                if (string.Equals(tag, gameObject.tag))
                    return;

                otherPlayers.Add(GameObject.FindGameObjectWithTag(tag));
            }
        }

        private void Update()
        {
            if (Diable.isDead)
                return;

            var otherPlayer = otherPlayers.Where(x => !GetDiable(x).isDead).Where(x =>
            {
                var loca = GetLocalBlackboard(x);

                return loca.currentReincarnation == 0 || loca.currentReincarnation == 1;
            }).OrderBy(x => Vector3.Distance(x.transform.position, gameObject.transform.position))
                .FirstOrDefault();

            if (otherPlayer == null)
                return;

            var dir = (otherPlayer.transform.position - transform.position).normalized;

            CharacterControllerSumo.MovePlayer(dir);

            if (LocalBlackboard.currentReincarnation == 0) //walrus
            {
                CharacterControllerSumo.Dash();
            }
            else if (LocalBlackboard.currentReincarnation == 1)
            {
                CharacterControllerSumo.Dash();
            }
            else if (LocalBlackboard.currentReincarnation == 2)
            {
                CharacterControllerSumo.Poop();
            }

        }

        public static LocalBlackboard GetLocalBlackboard(GameObject go)
        {
            return go.GetComponent<LocalBlackboard>();
        }

        public static Diable GetDiable(GameObject go)
        {
            return go.GetComponent<Diable>();
        }
    }
}
