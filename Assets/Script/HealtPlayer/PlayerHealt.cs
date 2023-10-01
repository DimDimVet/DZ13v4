//using Photon.Pun;
using Fusion;
using UnityEngine;

public class PlayerHealt : /*MonoBehaviour*/NetworkBehaviour
{
    //[Networked] public int HealtCount { get; set; }
    [Networked(OnChanged = nameof(HealtChanged))] public int HealtCount { get; set; }//����� ��� �������� ����� ��������� �� �������

    [SerializeField] private HealtSetting settingsData;

    //[HideInInspector] public int HealtCount;
    [HideInInspector] public int Damage;
    [HideInInspector] public bool Dead = false;

    private bool isOneTriger = true;

    private IRegistrator dataReg;
    private RegistratorConstruction rezultNetManager;

    private NetworkObject thisOblect;
    void Start()
    {
        thisOblect = GetComponent<NetworkObject>();//������ ���������
        Debug.Log($"start {thisOblect.Id}= {HealtCount}");
        if (settingsData.Healt != 0)
        {
            HealtCount = settingsData.Healt;//�������� �� ������ ����� ��������
        }
    }

    private static void HealtChanged(Changed<PlayerHealt> changed)
    {
        changed.LoadNew();//�������� ������ ���� ��� ����� �������� ���������
        int ChangedNew = changed.Behaviour.HealtCount;
        Debug.Log($"�������� ����� �������� {ChangedNew}");
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void GetDamageRpc(int damage)//� ����� ������� Rpc - ����� public void GetDamageRpc(int damage)
    {
        if (isOneTriger)//��������� ���������, �������� ������� �� ��������� �� �������
        {
            Debug.Log($"�������� �� ������� {thisOblect.Id} {HealtCount} {damage}");
            isOneTriger = false;
        }
        else//��������� �� �������, ����� ������� ��������
        {
            HealtCount -= damage;

            if (!Object.HasInputAuthority)
            {
                if (HealtCount <= 0)
                {
                    Dead = true;
                    DestoyGO();
                }
                Damage = 0;
            }

            isOneTriger = true;
            Debug.Log($"������� ���� �������� {thisOblect.Id} {HealtCount} {damage}");
        }
    }

    public void DestoyGO()
    {
        dataReg = new RegistratorExecutor();//������ � �����
        rezultNetManager = dataReg.NetManager();
        rezultNetManager.NetworkObject.DestroyThisGO(this.gameObject);
    }
}
