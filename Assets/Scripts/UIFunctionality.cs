using UnityEngine;
using UnityEngine.UIElements;

public class UIFunctionality : MonoBehaviour{

    /// UI related functionality

    public void OnEnable() {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button runButton = root.Q<Button>("RunButton");
        runButton.clicked += OnClick;
    }

    void OnClick() {
        Debug.Log("Clicked!");
        GridManager.singleton.execute(ExeBox.singleton.commandsRaw);
    }
}
