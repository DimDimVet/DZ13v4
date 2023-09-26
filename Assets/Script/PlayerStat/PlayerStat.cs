using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;
using System;

public class PlayerStat : NetworkBehaviour
{
    //public string PlayerName;
    [Networked/*(OnChanged =nameof(UpdatePlayerName))*/] public NetworkString<_32> PlayerName { get ; set ; }//типа стринг только под фотон


    [SerializeField] private TextMeshPro playerNameLabel;

    private IEnumerator Start()
    //private void Start()
    {
        Debug.Log(this.HasStateAuthority);
        if (this.HasStateAuthority)//типа определили мы ли это
        {
            PlayerName = FusionConnector.Instance.PlayerName;
        }


        yield return new WaitUntil(() => this.isActiveAndEnabled);
        yield return new WaitUntil(() => PlayerName.ToString() != null);

        playerNameLabel.text = PlayerName.ToString();
    }

    //public override void Spawned()
    //{
    //    //base.Spawned();
    //    playerNameLabel.text = PlayerName.ToString();
    //}

    protected static void UpdatePlayerName(Changed<PlayerStat> change)
    {
        change.Behaviour.playerNameLabel.text = change.Behaviour.PlayerName.ToString();
    }
}
