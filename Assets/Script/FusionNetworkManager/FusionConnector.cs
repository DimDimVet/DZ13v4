using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class FusionConnector : MonoBehaviour,INetworkRunnerCallbacks
{
    public static FusionConnector Instance;
    public bool ConnectOnAwake = false;
    [HideInInspector]public NetworkRunner NetworkRunner;

    [SerializeField] private NetworkObject playerPrefab;

    public string PlayerName;
    //Bulls
    //public List<GameObject> bulls;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (ConnectOnAwake==true) 
        {
            ConnectToRunner("Anon");
        }
    }

    public async void ConnectToRunner(string _playerName)
    {
        PlayerName = _playerName;

        if (NetworkRunner == null)
        {
            NetworkRunner = gameObject.AddComponent<NetworkRunner>();//добавим сам компонент NetworkRunne
        }
        //создание сеанса и его настройки, см. https://doc.photonengine.com/fusion/v2/manual/connection-and-matchmaking/matchmaking
        await NetworkRunner.StartGame(new StartGameArgs 
        {
            GameMode=GameMode.Shared,
            //SessionName=roomName,
            SessionName = "TestDZ",
            Scene = 0,
            PlayerCount =5,
            SceneManager=gameObject.AddComponent<NetworkSceneManagerDefault>()
            
        });
    }

    public void OnConnectedToServer(NetworkRunner runner) 
    {
        Debug.Log("OnConnectedToServer - ok");

        NetworkObject playerObject =runner.Spawn(playerPrefab,Vector3.zero);

        //по сути это именно локальный созданный объект
        runner.SetPlayerObject(runner.LocalPlayer,playerObject);//отслеживание локального игрока, при выходе уничтожает префаб

        //runner.SetPlayerObject(runner.LocalPlayer, playerObject);

    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("OnPlayerJoined - ok");
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {

    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        
    }
}
