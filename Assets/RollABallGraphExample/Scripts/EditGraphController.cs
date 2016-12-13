using NoFlo_Basic;
using NoFloEditor;
using UnityEngine;

public class EditGraphController : MonoBehaviour {

    public Graph Graph;
    public GraphEditor GraphEditor;
    public float NormalScale = 0.5f;
    public float MouseOverScale = 1;
    
    void OnMouseEnter() {
        transform.localScale = Vector3.one * MouseOverScale;
    }

    void OnMouseOver() {
        if (Input.GetMouseButtonUp(0)) {
            GraphEditor.Open(Graph);
        } 
    }

    void OnMouseExit() {
        transform.localScale = Vector3.one * NormalScale;
    }

}
