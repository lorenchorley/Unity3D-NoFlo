using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoFlo_Basic {

    public class DataTreatment {

        public static object TreatData(object Data, Graph Graph) {
            if (Data is IDictionary) {
                Dictionary<string, object> d = Data as Dictionary<string, object>;
                if (d.ContainsKey("type") && d.ContainsKey("id")) {
                    string id = d["id"] as string;
                    IGraphObject variable = Graph.AssociatedInterlink.GetLinkedVariableByID(id);

                    if (d["type"] as string != variable.GetObjectType())
                        throw new Exception("TODO");

                    return variable;
                } else {
                    throw new Exception("TODO");
                }
            } else {
                return Data;
            }
        }

    }

}