using UnityEngine;
using TMPro;

public class NumberType : CommandType{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int number = 1;

    void Start(){
        base.type = "" + number;
        base.Start();
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
        number = ((number) % 9 )+ 1;
        base.type = "" + number;
        TMP_Text textLabel = GetComponentInChildren<TMP_Text>();
        textLabel.text = type;
    }

    public void setNum(int num) {
        number = num;
    }
}
