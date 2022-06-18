using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelecter : MonoBehaviour
{



    public Camera camera;

    public float forceSize;

    //�÷��̾����� ��߰ڴ�.

    private CharacterController character;


    private void Start()
    {
        character = GetComponent<CharacterController>();
    }

    bool isTrue;
    Vector3 forceDirection;
    private void Update()
    {


        


        if(isTrue)
        {
            character.SimpleMove(forceDirection * forceSize);
        }


    }

    public void ChargeAttack()
    {
        //��¡�ð��� ������ �̰� �������� 
        
      
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.gameObject.GetComponent<Target>() != null)
                {
                    Debug.Log("������");
                    Vector3 distanceToTarget = hitInfo.point - transform.position;
                    forceDirection = distanceToTarget.normalized;

                    //������ٵ� ��
                    //rigidbody.AddForce(forceDirection * forceSize, ForceMode.Impulse);
                    //character.SimpleMove(forceDirection * forceSize);

                    StartCoroutine(Cast());
                }
            }
        
    }

    public float dashDuration;
    public  IEnumerator Cast()
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
