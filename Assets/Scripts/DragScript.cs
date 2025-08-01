using UnityEngine;

public class DragScript : MonoBehaviour {

    public bool dragable;
    public bool dragging;
    public GameObject current;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        dragable = dragging = false;
    }

    // Update is called once per frame
    void Update() {

        ////TODO add bounds so that it can only be dragged within the code area

        if (dragging && Input.GetMouseButton(0)) {
            Vector3 goal = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            goal.z = 0.0f;
            current.transform.position = Vector3.MoveTowards(current.transform.position, goal, 0.1f);
        }

        ////TODO add bounds so that if released outside code pen it dissapearss

        if (dragging && Input.GetMouseButtonUp(0)) {
            current = null;
            dragging = false;
        }
    }

    //OnMouseDownOnly works for LeftClick
    public void OnMouseDown() {
        Debug.Log("OnMouseDown");
        current = Instantiate(gameObject);
        current.GetComponent<DragScript>().dragable = true;
        dragging = true;
    }

    public void OnMouseDrag() {
        
    }

}
