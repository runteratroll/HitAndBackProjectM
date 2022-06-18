using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public SkinnedMeshRenderer renderer;

    private void Start()
    {

    }



    private void OnMouseEnter()
    {
        Debug.Log("마우스 갖다댐");
        renderer.material.color = Color.red;
    }

    private void OnMouseExit()
    {
        renderer.material.color = Color.white; //이걸로 약점간파 할까
    }
}
