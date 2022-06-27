using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Col_PlayerAtk : MonoBehaviour
{
    public ComboAttack combo;
    public string type_Atk;

    
    int comboStep;
    public string damgeType;
    public int damageInt; 
    //public int dmg; //스트링으로 변환해야겠다.
    public TextMeshProUGUI dmgText;

    public bool isSmash;
    private void OnEnable()
    {
       
        comboStep = combo.comboStep; //활성화 될떄마다
        //damageInt = int.Parse(damgeType);

    }

    private void Start()
    {
        
    }

    //이함수가 끝나면 이함수 실행하기


    void enmeyCriticalZone()
    {
        //몬스터에 크리티컬을 받아야하는데 

    }
    
    //공격하면 레이캐스트로 할까
    private void OnTriggerEnter(Collider other) 
    {


        HitEnemy(other);
    }


    void HitEnemy(Collider other)
    {
         if(other.CompareTag("HitBox_Enemy"))
        {

            //데미지랑 텍스트랑 연동되게
            //Debug.Log("뜨냐?");
            //함수애니메이션에서 콤보마다 인트를 해가지고 여기서 인트를 받아가지고 여기다가 넣기
            damgeType = string.Format("{0} + {1}", type_Atk, comboStep); // damageInt 
            dmgText.text = damgeType;
            dmgText.gameObject.SetActive(true);
            //코루틴으로 받아로면 여길실행되게 만들어야지
            IDamageble hp = other.GetComponent<IDamageble>();
            if(hp != null)
            {
                Debug.Log("맞음");
                hp.HealthDown(damageInt, other.transform.position, Vector3.up, 0);
            }
        }
    }

}
