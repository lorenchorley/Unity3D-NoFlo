using System.Collections.Generic;
using UnityEngine;

public class LinePointManager : MonoBehaviour {

    private List<LinePointUpdater> Updaters;

    void Awake() {
        Updaters = new List<LinePointUpdater>();
    }

    public void RegisterUpdater(LinePointUpdater updater) {
        if (Updaters.Contains(updater))
            throw new System.Exception("TODO");

        Updaters.Add(updater);
    }

    public void UnregisterUpdater(LinePointUpdater updater) {
        if (!Updaters.Contains(updater))
            throw new System.Exception("TODO");

        Updaters.Remove(updater);
    }

    public void UpdateLPs() {
        for (int i = 0; i < Updaters.Count; i++) {
            Updaters[i].UpdateLP();
        }
    }

}