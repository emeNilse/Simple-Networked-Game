using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class Car : NetworkBehaviour
{
    Vector3 startPosition;
    Vector3 currentPosition;
    float spawnZRange;
    AudioSource myAudio;
    Renderer myRen;
    

    [SerializeField]
    GameObject spawnZone;
    
    //change to Start? Because I use onnetworkspawn in clientplayermove
    public override void OnNetworkSpawn()
    {
        spawnZRange = Random.Range(spawnZone.transform.position.z - 10.0f, spawnZone.transform.position.z + 10.0f);
        startPosition = new Vector3(spawnZone.transform.position.x, spawnZone.transform.position.y + 1, spawnZRange);
        transform.position = startPosition;
        myAudio = GetComponent<AudioSource>();
        myRen = GetComponent<Renderer>();
    }

    void Update()
    {
        currentPosition = transform.position;

        if (IsOwner && Input.GetKeyDown(KeyCode.K))
        {
            ResetRotation();
        }

        if(IsOwner && Input.GetKeyDown(KeyCode.G))
        {
            GreetingSoundClientRpc();
        }

        if (IsOwner && Input.GetKeyDown(KeyCode.C))
        {
            Color carColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            ChangeColourClientRpc(carColor);
        }

        if (IsOwner && Input.GetKeyDown(KeyCode.I))
        {
            TryAndInteractWithNPC();
        }

    }

    void ResetRotation()
    {
        transform.position = new Vector3(currentPosition.x, currentPosition.y + 1, currentPosition.z);
        transform.rotation = Quaternion.identity;
    }

    [Rpc(SendTo.ClientsAndHost)]
    void GreetingSoundClientRpc()
    {
        myAudio.Play();
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
    }

        [Rpc(SendTo.ClientsAndHost)]
    void ChangeColourClientRpc(Color color)
    {
        myRen.material.color = color;
    }
}
