using UnityEngine;
using Unity.Netcode;


public class PlayerSetup : NetworkBehaviour
{
    
    void Update()
    {
        if(IsOwner && Input.GetKeyDown(KeyCode.I))
        {
            TryAndInteractWithNPC();
        }
    }

    void TryAndInteractWithNPC()
    {

        Debug.Log("We are the owner of our player pressing I");

        var npcsInScene = FindObjectsByType<NetworkNPC>(FindObjectsSortMode.None);

        if (npcsInScene.Length > 0)
        {
            Debug.Log("There is a NPC in the scene");
            var npcInScene = npcsInScene[0];

            npcInScene.InteractWithNPCServerRpc(NetworkManager.Singleton.LocalClientId);
        }


        //LayerMask npcLayerMask = LayerMask.NameToLayer("NPC");
        //RaycastHit[] results = Physics.BoxCastAll(transform.position, new Vector3(2.5f, 2.5f, 2.5f), transform.forward, Quaternion.identity, 5f, );

        //if (results.Length > 0)
        //{
        //    RaycastHit result = results[0];
        //    Debug.Log("There is an NPC nearby");

        //    if (result.collider.gameObject.TryGetComponent<NetworkNPC>(out NetworkNPC npc))
        //    {
        //        Debug.Log("Collider is an actual NetworkNPC");
        //        npc.InteractiveWithNPCServerRpc(NetworkManager.Singleton.LocalClientId);
        //    }
        //}
    }
}
