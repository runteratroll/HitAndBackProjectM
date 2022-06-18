using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{

    [Header("카메라")]
    private Transform cameraTransform = null; //질문 던지고 모르는거 있으면 찾든지 질문하던지

    //target
    public GameObject objTarget = null;

    //player transform 캐싱
    private Transform objTargetTransfrom = null;

    //카메라가 3가지야
    public enum CameraTypeState { First, Second, Third}

    //카메라 기본 3인칭
    public CameraTypeState cameraState = CameraTypeState.Third;

    [Header("3인칭 카메라")]
    //떨어진 거리
    public float distance = 6.0f;

    //추가 높이
    public float height = 1.75f;

    //smooth time
    public float heightDamp = 2f;
    public float rotationDamping = 3f;

    [Header("2인칭카메라")]
    public float rotationSpd = 10f;


    [Header("1인칭카메라")]
    //마우스 카메라 조절 티테일 좌표
    public float detailX = 5f;
    public float detailY = 5f;

    //마우스 회전 값

    private float rotationX = 0f;
    private float rotationY = 0f;

    //캐싱
    public Transform posFirstTarget = null;


    /// <summary>
    /// 1인칭 시점 카메라 조작
    /// </summary>
    void FirstCamera() {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotationX = cameraTransform.localEulerAngles.y + mouseX * detailX;
        rotationX = (rotationX > 180.0f) ? rotationX - 360.0f : rotationX;
        rotationY = rotationY * mouseX * detailX; //얘는 오일러가 필요없겠어 왜지?
        rotationY = (rotationY > 180.0f) ? rotationY - 360.0f : rotationY;

        cameraTransform.localEulerAngles = new Vector3(-rotationY, rotationX, 0f); //x위치에 y를 넣은 이유는? rotationX를 넣은 이유는?
        cameraTransform.position = posFirstTarget.position;
    }


    /// <summary>
    /// 2인칭 카메라 조작
    /// </summary>
    void SecondCamera() {
        cameraTransform.RotateAround(objTargetTransfrom.position, Vector3.up, rotationSpd * Time.deltaTime); //

        cameraTransform.LookAt(objTargetTransfrom);
    }

    private void Start() {
        cameraTransform = GetComponent<Transform>();

        if(objTarget != null) {
            objTargetTransfrom = objTarget.transform;
        }
    }
    private void LateUpdate() { //이동하고 난후해야되서 그런가 보다
        if(objTarget == null) {
            return;
        }

        if(objTargetTransfrom == null) {
            objTargetTransfrom = objTarget.transform;
        }


        switch (cameraState) {
            case CameraTypeState.Third:
            ThirdCamera();
            break;
            case CameraTypeState.Second:
            SecondCamera();
            break;
            case CameraTypeState.First:
            FirstCamera();
            break;
            default:
            break;
        }
    }

    void ThirdCamera() {
        //현재 타겟 y축 각도 값
        float objTargetRotationAngle = objTargetTransfrom.eulerAngles.y; //계산할떄는 문제는 안ㅅ생김

        //현재 타겟 높이 + 카메라가 위치한 높이 추가 높이
        float objHeight = objTargetTransfrom.position.y + height;


        //현재 각도 높이
        float nowRotationAnlge = cameraTransform.eulerAngles.y;
        float nowHeight = cameraTransform.position.y;

        //현재 각도에 대한 Damp
        nowRotationAnlge = Mathf.LerpAngle(nowRotationAnlge, objTargetRotationAngle, rotationDamping * Time.deltaTime); // hd

        //현재 높이에 대한 Damp
        nowHeight = Mathf.Lerp(nowHeight, objHeight, heightDamp * Time.deltaTime);

        //유니티 각도로 변경
        Quaternion nowRotation = Quaternion.Euler(0f, nowRotationAnlge, 0f);
        //암기가 아니라 이해, 3시간, 

        cameraTransform.position = objTargetTransfrom.position;
        cameraTransform.position -= nowRotation * Vector3.forward * distance;


        //최종이동
        cameraTransform.position = new Vector3(cameraTransform.position.x, nowHeight, cameraTransform.position.z);

        cameraTransform.LookAt(objTargetTransfrom);
    }
}
