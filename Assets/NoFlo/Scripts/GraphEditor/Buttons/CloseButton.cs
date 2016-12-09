using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour {

    public GraphEditor GraphEditor;

    void Start() {
        GetComponent<Button>().onClick.AddListener(() => {
            GraphEditor.Close();
        });
    }
    
}
