using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : SingleT<TestManager>
{
    //���⿡ Awak�� ���� �ڽ��� �θ� ���������� �̹� ���� �Լ��̸�?�� �����ϱ� �θ� 
    //�ڽ����� �����ִ� �ű�
    
    //�������̵� �Ƚᵵ ������ �Ǳ�
    //�ٸ� ��ũ��Ʈ�� �θ� ȣ���ҋ� �θ� �ƴ� �ڽ����� ��ȣ�� �ٷ��� override�� ���� �Ű���?
    //����ٴ� ���̴ϱ�
    //Event���� �ƴ� Action
    protected override void Awake()
    {
        base.Awake();
        Debug.Log("s"); //�׷��� Awake�ϸ� �ڽ����� ���°���?
    }
}
