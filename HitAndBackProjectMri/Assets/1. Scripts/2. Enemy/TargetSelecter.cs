using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelecter : MonoBehaviour
{



    public Camera camera;

    public float forceSize;

    //플레이어한테 줘야겠다.

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
        //차징시간이 끝난후 이걸 눌렀을떄 
        
      
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.gameObject.GetComponent<Target>() != null)
                {
                    Debug.Log("감지됨");
                    Vector3 distanceToTarget = hitInfo.point - transform.position;
                    forceDirection = distanceToTarget.normalized;

                    //리지드바디를 음
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
