using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{

    [Header("ī�޶�")]
    private Transform cameraTransform = null; //���� ������ �𸣴°� ������ ã���� �����ϴ���

    //target
    public GameObject objTarget = null;

    //player transform ĳ��
    private Transform objTargetTransfrom = null;

    //ī�޶� 3������
    public enum CameraTypeState { First, Second, Third}

    //ī�޶� �⺻ 3��Ī
    public CameraTypeState cameraState = CameraTypeState.Third;

    [Header("3��Ī ī�޶�")]
    //������ �Ÿ�
    public float distance = 6.0f;

    //�߰� ����
    public float height = 1.75f;

    //smooth time
    public float heightDamp = 2f;
    public float rotationDamping = 3f;

    [Header("2��Īī�޶�")]
    public float rotationSpd = 10f;


    [Header("1��Īī�޶�")]
    //���콺 ī�޶� ���� Ƽ���� ��ǥ
    public float detailX = 5f;
    public float detailY = 5f;

    //���콺 ȸ�� ��

    private float rotationX = 0f;
    private float rotationY = 0f;

    //ĳ��
    public Transform posFirstTarget = null;


    /// <summary>
    /// 1��Ī ���� ī�޶� ����
    /// </summary>
    void FirstCamera() {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotationX = cameraTransform.localEulerAngles.y + mouseX * detailX;
        rotationX = (rotationX > 180.0f) ? rotationX - 360.0f : rotationX;
        rotationY = rotationY * mouseX * detailX; //��� ���Ϸ��� �ʿ���ھ� ����?
        rotationY = (rotationY > 180.0f) ? rotationY - 360.0f : rotationY;

        cameraTransform.localEulerAngles = new Vector3(-rotationY, rotationX, 0f); //x��ġ�� y�� ���� ������? rotationX�� ���� ������?
        cameraTransform.position = posFirstTarget.position;
    }


    /// <summary>
    /// 2��Ī ī�޶� ����
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
    private void LateUpdate() { //�̵��ϰ� �����ؾߵǼ� �׷��� ����
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
        //���� Ÿ�� y�� ���� ��
        float objTargetRotationAngle = objTargetTransfrom.eulerAngles.y; //����ҋ��� ������ �Ȥ�����

        //���� Ÿ�� ���� + ī�޶� ��ġ�� ���� �߰� ����
        float objHeight = objTargetTransfrom.position.y + height;


        //���� ���� ����
        float nowRotationAnlge = cameraTransform.eulerAngles.y;
        float nowHeight = cameraTransform.position.y;

        //���� ������ ���� Damp
        nowRotationAnlge = Mathf.LerpAngle(nowRotationAnlge, objTargetRotationAngle, rotationDamping * Time.deltaTime); // hd

        //���� ���̿� ���� Damp
        nowHeight = Mathf.Lerp(nowHeight, objHeight, heightDamp * Time.deltaTime);

        //����Ƽ ������ ����
        Quaternion nowRotation = Quaternion.Euler(0f, nowRotationAnlge, 0f);
        //�ϱⰡ �ƴ϶� ����, 3�ð�, 

        cameraTransform.position = objTargetTransfrom.position;
        cameraTransform.position -= nowRotation * Vector3.forward * distance;


        //�����̵�
        cameraTransform.position = new Vector3(cameraTransform.position.x, nowHeight, cameraTransform.position.z);

        cameraTransform.LookAt(objTargetTransfrom);
    }
}
