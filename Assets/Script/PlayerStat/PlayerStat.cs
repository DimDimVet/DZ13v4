using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;
using System;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] private TextMeshPro playerNameLabel;
    private NetworkObject thisOblect;
    void Start()
    {
        thisOblect = GetComponent<NetworkObject>();//������ ���������
        playerNameLabel.text = thisOblect.Id.ToString();
    }
}
