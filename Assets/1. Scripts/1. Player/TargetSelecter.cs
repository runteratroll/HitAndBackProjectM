using System.Collections;
using UnityEngine;

public class TargetSelecter : MonoBehaviour
{



    public Camera camera;

    public float forceSize;

    public ParticleSystem particleSystem;
    //�÷��̾����� ��߰ڴ�.

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


    //�θ�� �������� �ð��� ���������� ��ų�� ������
    public void ChargeAttack()
    {
        //��¡�ð��� ������ �̰� �������� 


        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo ))
        {
            if (hitInfo.collider.gameObject.GetComponent<Target>() != null)
            {
                Debug.Log("������");
                Vector3 distanceToTarget = hitInfo.point - transform.position;
                hitInfo.transform.gameObject.layer = LayerMask.NameToLayer("Target");
                forceDirection = distanceToTarget.normalized;

                //������ٵ� ��
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
