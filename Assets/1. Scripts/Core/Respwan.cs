using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respwan : MonoBehaviour
{
    public Transform respawnPosition;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("���Ϥ�����");
        other.transform.position = respawnPosition.position;
    }

    

}
