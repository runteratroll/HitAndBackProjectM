using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPop : MonoBehaviour
{
    bool isPop  = true;
    int count = 0;


    public void SetActiveObj(GameObject SkillTree)
    {
        count++;
        count = count % 2; //1 이면 1 2이면 0 
  
        isPop = count == 0 ? true : false;

        if(isPop == true)
        {

        }

        SkillTree.SetActive(isPop);
       
    }
}
