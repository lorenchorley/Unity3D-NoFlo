using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTreatment {
    
    public static object TreatData(object Data, Graph Graph) {
        if (Data is IDictionary) {
            Dictionary<string, object> d = Data as Dictionary<string, object>;
            if (d.ContainsKey("type") && d.ContainsKey("id")) {
                IGraphObject variable;
                string id = d["id"] as string;

                if (!Graph.VariablesByID.TryGetValue(id, out variable))
                    throw new Exception("Object referenced in graph " + Graph.GraphFile.name + " not present: " + id);

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
