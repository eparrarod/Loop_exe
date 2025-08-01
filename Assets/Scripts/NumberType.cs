using UnityEngine;
using TMPro;

public class NumberType : CommandType{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private int number;

    void Start(){
        base.Start();
        number = 1;
        type = "" + number;
        
    }

    // Update is called once per frame
    void Update(){
        // Right mouse button
        if (Input.GetMouseButtonDown(1)) {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject) {
                cycleNumber();
            }
        }
    }

    private void cycleNumber() {
        number = (number + 1) % 10;
        type = "" + number;
        TMP_Text textLabel = GetComponentInChildren<TMP_Text>();
        textLabel.text = type;
    }
}
