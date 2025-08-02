using UnityEngine;

public class DragScript : MonoBehaviour {

    public bool dragging;
    private Vector3 goal;
    public bool isOverQueueArea;
    public GameObject queueArea;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        // Give it some transparency
        setTransparency(0.5f);
    }

    // Update is called once per frame
    void Update() {

        ////TODO add bounds so that it can only be dragged within the code area

        if (dragging && Input.GetMouseButton(0)) {
            goal = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            goal.z = 0.0f;
            transform.position = Vector3.MoveTowards(transform.position, goal, 0.1f);
        }

        
        //Release logic
        if (dragging && Input.GetMouseButtonUp(0)) {

            //Add to Queue
            if (isOverQueueArea) {
                CommandType ct = GetComponent<CommandType>();
                Debug.Log($"DT: {GetComponent<CommandType>().type}");
                queueArea.GetComponent<ExeBox>().addCommand(GetComponent<CommandType>().type);
            }

            // Undo transparency
            setTransparency(1.0f);
            dragging = false;

            
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Queue")) {
            queueArea = other.gameObject;
            isOverQueueArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Queue")) {
            isOverQueueArea = false;
        }
    }

    private void setTransparency(float value) {
        Color baseC = GetComponent<SpriteRenderer>().color;
        baseC.a = value;
        GetComponent<SpriteRenderer>().color = baseC;
        
    }

}
