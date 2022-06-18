using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBounce 
{
    void Bounce(Vector3 normal, float power = 1f);
}
