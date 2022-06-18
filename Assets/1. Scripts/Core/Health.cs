using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour, IDamageble 
{

    //����� �ｺ�� ���õǰԸ� ����� 
    public int currentHp;
    public int maxHp;


    private void Start()
    {
        currentHp = maxHp;
    }
    public virtual void HealthDown(int damage, Vector2 hitPoint, Vector2 normal, float power) //�� protected�� �ȵɱ�
    {
        currentHp -= damage; //�ǰ� ���̰� ƨ��°͵� ������ �ٸ��Ͱ����� �ؾߵǳ�

        //�ǰ� ��ƼŬ�� ���⼭ ���
        if (currentHp <= 0)
        {
            OnDie();
        }
    }

    //�Լ��� ���¸� ����? 
    //�Ǵٴ°� �����ϱ� ���θ�? ����� ��������Ʈ�� ���� ���ϱ�

    //������ ����Ʈ�� �ȳ����� ������Ʈ�� ������?
    //������ �ٸ� �Լ��� ���°ͺ���? ���� ���� �Լ��� ����ϴµ� ���θ� �ٸ�����
    protected abstract void OnDie();
    //�ϴ��ϰ��� �ٲ���

    //������ �ٿ�� �ٸ��ϱ� �������̽��� ���������� ���� ������
    //�ٿ ���ϴ� ���� �����ϱ� 

   // protected abstract void Bounce(Vector3 normal, float power = 1);
 
}
