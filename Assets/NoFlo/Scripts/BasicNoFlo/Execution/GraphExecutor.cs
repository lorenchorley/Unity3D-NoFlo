using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NoFlo_Basic {

    public abstract class GraphExecutor : MonoBehaviour {

        [Serializable]
        public class ExecutorEvents {
            public UnityEvent OnStart;
            public UnityEvent OnStop;
            public UnityEvent OnIdle;
            public UnityEvent OnResume;
        }
        public ExecutorEvents Events;

        protected Graph Graph;
        protected bool isInitialised = false;

        protected abstract void _Setup();
        protected abstract void _Stop();
        protected abstract void _ExecuteGraph(Graph Graph);
        protected abstract void _ContinueExecutionOn(Processable tasks);
        protected abstract void _ContinueExecutionOn(List<Processable> tasks);
        public abstract bool IsRunningGraph(Graph Graph);
        public abstract bool IsIdle();
        public abstract bool IsExecuting();
        public abstract bool IsStopped();
        public abstract ExecutionContext GetExecutionContext();

        void Awake() {
            StartExecution();
        }

        public void StartExecution() {
            if (isInitialised)
                return;

            _Setup();

            isInitialised = true;
        }

        public void StopExecution() {
            _Stop();
            if (Events.OnStop != null)
                Events.OnStop.Invoke();
        }

        public void ExecuteGraph(Graph Graph) {
            this.Graph = Graph;

            if (Events.OnStart != null)
                Events.OnStart.Invoke();

            _ExecuteGraph(Graph);
        }

        public void ContinueExecution(Processable task) {
            if (IsStopped())
                throw new System.Exception("Cannot continue execution on graph that is not running");

            if (Events.OnResume != null && IsIdle())
                Events.OnResume.Invoke();

            _ContinueExecutionOn(task);
        }

        public void ContinueExecution(List<Processable> tasks) {
            if (IsStopped())
                throw new System.Exception("Cannot continue execution on graph that is not running");

            if (Events.OnResume != null && IsIdle())
                Events.OnResume.Invoke();

            _ContinueExecutionOn(tasks);
        }

    }

}