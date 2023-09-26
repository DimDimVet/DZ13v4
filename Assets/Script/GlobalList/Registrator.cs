//using Photon.Pun;
using UnityEngine;

public class Registrator : MonoBehaviour
{
    private IRegistrator dataReg;
    private RegistratorConstruction registrator;

    private void Start()
    {
        dataReg = new RegistratorExecutor();
        registrator = new RegistratorConstruction
        {
            //IsDestroyGO = false,
            //Hash = gameObject.GetHashCode(),
            //HealtObj = GetComponent<Healt>(),
            //PlayerHealt = GetComponent<PlayerHealt>(),
            //ShootPlayer = GetComponent<ShootPlayer>(),
            //CameraMove = GetComponent<CameraMove>(),
            //UserInput = GetComponent<UserInput>(),
            //NetworkManager = GetComponent<NetworkManager>(),
            //ControlInventory= GetComponent<ControlInventory>(),
            //PickUpItem = GetComponent<PickUpItem>()
        };

        if (/*PhotonView.Get(this.gameObject) is PhotonView*/true)
        {
            if (registrator.NetworkManager == null)
            {
                registrator.PhotonHash = /*PhotonView.Get(this.gameObject).ViewID*/1001;
                registrator.PhotonIsMainGO = /*PhotonView.Get(this.gameObject).IsMine*/true;
            }
        }

        dataReg.SetData(registrator);
    }

}
