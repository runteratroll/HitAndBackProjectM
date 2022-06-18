using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDamageble //데미지블 이니까 그냥 적상태에 따라 함수만 다르게 하면되지않을까

{
    void HealthDown(int damage, Vector2 hitPoint, Vector2 normal, float power = 1f); //역할은 그냥 적한테 맞았을 뿐이니까
    //void MadHealthDown //근데 피다는거는 같지 않냐?

}
