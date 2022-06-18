using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour, IDamageble 
{

    //여기는 헬스만 관련되게만 만들까 
    public int currentHp;
    public int maxHp;


    private void Start()
    {
        currentHp = maxHp;
    }
    public virtual void HealthDown(int damage, Vector2 hitPoint, Vector2 normal, float power) //왜 protected는 안될까
    {
        currentHp -= damage; //피가 깍이고 튕기는것도 적마다 다를것같은데 해야되나

        //피격 파티클은 여기서 재생
        if (currentHp <= 0)
        {
            OnDie();
        }
    }

    //함수로 상태를 나눠? 
    //피다는건 같은니까 내부만? 여기는 모든오브젝트의 같은 경우니까

    //맞으면 이펙트가 안나오는 오브젝트가 있을까?
    //적마다 다른 함수를 쓰는것보다? 적은 같은 함수를 사용하는데 내부만 다른거지
    protected abstract void OnDie();
    //일단하고나서 바꾸자

    //적마다 바운스가 다르니까 인터페이스로 만들이유는 없지 않을까
    //바운스 안하는 적도 있으니까 

   // protected abstract void Bounce(Vector3 normal, float power = 1);
 
}
