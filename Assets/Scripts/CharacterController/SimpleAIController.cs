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

        private void Start()
        {
            CharacterControllerSumo = GetComponent<CharacterControllerSumo>();
            LocalBlackboard = GetComponent<LocalBlackboard>();
            Debug.Log(gameObject.tag + " is now an AI");
        }

        private void Update()
        {
            if (LocalBlackboard.currentReincarnation == 0) //walrus
            {
                return;
            }
            else if (LocalBlackboard.currentReincarnation == 1)
            {

            }
            else if (LocalBlackboard.currentReincarnation == 2)
            {

            }

        }
    }
}
