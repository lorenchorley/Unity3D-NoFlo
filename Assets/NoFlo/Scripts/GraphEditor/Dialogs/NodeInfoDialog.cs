using UnityEngine;
using UnityEngine.UI;
using NoFlo_Basic;

namespace NoFloEditor {

    public class NodeInfoDialog : MonoBehaviour {

        public Text ComponentName;
        public RectTransform InPortsContainer;
        public RectTransform OutPortsContainer;

        public GameObject InPortInfoTemplate;
        public GameObject OutPortInfoTemplate;
        public GameObject VariableTemplate;

        public GameObject VariableSelection;

        void Start() {
            gameObject.SetActive(false);
        }

        public void SetNode(NoFlo_Basic.Component Component) {
            NormalMode();
            ComponentName.text = Component.ComponentName;

            foreach (Transform t in InPortsContainer)
                Destroy(t.gameObject);
            foreach (Transform t in OutPortsContainer)
                Destroy(t.gameObject);

            foreach (InPort p in Component.Input.GetPorts()) {
                if (p.Hidden)
                    continue;

                PortInfo info = GameObject.Instantiate<GameObject>(InPortInfoTemplate).GetComponent<PortInfo>();
                info.transform.SetParent(InPortsContainer);
                info.Setup(p, this);
            }

            foreach (OutPort p in Component.Output.GetPorts()) {
                if (p.Hidden)
                    continue;

                PortInfo info = GameObject.Instantiate<GameObject>(OutPortInfoTemplate).GetComponent<PortInfo>();
                info.transform.SetParent(OutPortsContainer);
                info.Setup(p);
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);

        }

        public void SelectVariableModeFor(PortInfo portInfo) {
            InPortsContainer.gameObject.SetActive(false);
            OutPortsContainer.gameObject.SetActive(false);
            VariableSelection.SetActive(true);

            foreach (Transform t in VariableSelection.transform) {
                Destroy(t.gameObject);
            }

            InPort Port = portInfo.Port as InPort;
            foreach (UnityGraphObject v in Port.Component.Graph.AssociatedInterlink.GetLinkedVariables()) {

                // Type check
                bool found = false;
                for (int i = 0; i < Port.Types.Length; i++) {
                    if (Port.Types[i] == v.GetType()) {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    continue;

                GameObject variableSelector = GameObject.Instantiate<GameObject>(VariableTemplate);
                variableSelector.transform.SetParent(VariableSelection.transform);
                variableSelector.GetComponentInChildren<Text>().text = v.GetObjectID() + " : " + v.GetObjectType();
                variableSelector.GetComponent<Button>().onClick.AddListener(() => {
                    NormalMode();
                    Port.Component.Graph.SetDefaultValue(v, Port);
                });
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(VariableSelection.transform as RectTransform);

        }

        public void NormalMode() {
            InPortsContainer.gameObject.SetActive(true);
            OutPortsContainer.gameObject.SetActive(true);
            VariableSelection.SetActive(false);
        }

    }

}