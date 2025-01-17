using UnityEngine;

public class PingMark : MonoBehaviour
{
    public void EnablePing()
    {
        gameObject.SetActive(true);
    }

    public void DisablePing()
    {
        gameObject.SetActive(false);
    }
}

