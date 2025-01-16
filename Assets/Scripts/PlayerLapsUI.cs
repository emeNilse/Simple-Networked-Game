using UnityEngine;
using Unity.Netcode;
using TMPro;


public class PlayerLapsUI : MonoBehaviour
{
 
    //PlayerNetworkLaps playerNetworkLaps;
    public TMP_Text lapText;

    void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnclientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnclientDisconnected;
    }

    void OnDestroy()
    {
        if(NetworkManager.Singleton)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnclientConnected;
            NetworkManager.Singleton.OnClientConnectedCallback -= OnclientDisconnected;
        }
    }

    void OnclientConnected(ulong clientId)
    {
        if (IsLocalClient(clientId))
        {
            NetworkObject playerNetworkObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
            PlayerNetworkLaps playerNetworkLaps = playerNetworkObject.GetComponent<PlayerNetworkLaps>();
            if (playerNetworkLaps)
            {
                OnPlayerLapChanged(playerNetworkLaps.Lap);
                playerNetworkLaps.OnLapChanged += OnPlayerLapChanged;
            }
        }
        //playerNetworkLaps = FindAnyObjectByType<PlayerNetworkLaps>();
        //playerNetworkLaps.OnLapChanged += OnPlayerLapChanged;
        
    }

    private bool IsLocalClient(ulong clientId)
    {
        return NetworkManager.Singleton.LocalClientId == clientId;
    }

    void OnclientDisconnected(ulong clientId)
    {
        if (IsLocalClient(clientId))
        {
            //NetworkObject playerNetworkObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
            //PlayerNetworkLaps playerNetworkLaps = playerNetworkObject.GetComponent<PlayerNetworkLaps>();
            //if (playerNetworkLaps)
            //{
            //    playerNetworkLaps.OnLapChanged -= OnPlayerLapChanged;
            //}
            OnPlayerLapChanged(-1);
        }

        //playerNetworkLaps.OnLapChanged -= OnPlayerLapChanged;
    }

    void OnPlayerLapChanged(int newLapValue)
    { //textmeshpro
        lapText.text = "Lap: " + newLapValue.ToString();

    }
}
