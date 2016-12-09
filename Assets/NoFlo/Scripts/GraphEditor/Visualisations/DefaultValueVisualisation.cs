using System;
using UnityEngine.UI;

public class DefaultValueVisualisation : Visualisation {
    
    public DefaultValue DefaultValue;
    public Text Text;

    void Start() {
        Text = GetComponentInChildren<Text>();
        UpdateData();
    }

    public void SetupWithDefaultValue(DefaultValue DefaultValue) {
        this.DefaultValue = DefaultValue;

        if (DefaultValue.Port.Visualisation == null)
            throw new Exception("");

        transform.position = DefaultValue.Port.Visualisation.transform.position;
        transform.SetParent(DefaultValue.Port.Visualisation.transform);

    }

    public void UpdateData() {
        Text.text = DefaultValue.Data.ToString();
    }

    public override void Select() {
    }

    public override void Deselect() {
    }

}