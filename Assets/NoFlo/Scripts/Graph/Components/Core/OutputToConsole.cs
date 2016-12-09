﻿using System;
using UnityEngine;

[ComponentName("OutputToConsole")]
[ComponentPackage("core")]
public class OutputToConsole : Component {
    
    public override void Process(ExecutionContext context) {
        InPort In = Input.GetPort("In");
        if (In.HasData()) {
            object data = In.GetData();
            Debug.Log(data.ToString());
            Output.SendDone("PassOn", data, context);
        }
    }

    protected override void SetupInterfaces() {
        
        Input.AddPort(new InPort() {
            Name = "In",
            Description = "The string to output to the console",
            Types = new Type[] { typeof(object) },
            ProcessConnection = (OutPort otherPort) => {
            },
            ProcessDisconnection = (OutPort otherPort) => {
            },
        });

        Output.AddPort(new OutPort() {
            Name = "PassOn",
            Description = "The same string immediately passed on",
            Types = new Type[] { typeof(string) },
            ProcessConnection = (InPort otherPort) => {
            },
            ProcessDisconnection = (InPort otherPort) => {
            },
        });

    }

}
