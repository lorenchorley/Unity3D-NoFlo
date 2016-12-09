using System;
using System.Collections.Generic;
using UnityEngine;

public class InPort : Port {

    public bool RememberOnlyLatest;

    public Action<OutPort> ProcessConnection;
    public Action<OutPort> ProcessDisconnection;

    private LinkedList<object> receivedQueue;
    private object onlyData;

    DebugMessageVisualisation currentMessage;

    public InPort() {
        receivedQueue = new LinkedList<object>();
        RememberOnlyLatest = false;
    }

    public override bool IsInput() {
        return true;
    }

    public void Accept(object data) {
        if (RememberOnlyLatest)
            onlyData = data;
        else
            receivedQueue.AddLast(data);
    }

    public bool HasData() {
        if (RememberOnlyLatest)
            return onlyData != null;
        else
            return receivedQueue.Count > 0;
    }

    public int GetDataCount() {
        if (RememberOnlyLatest)
            return (onlyData == null) ? 0 : 1;
        else
            return receivedQueue.Count;
    }

    public void ClearData() {
        if (!RememberOnlyLatest)
            receivedQueue.Clear();
    }

    public object GetData() {
        if (RememberOnlyLatest) {
            return onlyData;
        }

        object data = receivedQueue.First.Value;
        receivedQueue.RemoveFirst();

        if (currentMessage != null) {
            if (receivedQueue.Count == 0) {
                GameObject.Destroy(currentMessage.gameObject);
            } else {
                currentMessage.SetMessage(receivedQueue.First.Value.ToString());
            }
        }

        return data;
    }
    
    public override void Process(ExecutionContext context) {
        context.QueueTask(Component);
    }

    public override void DebugHighlight() {
        if (Component.Graph.GraphEditor != null && Visualisation != null) { 
            currentMessage = GameObject.Instantiate<GameObject>(Component.Graph.GraphEditor.Templates.DebugMessageTemplate).GetComponent<DebugMessageVisualisation>();
            currentMessage.Setup(receivedQueue.First.Value.ToString(), Visualisation.transform); // TODO fix InvalidOperationException
        }
    }

    public override void DebugUnhighlight() {
    }

    public override string ToString() {
        return Component.ComponentName + "." + Name;
    }

}
