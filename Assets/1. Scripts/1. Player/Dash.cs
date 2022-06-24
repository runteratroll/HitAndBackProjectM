using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CreatringCharacters.Abilities
{
    public class Dash : Ability
    {
        [SerializeField] private float dashForce;
        [SerializeField] private float dashDuration;

        private CharacterController cC;
        public Player player;

        private void Awake()
        {
            cC = GetComponent<CharacterController>();
        }

        bool isTrue = false;

        private void Update()
        {
            if(player.CanUseDash() == true)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    StartCoroutine(Cast());
                }

                if (isTrue == true)
                {
                    cC.SimpleMove(transform.forward * dashForce);
                }
            }
            
         
        }
        //대쉬중에는 이동못하게 만들어야지 
        //불변수를 모두에게 주는 방법은?
        //여기에다 SendMessage써야지 
        //모든행동을 못하게하는 함수를 만들고 메시지를 전달하면 되잖아?
        public override IEnumerator Cast()
        {

            //cC.SimpleMove(Camera.main.transform.forward * dashForce);
            isTrue = true;
            gameObject.SendMessage("SetStopMove", isTrue);
            //Camera.main.transform.forward * dashForce, ForceMode.VelocityChange);'


            yield return new WaitForSeconds(dashDuration);

            isTrue = false;
            gameObject.SendMessage("SetStopMove", isTrue);
            //cC.SimpleMove(transform.position);
        }
    }
}

