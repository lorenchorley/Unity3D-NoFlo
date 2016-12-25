using NoFlo_Basic;
using UnityEngine;

namespace NoFloEditor {

    [RequireComponent(typeof(GraphInterlink))]
    public class EditGraphController : MonoBehaviour {

        public Graph Graph;
        public GraphEditor GraphEditor;
        public GraphInterlink AssociatedInterlink;

        public Animator Animator;

        public Renderer Renderer;
        public Material NormalExecution;
        public Material DebugExecution;

        void Awake() {
            if (AssociatedInterlink == null)
                AssociatedInterlink = GetComponent<GraphInterlink>();

            UpdateListeners();
            UpdateAppearence();
        }

        void OnMouseOver() {
            if (Input.GetMouseButtonUp(0) && !GraphEditor.isOpen) {
                GraphEditor.Open(Graph);
                AssociatedInterlink.HideInterconnections();
                AssociatedInterlink.HideRange();
            }
        }

        public void UpdateListeners() {
            if (Graph != null) {
                Graph.OnChangeExecutor.AddListener(OnChangeExecutor);
                Graph.DebugExecutor.Events.OnStart.AddListener(OnGraphStart);
                Graph.DebugExecutor.Events.OnStop.AddListener(OnGraphStop);
                Graph.DebugExecutor.Events.OnIdle.AddListener(OnGraphIdle);
                Graph.DebugExecutor.Events.OnResume.AddListener(OnGraphStart);
                Graph.PrimaryExecutor.Events.OnStart.AddListener(OnGraphStart);
                Graph.PrimaryExecutor.Events.OnStop.AddListener(OnGraphStop);
                Graph.PrimaryExecutor.Events.OnIdle.AddListener(OnGraphIdle);
                Graph.PrimaryExecutor.Events.OnResume.AddListener(OnGraphStart);
            }
        }

        public void UpdateAppearence() {
            if (Graph != null && Graph.CurrentExecutor != null) {
                
                if (Graph.CurrentExecutor.IsStopped()) {
                    OnGraphStop();
                } else if (Graph.CurrentExecutor.IsIdle()) {
                    OnGraphIdle();
                } else {
                    OnGraphStart();
                }

            }
        }

        private void OnGraphStart() {
            Animator.SetTrigger("Start");
        }

        private void OnGraphStop() {
            Animator.SetTrigger("Stop");
        }

        private void OnGraphIdle() {
            Animator.SetTrigger("Idle");
        }

        private void OnChangeExecutor() {
            if (Graph.InDebugMode()) {
                SetExecutionModeDebug();
            } else {
                SetExecutionModeNormal();
            }
        }

        private void SetExecutionModeNormal() {
            Renderer.material = NormalExecution;
        }

        private void SetExecutionModeDebug() {
            Renderer.material = DebugExecution;
        }

    }

}