using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PieceManager : MonoBehaviour
{
    
    public float lastRight;
    public float lastLeft;
    public float lastRotation;
    public static bool placedFirstBlock;
    public List<Vector2> positions = new List<Vector2>();
    Vector2 currentPos = new Vector2(0, 8);
    float moveTime = 1f;
    public Tilemap playField;
    public RuleTile wall;
    public RuleTile wallSelected;
    float lastFall;
    Vector2 lastPos = new Vector2(-50, -50);
    public List<Vector2> positionsBeforeDeletion = new List<Vector2>();
    public GameObject dec;

    void Start()
    {
        if(Time.time <= 0.5f)
        {
            placedFirstBlock = false;
        }
        if (!isInValidPos())
        {
            Debug.Log("GAME OVER");
            dec.GetComponent<Decorator>().gameOver = true;
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
        Debug.Log(Time.time - lastLeft);
        if (Input.GetKey(KeyCode.LeftArrow) && Time.time - lastLeft >= 0.2f)
        {
            lastLeft = Time.time;
            Debug.Log(0);
            currentPos += new Vector2(-1, 0);

            if (isInValidPos())
                UpdateTilemap();
            else
                currentPos += new Vector2(1, 0);
            
        }
        else if (Input.GetKey(KeyCode.RightArrow) && Time.time - lastRight >= 0.2f)
        {
            lastRight = Time.time;
            currentPos -= new Vector2(-1, 0);

            if (isInValidPos())
                UpdateTilemap();
            else
                currentPos -= new Vector2(1, 0);
            
        }
        else if (Input.GetKey(KeyCode.UpArrow) && Time.time - lastRotation >= 0.2f)
        {
            lastRotation = Time.time; 
            for (int i = 0; i < positions.Count; i++)
            {
                positionsBeforeDeletion[i] = positions[i];
            }
            for (int i = 0; i < positions.Count; i++)
            {
                positions[i] = new Vector2(positions[i].y, -positions[i].x);
            }

            if (isInValidPos()){

                for (int i = 0; i < positions.Count; i++)
                {
                    playField.SetTile(new Vector3Int(((int)lastPos.x + (int)positionsBeforeDeletion[i].x), (int)lastPos.y + (int)positionsBeforeDeletion[i].y, 0), null);
                }
                lastPos = currentPos;
                for (int i = 0; i < positions.Count; i++)
                {
                    playField.SetTile(new Vector3Int(((int)currentPos.x + (int)positions[i].x), (int)currentPos.y + (int)positions[i].y, 0), wallSelected);
                }
            }    
            else
            {
                for (int i = 0; i < positions.Count; i++)
                {
                    positions[i] = positionsBeforeDeletion[i];
                }
            }
               
                   
        }
        else if ((Input.GetKey(KeyCode.DownArrow) && Time.time - lastFall >= 0.1f) ||
                 Time.time - lastFall >= moveTime)
        {
            currentPos += new Vector2(0, -1);

            if (isInValidPos())
            {
                UpdateTilemap();
            }
            else
            {
                currentPos += new Vector2(0, 1);

                for (int i = 0; i < positions.Count; i++)
                {
                    playField.SetTile(new Vector3Int(((int)currentPos.x + (int)positions[i].x), (int)currentPos.y + (int)positions[i].y, 0), wall);
                }
                placedFirstBlock = true;
                //if(!dec.GetComponent<Decorator>().gameOver)
                FindObjectOfType<Spawner>().spawnNext();

                enabled = false;
            }

            lastFall = Time.time;
        }
        else if(Input.GetKeyDown(KeyCode.Return) && placedFirstBlock)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                playField.SetTile(new Vector3Int(((int)lastPos.x + (int)positions[i].x), (int)lastPos.y + (int)positions[i].y, 0), null);
            }
            dec.GetComponent<Decorator>().gameOver = true;
            Destroy(gameObject);
        }
    }

    public bool isInValidPos()
    {

        for (int i = 0; i < positions.Count; i++)
        {
            if(!GridManager.isInsideBorder(currentPos + positions[i]))
            {
                return false;
            }

            if(playField.GetTile(new Vector3Int(((int)currentPos.x + (int)positions[i].x), ((int)currentPos.y + (int)positions[i].y), 0)) != null
               && playField.GetTile(new Vector3Int(((int)currentPos.x + (int)positions[i].x), ((int)currentPos.y + (int)positions[i].y), 0)) != wallSelected)
            {

                return false;
            }
        }

        return true;

    }

    void moveDown()
    {
        for (int i = 0; i < positions.Count; i++)
        {
            playField.SetTile(new Vector3Int(((int)currentPos.x + (int)positions[i].x), (int)currentPos.y + (int)positions[i].y, 0), null);
        }
        currentPos.y--;
        for (int i = 0; i < positions.Count; i++)
        {
            playField.SetTile(new Vector3Int(((int)currentPos.x + (int)positions[i].x), (int)currentPos.y + (int)positions[i].y, 0), wallSelected);
        }


    }

    void UpdateTilemap()
    {
        for (int i = 0; i < positions.Count; i++)
        {
            playField.SetTile(new Vector3Int(((int)lastPos.x + (int)positions[i].x), (int)lastPos.y + (int)positions[i].y, 0), null);
        }
        lastPos = currentPos;
        for (int i = 0; i < positions.Count; i++)
        {
            playField.SetTile(new Vector3Int(((int)currentPos.x + (int)positions[i].x), (int)currentPos.y + (int)positions[i].y, 0), wallSelected);
        }

    }

    bool CollidingWithSibling(int i)
    {


        for (int j = 0; j < positions.Count; j++)
        {
            if (lastPos + positions[i] == currentPos + positions[j])
            {
                return true;
            }    
        }

        return false;
    }
}
