using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ExeBox : MonoBehaviour {

    [SerializeField]
    public static ExeBox singleton;

    [SerializeField]
    public List<GameObject> commands;

    [SerializeField]
    public List<string> commandsRaw;

    private Dictionary<string, string> commandMap;

    public GameObject baseLine;
    float heightPerLine;

    //
    private bool closed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        if (singleton == null) {
            singleton = this;
        }

        commandMap = new Dictionary<string, string>();
        commandMap.Add("Repeat", "Repeat");
        commandMap.Add("N", "moveUp();");
        commandMap.Add("NE", "moveNE();");
        commandMap.Add("E", "moveRight();");
        commandMap.Add("SE", "moveSE();");
        commandMap.Add("S", "moveDown();");
        commandMap.Add("SW", "moveSW();");
        commandMap.Add("W", "moveLeft();");
        commandMap.Add("NW", "moveNW();");

        //Determine height of line for commands
        SpriteRenderer spriteRenderer = baseLine.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) {
            Vector2 size = spriteRenderer.bounds.size;
            heightPerLine = size.y;
        }
    }

    // Update is called once per frame
    void Update() {
    }

    public void addCommand(string type) {
        if (commandsRaw.Count < 14) {
            if (closed) {
                Destroy(commands[commands.Count - 1]);
                closed = false;
            }
            string commandFull = commandMap.GetValueOrDefault(type, $"{type} time");
            // Add Number to the right of repeat
            bool number = int.TryParse(type, out int result);
            if (number && commandsRaw.Count > 0) {
                if (result > 1) {
                    Debug.Log(">1");
                    commandFull = $"{commandFull}s";
                }
                string lastCommand = commandsRaw[commandsRaw.Count - 1];
                if (lastCommand.Equals("Repeat")) {
                    commandFull = $"Repeat {commandFull}";
                    commandsRaw.RemoveAt(commandsRaw.Count - 1);
                    GameObject lastCommandLine = commands[commands.Count - 1];
                    commands.RemoveAt(commands.Count - 1);
                    lastCommandLine.GetComponentInChildren<TMP_Text>().text = commandFull;
                    commands.Add(lastCommandLine);
                } else {// The last command is not a repeat command
                    number = false;
                }
            } else { //A number but empty queue
                number = false;
            }

            if (!number) {
                addCommandBox(commandFull);
            }
            commandsRaw.Add(commandFull);
        }
    }

    public void closeLoop() {
        closed = true;
        addCommandBox("}");
    }

    public void addCommandBox(string text) {
        Vector3 position = baseLine.transform.position;
        position.y = position.y - (heightPerLine * (commandsRaw.Count + 1));
        GameObject newCommandLine = Instantiate(baseLine, transform);
        newCommandLine.transform.position = position;
        newCommandLine.GetComponentInChildren<TMP_Text>().text = text;
        commands.Add(newCommandLine);
    }

    public void clearQueue() {
        commandsRaw = new List<string>();
        foreach (GameObject line in commands) {
            Destroy(line);
        }
        commands = new List<GameObject>();
    }

}