using UnityEngine;

public class RobotMovement : MonoBehaviour{

    private Vector2 position;
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

    public void initBounds(int r, int c, Vector2 pos) {
        rows = r - 1;
        cols = c - 1;
        setPosition(pos);
    }

    public void setPosition(Vector2 pos) {
        position = pos;
    }



    private void moveVertical(int dir) {
        if (position.y < rows && position.y > 0){
            position.y = position.y + dir;
        }
    }

    private void moveHorizontal(int dir) {
        if (position.x < cols && position.x > 0) {
            position.x = position.x + dir;
        }
    }

    public void moveLeft() {
        moveHorizontal(-1);

    }

    public void moveRight() {
        moveHorizontal(1);
    }

    public void moveUp() {
        moveVertical(-1);
    }

    public void moveDown() {
        moveVertical(1);
    }

    public void moveDiagonal(int dir) {
        // 0 = NE  1 = SE  2 = SW  3 = NW

        switch (dir) {
            case 0: //NE
                if (position.y < rows && position.x > 0) {
                    position.x = position.x - 1;
                    position.y = position.y + 1;
                }
                break;
            case 1: //SE
                if (position.x < cols && position.y > 0) {
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
                if (position.y < rows && position.x > 0) {
                    position.x = position.x - 1;
                    position.y = position.y + 1;
                }
                break;
        }

    }


}
