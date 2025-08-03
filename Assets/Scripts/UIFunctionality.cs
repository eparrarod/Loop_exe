using UnityEngine;
using UnityEngine.UIElements;

public class UIFunctionality : MonoBehaviour{

    private ProgressBar goals;
    void Start() {

    }

    void Update() {
        if(goals != null) {
            goals.highValue = GridManager.singleton.targetCount;
            goals.value = GridManager.singleton.targetsReached;
        }
    }

    public void OnEnable() {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        goals = root.Q<ProgressBar>("GoalBar");
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
