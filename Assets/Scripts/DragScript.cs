using UnityEngine;

public class DragScript : MonoBehaviour{

    bool dragging;
    public GameObject current;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        dragging = false;
    }

    // Update is called once per frame
    void Update(){

        // TODO: Detect and instantiate corresponding block
        if (!dragging && Input.GetMouseButtonDown(0)) {
            dragging = true;
        }

        //TODO add bounds so that it can only be dragged within the code area

        if (dragging && Input.GetMouseButton(0)) {
            Vector3 goal = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            goal.z = 0.0f;
            current.transform.position = Vector3.MoveTowards(transform.position, goal, 0.1f);
        }

        if (dragging && Input.GetMouseButtonUp(0)) {
            current = null;
            dragging = false;
        }
    }
}
