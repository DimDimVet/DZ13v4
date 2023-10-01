//using Photon.Pun;
using Fusion;
using UnityEngine;

public class PlayerHealt : /*MonoBehaviour*/NetworkBehaviour
{
    //[Networked] public int HealtCount { get; set; }
    [Networked(OnChanged = nameof(HealtChanged))] public int HealtCount { get; set; }//метод для проверки факта изменения на сервере

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
        thisOblect = GetComponent<NetworkObject>();//найдем компонент
        Debug.Log($"start {thisOblect.Id}= {HealtCount}");
        if (settingsData.Healt != 0)
        {
            HealtCount = settingsData.Healt;//отправим на сервер новое значение
        }
    }

    private static void HealtChanged(Changed<PlayerHealt> changed)
    {
        changed.LoadNew();//попросим сервер дать нам новое значение переменой
        int ChangedNew = changed.Behaviour.HealtCount;
        Debug.Log($"Проверим новое значение {ChangedNew}");
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void GetDamageRpc(int damage)//в имени указать Rpc - важно public void GetDamageRpc(int damage)
    {
        if (isOneTriger)//локальное изменение, значения изменим по изменению на сервере
        {
            Debug.Log($"Изменено на сервера {thisOblect.Id} {HealtCount} {damage}");
            isOneTriger = false;
        }
        else//изменение на сервере, соотв изменим локально
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
            Debug.Log($"Получен урон локально {thisOblect.Id} {HealtCount} {damage}");
        }
    }

    public void DestoyGO()
    {
        dataReg = new RegistratorExecutor();//доступ к листу
        rezultNetManager = dataReg.NetManager();
        rezultNetManager.NetworkObject.DestroyThisGO(this.gameObject);
    }
}
