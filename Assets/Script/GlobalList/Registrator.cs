//using Photon.Pun;
using Fusion;
using UnityEngine;

public class Registrator : MonoBehaviour
{
    private IRegistrator dataReg;
    private RegistratorConstruction registrator;

    private NetworkObject thisOblect;//найдем компонент

    private void Start()
    {
        thisOblect = GetComponent<NetworkObject>();//найдем компонент

        dataReg = new RegistratorExecutor();
        registrator = new RegistratorConstruction
        {
            IsDestroyGO = false,
            Hash = gameObject.GetHashCode(),
            HealtObj = GetComponent<Healt>(),
            PlayerHealt = GetComponent<PlayerHealt>(),
            ShootPlayer = GetComponent<ShootPlayer>(),
            CameraMove = GetComponent<CameraMove>(),
            UserInput = GetComponent<UserInput>(),
            //NetworkObject = GetComponent<NetworkObject>(),
            ControlInventory = GetComponent<ControlInventory>(),
            PickUpItem = GetComponent<PickUpItem>()
        };

        if (thisOblect!=null)
        {
            if (thisOblect.HasStateAuthority)
            {
                registrator.PhotonHash = thisOblect.Id;
                registrator.PhotonIsMainGO = thisOblect.HasStateAuthority;
            }
        }
        

        dataReg.SetData(registrator);
    }

}
