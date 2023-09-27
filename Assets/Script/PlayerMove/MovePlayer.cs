//using Photon.Pun;
using Fusion;
using System.ComponentModel;
using Unity.Mathematics;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private MoveSettings moveSettings;
    [SerializeField] private Transform cameraPoint;

    private IRegistrator dataReg;
    private RegistratorConstruction rezultListCamera;
    private RegistratorConstruction rezultListInput;

    private float speedMove;
    private Transform transformCamera;
    private float2 angleCamera;

    private NetworkObject thisOblect;

    private bool isRun;
    void Start()
    {
        thisOblect = GetComponent<NetworkObject>();//������ ���������

        speedMove = moveSettings.SpeedMove;
        //���� ������ � ����������
        dataReg = new RegistratorExecutor();//������ � �����
        rezultListInput = dataReg.GetDataPlayer();
        rezultListCamera = dataReg.GetDataCamera();

    }

    void Update()
    {
        //���� ���� �� �����
        if (isRun == false)
        {
            rezultListInput = dataReg.GetDataPlayer();
            if (rezultListInput.PhotonIsMainGO)
            {
                if (rezultListInput.UserInput != null)
                {
                    isRun=true;
                }
            }

        }


        if (thisOblect.HasStateAuthority && isRun) /*PhotonView.Get(this.gameObject).IsMine � thisOblect.HasStateAuthority*/
        {

            //���� ���� �� �����
            if (rezultListCamera.CameraMove == null)
            {
                rezultListCamera = dataReg.GetDataCamera();
                return;
            }

            rezultListCamera.CameraMove.GetTransformPointCamera = transformCamera;
            angleCamera = rezultListCamera.CameraMove.AngleCamera;

            transform.Rotate(Vector3.up, angleCamera.x);//������� �����
            transformCamera = cameraPoint;


            if (rezultListInput.UserInput.InputData.Move.y > 0)
            {
                Vector3 currentPosition = transform.position;
                currentPosition += transform.forward / speedMove;
                transform.position = currentPosition;
            }
            if (rezultListInput.UserInput.InputData.Move.y < 0)
            {
                Vector3 currentPosition = transform.position;
                currentPosition -= transform.forward / speedMove;
                transform.position = currentPosition;
            }

            if (rezultListInput.UserInput.InputData.Move.x > 0)
            {
                Vector3 currentPosition = transform.position;
                currentPosition += transform.right / speedMove;
                transform.position = currentPosition;
            }
            if (rezultListInput.UserInput.InputData.Move.x < 0)
            {
                Vector3 currentPosition = transform.position;
                currentPosition -= transform.right / speedMove;
                transform.position = currentPosition;
            }
        }

    }
}

