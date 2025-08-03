using UnityEngine;
using UnityEngine.UIElements;

public class UIFunctionality : MonoBehaviour{

    /// UI related functionality

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

    //    SetFillColor(Color.green);
    //    SetFillColor(Color.green);
    }

    //public void SetFillColor(Color color) {
    //    var actualBar = goals.Q(className: "unity-progress-bar__progress");
    //    actualBar.style.backgroundColor = new StyleColor(color);
    //}

    //public void SetBackgroundColor(Color color) {
    //    goals.style.backgroundColor = new StyleColor(color);
    //}

    void OnRunClick() {
        Debug.Log($"Clicked!{ExeBox.singleton.commandsRaw.Count}");
        GridManager.singleton.execute(ExeBox.singleton.commandsRaw);
    }

    void OnClearClick() {
        Debug.Log($"Clicked! Clear");
        ExeBox.singleton.clearQueue();
    }
}
