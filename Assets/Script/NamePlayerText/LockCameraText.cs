using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCameraText : MonoBehaviour
{
    private GameObject mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main.gameObject;
    }

    void Update()
    {
        transform.LookAt(mainCamera.transform);
    }
}
