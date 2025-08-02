using UnityEngine;
using UnityEngine.UIElements;

public class UIFunctionality : MonoBehaviour{

    /// UI related functionality

    public void OnEnable() {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button runButton = root.Q<Button>("RunButton");
        runButton.clicked += OnRunClick;
        Button clearButton = root.Q<Button>("ClearButton");
        clearButton.clicked += OnClearClick;
    }

    void OnRunClick() {
        Debug.Log($"Clicked!{ExeBox.singleton.commandsRaw.Count}");
        GridManager.singleton.execute(ExeBox.singleton.commandsRaw);
    }

    void OnClearClick() {
        Debug.Log($"Clicked! Clear");
        ExeBox.singleton.clearQueue();
    }
}
