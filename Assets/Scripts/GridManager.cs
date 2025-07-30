using UnityEngine;

public class GridManager : MonoBehaviour{

    [SerializeField]
    public GameObject[] tiles;

    public GameObject[,] grid;
    [SerializeField]
    public GameObject pivotPoint;
    public int rows;
    public int columns;
    public float xSpacing = 0.0f;
    public float ySpacing = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        createGrid();
    }

    // Update is called once per frame
    void Update(){
        //Debug.Log("update");
    }

    // Create Grid
    void createGrid() {
        // Get size of tile prefab
        Vector2 dimensions = tiles[0].GetComponent<SpriteRenderer>().size;
        float scale = tiles[0].GetComponent<Transform>().localScale.x;
        Debug.Log($"Prefab size: {dimensions}");
        xSpacing = dimensions[0] * scale;
        ySpacing = dimensions[1] * scale;

        grid = new GameObject[rows, columns];

        // Instantiate all the dice in the UI
        for (int y = 0; y < rows; y++) {
            for (int x = 0; x < columns; x++) {
                Vector3 pivot = pivotPoint.transform.position;

                float totalw = rows * xSpacing;
                float totalh = rows * xSpacing;

                pivot[0] = pivot[0] - (totalw / 2);
                pivot[1] = pivot[1] - (totalh / 2);

                Vector3 position = pivot + new Vector3(x * xSpacing, y * ySpacing, 0);
                Debug.Log(position);
                grid[x, y] = Instantiate(tiles[0], position, Quaternion.identity, transform);
            }
        }
    }

}


