using NoFlo_Basic;
using UnityEngine;

public class DoorController : UnityGraphObject {
    
    public Animator Animator;
    private bool isOpen;

    public override string GetObjectType() {
        return "Door";
    }

    void Awake() {
        isOpen = false;
    }

    public void Toggle() {
        if (isOpen)
            Close();
        else
            Open();
    }

    public void Open() {
        if (!isOpen) { 
            isOpen = true;
            Animator.SetTrigger("Toggle");
            TriggerEvent("open");
        }
    }

    public void Close() {
        if (isOpen) {
            isOpen = false;
            Animator.SetTrigger("Toggle");
            TriggerEvent("closed");
        }
    }

}
