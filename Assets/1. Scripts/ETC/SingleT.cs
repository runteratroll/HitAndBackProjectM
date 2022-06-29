using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleT<T> : MonoBehaviour where T : SingleT<T> 
{

    //abstract가 없어도 부모의 있는 함수가 실행되는 구나
    //그렇다는 말은? 자식은 부모의 모든걸 수행한다.
    //이미 부모의 상태를 가지고 있다는 거지
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
