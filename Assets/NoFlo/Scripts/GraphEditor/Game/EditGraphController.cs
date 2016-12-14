using NoFlo_Basic;
using NoFloEditor;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NoFloEditor {

    [RequireComponent(typeof(GraphInterlink))]
    public class EditGraphController : MonoBehaviour {

        public Graph Graph;
        public GraphEditor GraphEditor;
        public GraphInterlink AssociatedInterlink;

        void Awake() {
            if (AssociatedInterlink == null)
                AssociatedInterlink = GetComponent<GraphInterlink>();
        }

        void OnMouseOver() {
            if (Input.GetMouseButtonUp(0)) {
                GraphEditor.Open(Graph);
            }
        }

    }

}