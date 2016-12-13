using NoFlo_Basic;
using UnityEngine;
using UnityEngine.UI;

namespace NoFloEditor {

    public class PortInfo : MonoBehaviour {

        public bool IsInPort;
        public Text PortName;
        public Text PortType;
        public InputField DefaultValue;
        public Button SelectVariable;

        public Port Port;

        DefaultValue dv;

        public void Setup(InPort p, NodeInfoDialog NodeInfoDialog) {
            Port = p;
            PortName.text = p.Name;
            PortType.text = p.TypesToString();

            DefaultValue.onEndEdit.AddListener((s) => {
                if (s == "") {
                    // TODO Delete default value
                    p.Component.Graph.RemoveDefaultValue(p);
                } else {
                    // TODO Check data types, if string, ok.
                    // If number, try to parse.

                    if (p.Component.Graph.DefaultValuesByInPort.TryGetValue(p, out dv)) {
                        dv.SetData(s);
                    } else {
                        p.Component.Graph.AddDefaultValue(s, p);
                    }

                }
            });

            if (p.Component.Graph.DefaultValuesByInPort.TryGetValue(p, out dv)) {
                DefaultValue.text = dv.Data.ToString();
            }

            SelectVariable.onClick.AddListener(() => {
                NodeInfoDialog.SelectVariableModeFor(this);
            });

        }

        public void Setup(Port p) {
            PortName.text = p.Name;
            PortType.text = p.TypesToString();
            Port = p;
        }

    }

}