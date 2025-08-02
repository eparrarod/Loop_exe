using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GridManager : MonoBehaviour{

    public static GridManager singleton;

    [SerializeField]
    public GameObject[] tiles; //list of tiles

    [SerializeField]
    public char[] chars; // list of chars

    public Dictionary<char, GameObject> tileMap; //Mapping from char to tile

    public char[,] mapEncoded; // Level as a char matrix

    [SerializeField]
    public GameObject pivotPoint; // Background of tile grid area
    Vector3 pivot; // pivot for calculating Tile positions

    [SerializeField]
    public GameObject[] characters; // robots

    public GameObject robot;

    public int rows;
    public int columns;
    public float xSpacing = 0.0f;
    public float ySpacing = 0.0f;

    public GameObject[,] levelGrid;

    // Scoring

    public int targetCount;
    public int targets;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        if (singleton == null) {
            singleton = this;
        }
        targetCount = 0;
        targets = 0 ;
        createGrid();
    }

    // Update is called once per frame
    void Update(){
        //Debug.Log("update");
    }

    // Create Grid
    void createGrid() {

        tileMap = new Dictionary<char, GameObject>();
        for (int i = 0; i < tiles.Length; i++) {
            tileMap.Add(chars[i], tiles[i]);
        }

        //Hardcoded Level
        mapEncoded = new char[,] { { 'G', '.', '.', '#', '.', '.', 'G' },
    { '.', '.', '.', '#', '.', '.', '.' },
    { '.', '.', '.', '#', '.', '.', '.' },
    { '#', '#', '#', 'R', '#', '#', '#' },
    { '.', '.', '.', '#', '.', '.', '.' },
    { '.', '.', '.', '#', '.', '.', '.' },
    { 'G', '.', '.', '#', '.', '.', 'G' } };

        rows = mapEncoded.GetLength(0);
        columns = mapEncoded.GetLength(1);

        // Get size of tile prefab
        Vector2 dimensions = tiles[0].GetComponent<SpriteRenderer>().size;
        float scale = tiles[0].GetComponent<Transform>().localScale.x;
        Debug.Log($"Prefab size: {dimensions}");
        xSpacing = (dimensions[0] - 0.02f) * scale;
        ySpacing = (dimensions[1] - 0.02f) * scale;

        levelGrid = new GameObject[rows, columns];

        // Instantiate all the dice in the UI
        for (int y = 0; y < rows; y++) {
            for (int x = 0; x < columns; x++) {
                pivot = pivotPoint.transform.position;

                float totalw = rows * xSpacing;
                float totalh = rows * ySpacing;

                // compute space available

                //Vector2 sizeBgk = pivotPoint.GetComponent<SpriteRenderer>().size;
                //float scaleBkg = pivotPoint.GetComponent<Transform>().localScale.x;
                //float widthBgk = sizeBgk.x * scaleBkg;
                //float heightBgk = sizeBgk.y * scaleBkg;

                //float horizontalSpace = widthBgk - totalw;
                //float verticalSpace = heightBgk - totalh;

                //float scaleHor = (totalw + horizontalSpace) / totalw;
                //float scaleVer = (totalh + verticalSpace) / totalh;

                //scale = Mathf.Min(scaleVer/2, scaleHor/2);

                //xSpacing = xSpacing * scale;
                //ySpacing = ySpacing * scale;


                // center within the center of the background of the GridArea
                pivot[0] = pivot[0] - (totalw / 2);
                pivot[1] = pivot[1] - (totalh / 2);

                Vector3 position = pivot + new Vector3(x * xSpacing, y * ySpacing, 0);
                Debug.Log($"[{x},{y}] -> {position}");

                GameObject currentTile;

                switch (mapEncoded[x, y]) {
                    case 'G':
                        currentTile = Instantiate(tiles[1], position, Quaternion.identity, transform);
                        targets++;
                        break;
                    case '#':
                        currentTile = Instantiate(tiles[2], position, Quaternion.identity, transform);
                        break;
                    case 'R':
                        StartCoroutine(CreateRobot(position));
                        currentTile = Instantiate(tiles[0], position, Quaternion.identity, transform);
                        robot.GetComponent<RobotMovement>().initBounds(rows, columns, new Vector2Int(x, y));
                        break;
                    default:
                        currentTile = Instantiate(tiles[0], position, Quaternion.identity, transform);
                        break;
                }

                levelGrid[x, y] = currentTile;
            }
        }
    }

    IEnumerator CreateRobot(Vector3 pos) {
        robot = Instantiate(characters[0], pos, Quaternion.identity, transform);

        yield return new WaitUntil(() => robot.GetComponent<Transform>() != null);

        // Now the object is ready
        Debug.Log("Robot is fully initialized!");
    }

    public void execute(List<string> commands) {

        RobotMovement movement = robot.GetComponent<RobotMovement>();
        int repeatNextCommand = 1;
        foreach (string s in commands) {
            string[] pieces = s.Split(" ");
            

            if (pieces.Length == 3) { // repeat with number
                repeatNextCommand = int.Parse(pieces[1]);
            } else if (pieces.Length == 2) { // number only
                // Todo: Robot eyes blink that amount of times

            } else { // A direction or a repeat without number
                for (int i = 0 ; i < repeatNextCommand; i++) {

                    Debug.Log($"-> {s} }} {i+1}/{repeatNextCommand}");

                    //Get Current robot
                    //levelGrid[movement.position.x, movement.position.y];
                    Vector3 oldPos = new Vector3(movement.position.x * xSpacing, movement.position.y * ySpacing, 0);

                    Debug.Log($"moveO index {movement.position}");

                    switch (s) {
                        case "moveUp();":
                            Debug.Log($"up");
                            movement.moveUp();
                            break;
                        case "moveNE();":
                            movement.moveDiagonal(0);
                            break;
                        case "moveRight();":
                            movement.moveRight();
                            break;
                        case "moveSE();":
                            movement.moveDiagonal(1);
                            break;
                        case "moveDown();":
                            Debug.Log($"down");
                            movement.moveDown();
                            break;
                        case "moveSW();":
                            movement.moveDiagonal(2);
                            break;
                        case "moveLeft();":
                            movement.moveLeft();
                            break;
                        case "moveNW();":
                            movement.moveDiagonal(3);
                            break;
                        case "Repeat" :
                            break;
                    }

                    //Debug.Log($"old pos {oldPos}");
                    //Debug.Log($"RT {robot.transform.position}");
                    Debug.Log($"moveN index {movement.position}");
                    Vector3 newPos = pivot + new Vector3(movement.position.x * xSpacing, movement.position.y * ySpacing, 0);;
                    //Debug.Log($"new pos {newPos}");
                    robot.transform.position = newPos;
                    //Debug.Log($"RTAf {robot.transform.position}");
                }
            }
        }
    }
}


