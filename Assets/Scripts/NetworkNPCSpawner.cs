using UnityEngine;
using Unity.Netcode;

public class NetworkNPCSpawner : NetworkBehaviour
{
    [SerializeField] GameObject npcPrefab;

    float time = 3f;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
    }


}
