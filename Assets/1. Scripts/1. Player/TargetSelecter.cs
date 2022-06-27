using System.Collections;
using UnityEngine;

public class TargetSelecter : MonoBehaviour
{



    public Camera camera;

    public float forceSize;

    public ParticleSystem particleSystem;
    //플레이어한테 줘야겠다.

    private CharacterController character;
    public Player player;

    private void Start()
    {
        character = GetComponent<CharacterController>();
    }

    bool isTrue;
    Vector3 forceDirection;
    private void Update()
    {

        if (player.CanUseChargedAttack() == true)
        {

            if (isTrue)
            {
                character.SimpleMove(forceDirection * forceSize);
            }
        }





    }


    //부모로 만들어가지고 시간이 지날떄까지 스킬을 못쓰는
    public void ChargeAttack()
    {
        //차징시간이 끝난후 이걸 눌렀을떄 


        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo ))
        {
            if (hitInfo.collider.gameObject.GetComponent<Target>() != null)
            {
                Debug.Log("감지됨");
                Vector3 distanceToTarget = hitInfo.point - transform.position;
                hitInfo.transform.gameObject.layer = LayerMask.NameToLayer("Target");
                forceDirection = distanceToTarget.normalized;

                //리지드바디를 음
                //rigidbody.AddForce(forceDirection * forceSize, ForceMode.Impulse);
                //character.SimpleMove(forceDirection * forceSize);
                particleSystem.Play();
                StartCoroutine(Cast(hitInfo));
            }
        }

    }



    public float dashDuration;
    public IEnumerator Cast(RaycastHit hit)
    {

        gameObject.layer = LayerMask.NameToLayer("PlayerSmash");
        //cC.SimpleMove(Camera.main.transform.forward * dashForce);
        isTrue = true;
        gameObject.SendMessage("SetStopMove", isTrue);
        //Camera.main.transform.forward * dashForce, ForceMode.VelocityChange);'


        yield return new WaitForSeconds(dashDuration);
        hit.transform.gameObject.layer = LayerMask.NameToLayer("Enemy");
        gameObject.layer = LayerMask.NameToLayer("Player");
        isTrue = false;
        gameObject.SendMessage("SetStopMove", isTrue);
        //cC.SimpleMove(transform.position);
    }
}
