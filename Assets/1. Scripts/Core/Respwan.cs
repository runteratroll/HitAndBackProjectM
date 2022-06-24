using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Respwan : MonoBehaviour
{
    public Transform respawnPosition;
 

    private void OnTriggerStay(Collider other)
    {

        GameManager.PlayerMove.SetStopMove(true);
        Debug.Log("し艦たた情し");
        GameManager.Player.transform.position = respawnPosition.position;
        //SceneManager.LoadScene("SampleScene");
    }


    private void OnTriggerExit(Collider other)
    {
        GameManager.PlayerMove.SetStopMove(false);
    }
}
