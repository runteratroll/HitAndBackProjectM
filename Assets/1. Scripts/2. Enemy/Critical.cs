using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Critical : MonoBehaviour
{

    public LayerMask PlayerCollider;
    bool isCritical = false;
    private void OnTriggerEnter(Collider other)
    {
        if((1 << other.gameObject.layer & PlayerCollider) > 0)
        {
            
            isCritical = true;
        }
    }


}
