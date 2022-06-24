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
        //�뽬�߿��� �̵����ϰ� �������� 
        //�Һ����� ��ο��� �ִ� �����?
        //���⿡�� SendMessage����� 
        //����ൿ�� ���ϰ��ϴ� �Լ��� ����� �޽����� �����ϸ� ���ݾ�?
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

