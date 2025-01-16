using UnityEngine;
using Unity.Netcode;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class PlayerNetworkLaps : NetworkBehaviour
{
    private NetworkVariable<int> lapVar = new(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    //private NetworkList<int> lapVarList;
    private List<int> lapList = new List<int>();
    private List<int> lapCheck = new List<int>();
    public UnityAction<int> OnLapChanged;

    public int Lap
    {
        get => lapVar.Value;
    }

    private void Awake()
    {
        //lapVarList = new NetworkList<int>(new List<int>(), NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        lapCheck.Add(1);
        lapCheck.Add(2);
        lapCheck.Add(3);
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        lapVar.OnValueChanged += OnLapValueChanged;
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        lapVar.OnValueChanged -= OnLapValueChanged;
    }

    private void OnLapValueChanged(int oldValue, int newValue)
    {
        OnLapChanged?.Invoke(newValue);
    }

    void Update()
    {
        //when checkpoints 1, 2, and end reached, update lap
        if (!IsOwner) return;

        Debug.Log(OwnerClientId + "; " + lapVar.Value);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Checkpoint 1"))
        {
            lapList.Add(1);
            Debug.Log("checkpoint 1 reached");
        }
        if(other.gameObject.CompareTag("Checkpoint 2"))
        {  
            lapList.Add(2);
            Debug.Log("checkpoint 2 reached");
        }
        if(other.gameObject.CompareTag("Checkpoint 3"))
        {  
            lapList.Add(3);
            Debug.Log("checkpoint 3 reached");
            CompareLists();
            lapList.Clear();
        }
    }

    private void CompareLists()
    {
        if (lapList.Count != lapCheck.Count)
        {
            return;
        }

        for (int i = 0; i < lapCheck.Count; i++)
        {
            if(lapList[i] != lapCheck[i])
            {
                return;
            }
        }

        lapVar.Value += 1;
        Debug.Log("Lap complete");
    }
}
