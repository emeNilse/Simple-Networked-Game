using UnityEngine;
using Unity.Netcode;
using Unity.Cinemachine;

public class ClientPlayerMove : NetworkBehaviour
{
    [SerializeField]
    Car _car;
    [SerializeField]
    WheelController _wheelController;
    [SerializeField]
    Transform _cameraFollow;

    private void Awake()
    {
        _car.enabled = false;
        _wheelController.enabled = false;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        enabled = IsClient;

        if(!IsOwner)
        {
            enabled = false;
            _car.enabled = false;
            _wheelController.enabled = false;
            return;
        }

        _car.enabled = true;
        _wheelController.enabled = true;
        FindAnyObjectByType<CinemachineCamera>().Follow = _cameraFollow;
    }
}
