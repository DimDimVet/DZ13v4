using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerCamera : MonoBehaviour
{
    [SerializeField] Transform playerCameraRoot;
    private void Awake()
    {
        GameObject virtualCamera = GameObject.Find("PlayerFollowCamera");
        //�����
    }

    private void Start()
    {
        NetworkObject thisOblect = GetComponent<NetworkObject>();

        if (thisOblect.HasStateAuthority)//������������ �������� ������� 
        {

        }
    }
}
