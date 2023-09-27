
using Fusion;
using UnityEngine;

public struct RegistratorConstruction
{
    public int Hash;
    public NetworkId PhotonHash;
    public bool IsDestroyGO { get; set; }
    public bool PhotonIsMainGO;
    public ControlInventory ControlInventory;
    public Healt HealtObj;
    public PlayerHealt PlayerHealt;
    public ShootPlayer ShootPlayer;
    public CameraMove CameraMove;
    public string Name;
    public UserInput UserInput;
    public NetworkManager NetworkObject;
    public PickUpItem PickUpItem;

}
