using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleT<T> : MonoBehaviour where T : SingleT<T> 
{

    //abstract�� ��� �θ��� �ִ� �Լ��� ����Ǵ� ����
    //�׷��ٴ� ����? �ڽ��� �θ��� ���� �����Ѵ�.
    //�̹� �θ��� ���¸� ������ �ִٴ� ����
    public static T Instance;

    protected virtual void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
            return;
        }

        Debug.Log("SingltT");

        Instance = this as T;
        DontDestroyOnLoad(this);
    }
}
