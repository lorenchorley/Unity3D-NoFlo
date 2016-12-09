using UnityEngine;
using UnityEngine.UI;

public class DebugMessageVisualisation : Visualisation {

    public Text Text;

    public void SetMessage(string message) {
        Text.text = message;
    }

    public void Setup(string message, Transform target) {
        Text.text = message;
        transform.SetParent(target);
        transform.localPosition = Vector3.zero;
    }

    public override void Select() {
    }

    public override void Deselect() {
    }

}
