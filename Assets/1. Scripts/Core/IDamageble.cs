using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDamageble //�������� �̴ϱ� �׳� �����¿� ���� �Լ��� �ٸ��� �ϸ����������

{
    void HealthDown(int damage, Vector2 hitPoint, Vector2 normal, float power = 1f); //������ �׳� ������ �¾��� ���̴ϱ�
    //void MadHealthDown //�ٵ� �Ǵٴ°Ŵ� ���� �ʳ�?

}
