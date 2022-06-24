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

    private void OnEnable()
    {
        comboStep = combo.comboStep; //활성화 될떄마다
        //damageInt = int.Parse(damgeType);

    }

    private void Start()
    {
        //
    }

    
    //공격하면 레이캐스트로 할까
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("HitBox_Enemy"))
        {
            //Debug.Log("뜨냐?");
            damgeType = string.Format("{0} + {1}", type_Atk, comboStep);
            dmgText.text = damgeType;
            dmgText.gameObject.SetActive(true);

            IDamageble hp = other.GetComponent<IDamageble>();
            if(hp != null)
            {
                Debug.Log("맞음");
                hp.HealthDown(damageInt, other.transform.position, Vector3.up, 0);
            }
        }
    }

}
