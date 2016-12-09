using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnityGraphObject : MonoBehaviour, IGraphObject {

    public string ID;

    private List<InPort> Subscribers;
    private List<Processable> SubscribedComponents;

    public abstract string GetObjectType();

    public UnityGraphObject() {
        Subscribers = new List<InPort>();
        SubscribedComponents = new List<Processable>();
    }

    public string GetObjectID() {
        return ID;
    }
    
    public void TriggerEvent(object Data) {
        HashSet<Graph> GraphsToContinue = new HashSet<Graph>();
        for (int i = 0; i < Subscribers.Count; i++) {
            Subscribers[i].Accept(Data);
            GraphsToContinue.Add(Subscribers[i].Component.Graph);
        }
        foreach (Graph Graph in GraphsToContinue) {
            Graph.CurrentExecutor.ContinueExecution(SubscribedComponents);
        }
    }

    public void SubscribeToEvents(InPort InPort) {
        if (Subscribers.Contains(InPort))
            throw new Exception("TODO");

        Subscribers.Add(InPort);

        if (!SubscribedComponents.Contains(InPort.Component)) // TODO take advantage of list indexing. Add the corresponding component at the same index as the port in it's own list
            SubscribedComponents.Add(InPort.Component);
    }

    public void UnsubscribeFromEvents(InPort InPort) {
        if (Subscribers.Contains(InPort))
            Subscribers.Remove(InPort);
        if (SubscribedComponents.Contains(InPort.Component))
            SubscribedComponents.Remove(InPort.Component);
    }

    public bool IsSubscribedToGraph(Graph Graph) {
        for (int i = 0; i < Subscribers.Count; i++) {
            if (Subscribers[i].Component.Graph == Graph)
                return true;
        }
        return false;
    }

    public void ForcablyUnsubscribeFromGraph(Graph Graph) {
        for (int i = Subscribers.Count - 1; i >= 0; i--) {
            if (Subscribers[i].Component.Graph == Graph)
                Subscribers.RemoveAt(i);
        }
    }

}
