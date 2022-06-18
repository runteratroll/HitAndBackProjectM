using System.Collections;
//using System.Numerics;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

   


    //ĳ���� �߷°�
    public float verticalSpd;
    private float gravity = 9.8f;
    [Header("�̵�����")]

    float speed;
    public float vertical;
    public float horizontal;
    public Vector3 moveAmount = Vector3.zero;
    public enum PlayerState { None, Idle, Walk, Run, Attack, Skill }
    PlayerState state = PlayerState.None;
    //ĳ���� ���� �̵� �ӵ� (�ȱ�)
    public float walkMoveSpd = 2.0f;

    //ĳ���� ���� �̵� �ӵ� (�޸���)
    public float runMoveSpd = 3.5f;

    //CharacterController ĳ�� �غ�
    private CharacterController controllerCharacter = null;

    //ĳ���� CollisionFlags �ʱⰪ ����

    private CollisionFlags collisionFlags = CollisionFlags.None; //���߿� ���ִ°� 





    [Header("��������")]
    //������ ���� Ŀ����
    public TrailRenderer AtkTraillRenderer = null;


    public PlayerAnimation playerAnimation;




    private void Awake()
    {
        controllerCharacter = GetComponent<CharacterController>();
        playerAnimation = GetComponent<PlayerAnimation>();


    }




    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;
    private void Update()
    {

        setGravity();
        SetMove();

        PlayerSetEnum();
   

    }


    /// <summary>
    /// �̵��Լ� �Դϴ� ĳ����
    /// </summary>
    private bool stopMove = false;

    public void SetStopMove(bool dar)
    {
        Debug.Log("SendMessage����");
        stopMove = dar;
    }
    Vector3 _vecTemp;

    public void PlayerSetEnum()
    {
        switch (state)
        {
            case PlayerState.None:
                break;
            case PlayerState.Idle:
                break;
            case PlayerState.Walk:
                playerAnimation.SetRun(false);
                break;
            case PlayerState.Run:
               
                    playerAnimation.SetRun(true);
                
                break;
            case PlayerState.Attack:
                break;
            case PlayerState.Skill:
                break;
            default:
                break;
        }
    }

 

    private void SetMove()
    {
        if (stopMove == false)
        { 
            Debug.Log("����");
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y; //z���� �Ǵ°� �ƴϾ�?
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                // ������ �̵� ��
                speed = walkMoveSpd;

                if (Input.GetKey(KeyCode.Space))
                {
                    speed = runMoveSpd;
                    state = PlayerState.Run;
                }
                else
                {
                    state = PlayerState.Walk;
                }

                _vecTemp = new Vector3(0f, verticalSpd, 0f);



                moveAmount = moveDir.normalized * speed * Time.deltaTime + _vecTemp;

                collisionFlags = controllerCharacter.Move(moveAmount);// controllerCharacter.Move(moveAmount * Time.deltaTime); //�� �����̵� �Ѱɱ� //SimpleMove��
            }
            
        }
        else
        {
            _vecTemp = new Vector3(0f, verticalSpd, 0f);
            moveAmount += _vecTemp;
            collisionFlags = controllerCharacter.Move(moveAmount * Time.deltaTime);// controllerCharacter.Move(moveAmount * Time.deltaTime); //�� �����̵� �Ѱɱ� //SimpleMove��
        }
        
    }

    public void SetHit(Vector3 normal, float power, float delay)
    {
        stopMove = true;
        moveAmount = -normal * power + new Vector3(0, 2f);

        StartCoroutine(RecoverProcess(delay));
    }

    IEnumerator RecoverProcess(float delay)
    {

        yield return new WaitForSeconds(delay);
        stopMove = false;
    }










    /// <summary>
    ///  ĳ���� �߷� ����
    /// </summary>
    void setGravity()
    {
        //��ä�� 0���� 0.1���̸� �Դٰ��� �ϴ°ɱ�
        if (collisionFlags == CollisionFlags.Below)
        {
            Debug.Log("���� ����ϴ�?");
            verticalSpd = 0f;
        }
        else if ((collisionFlags == CollisionFlags.None))
        {
            Debug.Log("�߷� �� ����");

            verticalSpd -= gravity * Time.deltaTime;//���⿡�� ���� ������ 

        }


    }




}
