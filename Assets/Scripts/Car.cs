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
            GreetingSound();
        }

        if (IsOwner && Input.GetKeyDown(KeyCode.C))
        {
            Color carColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            ChangeColourClientRpc(carColor);
        }

    }

    void ResetRotation()
    {
        transform.position = new Vector3(currentPosition.x, currentPosition.y + 1, currentPosition.z);
        transform.rotation = Quaternion.identity;
    }

    void GreetingSound()
    {
        myAudio.Play();
    }
    
    [Rpc(SendTo.ClientsAndHost)]
    void ChangeColourClientRpc(Color color)
    {
        myRen.material.color = color;
    }
}
