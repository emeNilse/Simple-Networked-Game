using UnityEngine;
using Unity.Netcode;


public class NetworkNPC : NetworkBehaviour
{
    [SerializeField] GameObject pingMarkPrefab;

    bool hasPingMark = false;
    private GameObject pingMarkSpawned = null;

    [ServerRpc(RequireOwnership = false)]
    public void InteractWithNPCServerRpc(ulong clientId)
    {
        if (pingMarkPrefab != null)
        {
            if (hasPingMark)
            {
                pingMarkSpawned.GetComponent<NetworkObject>().Despawn();
            }
            else
            {
                GameObject pingMark = Instantiate(pingMarkPrefab, transform);
                pingMark.GetComponent<NetworkObject>().Spawn();
                pingMarkSpawned = pingMark;
            }

            hasPingMark = !hasPingMark;
        }

        InteractWithNPCClientRpc(clientId);
    }

    [ClientRpc]
    public void InteractWithNPCClientRpc(ulong clientId)
    {
        Debug.Log("Client with ID " + clientId + " is trying to interact with the NPC");
    }


    //Keeping this so that I can work on game object movement

    //Vector3 startPosition;
    //float freq = 5f;
    //float offset = 0f;
    //float magn = 5f;


    //private void Start()
    //{
    //    startPosition = transform.position;
    //}

    //private void Update()
    //{
    //    if (IsServer)
    //        transform.position = startPosition + transform.forward * Mathf.Sin(Time.time * freq + offset) * magn;
    //}
}
