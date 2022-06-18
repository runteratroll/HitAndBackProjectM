using System.Collections;
//using System.Numerics;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

   


    //캐릭터 중력값
    public float verticalSpd;
    private float gravity = 9.8f;
    [Header("이동관련")]

    float speed;
    public float vertical;
    public float horizontal;
    public Vector3 moveAmount = Vector3.zero;
    public enum PlayerState { None, Idle, Walk, Run, Attack, Skill }
    PlayerState state = PlayerState.None;
    //캐릭터 직선 이동 속도 (걷기)
    public float walkMoveSpd = 2.0f;

    //캐릭터 직선 이동 속도 (달리기)
    public float runMoveSpd = 3.5f;

    //CharacterController 캐싱 준비
    private CharacterController controllerCharacter = null;

    //캐릭터 CollisionFlags 초기값 설정

    private CollisionFlags collisionFlags = CollisionFlags.None; //공중에 떠있는거 





    [Header("전투관련")]
    //공격할 때만 커지게
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
    /// 이동함수 입니다 캐릭터
    /// </summary>
    private bool stopMove = false;

    public void SetStopMove(bool dar)
    {
        Debug.Log("SendMessage받음");
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
            Debug.Log("흐흐");
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y; //z여야 되는거 아니야?
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                // 프레임 이동 양
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

                collisionFlags = controllerCharacter.Move(moveAmount);// controllerCharacter.Move(moveAmount * Time.deltaTime); //왜 순간이동 한걸까 //SimpleMove맨
            }
            
        }
        else
        {
            _vecTemp = new Vector3(0f, verticalSpd, 0f);
            moveAmount += _vecTemp;
            collisionFlags = controllerCharacter.Move(moveAmount * Time.deltaTime);// controllerCharacter.Move(moveAmount * Time.deltaTime); //왜 순간이동 한걸까 //SimpleMove맨
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
    ///  캐릭터 중력 설정
    /// </summary>
    void setGravity()
    {
        //대채왜 0에서 0.1사이를 왔다갔다 하는걸까
        if (collisionFlags == CollisionFlags.Below)
        {
            Debug.Log("여기 통과하니?");
            verticalSpd = 0f;
        }
        else if ((collisionFlags == CollisionFlags.None))
        {
            Debug.Log("중력 값 적용");

            verticalSpd -= gravity * Time.deltaTime;//여기에서 이제 뭐뭐를 

        }


    }




}
