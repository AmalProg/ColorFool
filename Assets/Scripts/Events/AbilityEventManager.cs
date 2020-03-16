using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class IntEvent : UnityEvent<int> {
}

public class AbilityEventManager : MonoBehaviour {

    static public IntEvent waterTeleportStartEvent;
    static public UnityEvent waterTeleportEndEvent;

    static public UnityEvent waterPathResetEvent;

    void Awake() {
        if (waterTeleportStartEvent == null)
            waterTeleportStartEvent = new IntEvent();
        if (waterTeleportEndEvent == null)
            waterTeleportEndEvent = new UnityEvent();

        if (waterPathResetEvent == null)
            waterPathResetEvent = new UnityEvent();


    }
}