using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour{

    [SerializeField]
    public GameObject[] tiles; //list of tiles

    [SerializeField]
    public char[] chars; // list of chars

    public Dictionary<char, GameObject> tileMap; //Mapping from char to tile

    public char[,] mapEncoded; // Level as a char matrix

    [SerializeField]
    public GameObject pivotPoint; // Background of tile grid area

    [SerializeField]
    public GameObject[] characters; // robots

    public GameObject character;

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
                Vector3 pivot = pivotPoint.transform.position;

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
                Debug.Log(position);

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
                        character = Instantiate(characters[0], position, Quaternion.identity, transform);
                        currentTile = Instantiate(tiles[0], position, Quaternion.identity, transform);
                        break;
                    default:
                        currentTile = Instantiate(tiles[0], position, Quaternion.identity, transform);
                        break;
                }

                levelGrid[x, y] = currentTile;
            }
        }
    }

}


