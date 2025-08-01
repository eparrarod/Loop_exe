using UnityEngine;
using TMPro;

public class CommandType : MonoBehaviour{

    public string type;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start(){
        TMP_Text textLabel = GetComponentInChildren<TMP_Text>();
        textLabel.text = type;
    }

    // Update is called once per frame
    void Update(){
        
    }
}
