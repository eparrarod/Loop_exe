using UnityEngine;
using System.Collections.Generic;

public class RobotMovement : MonoBehaviour{

    public Vector2Int position;
    private int rows;
    private int cols;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    public Vector2 getPosition() {
        return position;
    }

    public void initBounds(int r, int c, Vector2Int pos) {
        rows = r;
        cols = c;
        setPosition(pos);
    }

    public void setPosition(Vector2Int pos) {
        position = pos;
    }



    private bool moveVertical(int dir) {
        bool valid = false;

        Debug.Log($"OldCoords {position}");
        Debug.Log($"conds [{position.y < rows - 1} , {position.y > 0}]");
        if (position.y < rows - 1 && position.y > 0){
            position.y = position.y + dir;
            valid = true;
        }
        Debug.Log($"NewCoords {position}");
        return valid;
    }

    private void moveHorizontal(int dir) {
        Debug.Log($"OldCoords {position}");
        Debug.Log($"conds [{position.y < cols - 1} , {position.y > 0}]");

        if (position.x < cols - 1 && position.x > 0) {
            position.x = position.x + dir;
        }
        Debug.Log($"NewCoords {position}");
    }

    public void moveLeft() {
        moveHorizontal(-1);
    }

    public void moveRight() {
        moveHorizontal(1);
    }

    public bool moveUp() {
       return moveVertical(1);
    }

    public bool moveDown() {
        return moveVertical(-1);
    }

    public void moveDiagonal(int dir) {
        // 0 = NE  1 = SE  2 = SW  3 = NW

        switch (dir) {
            case 0: //NE
                if (position.y < rows - 1 && position.x > 0) {
                    position.x = position.x - 1;
                    position.y = position.y + 1;
                }
                break;
            case 1: //SE
                if (position.x < cols - 1 && position.y > 0) {
                    position.x = position.x + 1;
                    position.y = position.y - 1;
                }
                break;
            case 2: //SW
                if (position.y > 0 && position.x > 0) {
                    position.x = position.x - 1;
                    position.y = position.y - 1;
                }
                break;
            case 3: //NW
                if (position.y < rows - 1 && position.x > 0) {
                    position.x = position.x - 1;
                    position.y = position.y + 1;
                }
                break;
        }

    }
}
