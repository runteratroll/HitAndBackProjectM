using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : SingleT<TestManager>
{
    //여기에 Awak를 쓰면 자식이 부모를 가려버리네 이미 같은 함수이름?이 있으니까 부모가 
    //자식한테 물려주는 거군
    
    //오버라이드 안써도 실행은 되군
    //다른 스크립트가 부모를 호출할떄 부모가 아닌 자식한테 신호를 줄려고 override를 쓰는 거겠지?
    //덮어쓴다는 말이니까
    //Event많이 아닌 Action
    protected override void Awake()
    {
        base.Awake();
        Debug.Log("s"); //그러면 Awake하면 자식한테 가는거지?
    }
}
