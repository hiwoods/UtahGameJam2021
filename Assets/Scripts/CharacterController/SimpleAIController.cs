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

        private void Start()
        {
            CharacterControllerSumo = GetComponent<CharacterControllerSumo>();

            Debug.Log(gameObject.tag + " is now an AI");
        }

        private void Update()
        {
        }
    }
}
