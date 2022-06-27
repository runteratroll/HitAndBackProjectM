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
    //public int dmg; //��Ʈ������ ��ȯ�ؾ߰ڴ�.
    public TextMeshProUGUI dmgText;

    public bool isSmash;
    private void OnEnable()
    {
       
        comboStep = combo.comboStep; //Ȱ��ȭ �ɋ�����
        //damageInt = int.Parse(damgeType);

    }

    private void Start()
    {
        
    }

    //���Լ��� ������ ���Լ� �����ϱ�


    void enmeyCriticalZone()
    {
        //���Ϳ� ũ��Ƽ���� �޾ƾ��ϴµ� 

    }
    
    //�����ϸ� ����ĳ��Ʈ�� �ұ�
    private void OnTriggerEnter(Collider other) 
    {


        HitEnemy(other);
    }


    void HitEnemy(Collider other)
    {
         if(other.CompareTag("HitBox_Enemy"))
        {

            //�������� �ؽ�Ʈ�� �����ǰ�
            //Debug.Log("�߳�?");
            //�Լ��ִϸ��̼ǿ��� �޺����� ��Ʈ�� �ذ����� ���⼭ ��Ʈ�� �޾ư����� ����ٰ� �ֱ�
            damgeType = string.Format("{0} + {1}", type_Atk, comboStep); // damageInt 
            dmgText.text = damgeType;
            dmgText.gameObject.SetActive(true);
            //�ڷ�ƾ���� �޾Ʒθ� �������ǰ� ��������
            IDamageble hp = other.GetComponent<IDamageble>();
            if(hp != null)
            {
                Debug.Log("����");
                hp.HealthDown(damageInt, other.transform.position, Vector3.up, 0);
            }
        }
    }

}
