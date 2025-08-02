using UnityEngine;

public class CloneScript : MonoBehaviour {

    public GameObject commandBox;

    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }

    //OnMouseDownOnly works for LeftClick
    public void OnMouseDown() {
        GameObject current = Instantiate(commandBox);

        if (current.GetComponent<NumberType>() != null) {
            current.GetComponent<NumberType>().number = GetComponent<NumberType>().number;
        }
        current.GetComponent<CommandType>().type = GetComponent<CommandType>().type;
        current.GetComponent<DragScript>().dragging = true;
    }
}
