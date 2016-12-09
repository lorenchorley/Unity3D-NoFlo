using UnityEngine;

public class MouseTracker : MonoBehaviour {
    
    void Update() {
        transform.position = UnityEngine.Input.mousePosition;
    }

}