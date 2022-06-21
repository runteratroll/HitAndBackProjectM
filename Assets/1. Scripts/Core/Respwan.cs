using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respwan : MonoBehaviour
{
    public Transform respawnPosition;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("し艦たた情し");
        other.transform.position = respawnPosition.position;
    }

    

}
