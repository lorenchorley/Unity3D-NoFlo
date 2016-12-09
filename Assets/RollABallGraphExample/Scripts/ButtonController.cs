using UnityEngine;

public class ButtonController : UnityGraphObject {

    public override string GetObjectType() {
        return "Button";
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player")
            TriggerEvent("Pressed");
    }

}
