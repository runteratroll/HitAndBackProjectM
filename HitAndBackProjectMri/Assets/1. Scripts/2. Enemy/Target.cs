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
        Debug.Log("���콺 ���ٴ�");
        renderer.material.color = Color.red;
    }

    private void OnMouseExit()
    {
        renderer.material.color = Color.white; //�̰ɷ� �������� �ұ�
    }
}
