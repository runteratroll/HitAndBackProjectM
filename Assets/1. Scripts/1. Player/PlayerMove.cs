using System.Collections;
//using System.Numerics;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{


    [Header("�̵�����")]
    public float buffSpeed = 1;


    //ĳ���� �߷°�
    public float verticalSpd;
    public float gravity = 9.8f;
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


    public LayerMask isGround;

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


        SetMove();
        setGravity();
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
                playerAnimation.SetWalkRun(false, false);
                break;
            case PlayerState.Walk:
                playerAnimation.SetWalkRun(false , true);
                break;
            case PlayerState.Run:
                Debug.Log("��");
                    playerAnimation.SetWalkRun(true , false);
                
                break;
            case PlayerState.Attack:
                break;
            case PlayerState.Skill:
                break;
            default:
                break;
        }
    }

    public void SetMovementSpeed(float setSpeed)
    {
        buffSpeed *= setSpeed;
    }

  
    
    private void SetMove()
    {
        if (stopMove == false)
        { 
            Debug.Log("����");
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            if (horizontal == 0 && vertical == 0)
            {
                state = PlayerState.Idle;
            }
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
                   // SoundManager.instance.PlaySE("PlayerGrassRun");

                }
                else
                {
                    state = PlayerState.Walk;
                   // SoundManager.instance.PlaySE("PlayerGrassWalk");
                }

              

                _vecTemp = new Vector3(0f, verticalSpd * Time.deltaTime, 0f);


                moveAmount.y = 0;
                moveAmount = (moveDir.normalized * speed * buffSpeed * Time.deltaTime) + _vecTemp;
                
                 
                collisionFlags = controllerCharacter.Move(moveAmount);// controllerCharacter.Move(moveAmount * Time.deltaTime); //�� �����̵� �Ѱɱ� //SimpleMove��
            } else
            {
       
            }
            
        }
        else
        {
            horizontal = 0;
            vertical = 0;
            //����
            _vecTemp = new Vector3(0f, verticalSpd, 0f);
            moveAmount.y = 0;
            moveAmount += _vecTemp;
            collisionFlags = controllerCharacter.Move(moveAmount * Time.deltaTime);// controllerCharacter.Move(moveAmount * Time.deltaTime); //�� �����̵� �Ѱɱ� //SimpleMove��
        }
        horizontal = 0;
        vertical = 0;
      


        //if(horizontal != 0 && vertical != 0)
        //{
        //    //SoundManagerM.PlaySound(SoundManagerM.Sound.PlayerMove);
        //}

    }

    public void SetHit(Vector3 normal, float power, float delay)
    {
        playerAnimation.SetDamage(true);
        stopMove = true;
        moveAmount = -normal * power + new Vector3(0, 2f);

        StartCoroutine(RecoverProcess(delay));

    }

    IEnumerator RecoverProcess(float delay)
    {

        yield return new WaitForSeconds(delay);
        playerAnimation.SetDamage(false);
        stopMove = false;
    }










    /// <summary>
    ///  ĳ���� �߷� ����
    /// </summary>
    /// 
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == isGround)
        {
            verticalSpd = 0f;
        }
    }
    void setGravity()
    {
        
        if ((collisionFlags == CollisionFlags.None))
        {
          

            verticalSpd -= gravity * Time.deltaTime;//���⿡�� ���� ������ 

        }


    }




}
