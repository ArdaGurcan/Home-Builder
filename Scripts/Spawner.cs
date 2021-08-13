using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawner : MonoBehaviour
{
    public Tilemap playField;
    public RuleTile wall;
    public RuleTile roof;
    public Piece[] pieces;
    public GameObject pieceManager;

    public void spawnNext()
    {
        int r = Random.Range(0, pieces.Length);

        for (int i = 0; i < pieces[r].positions.Length; i++)
        {
            //playField.SetTile(new Vector3Int((int)pieces[r].positions[i].x, (int)pieces[r].positions[i].y + 9, 0), wall);
        }
        GameObject manager = Instantiate(pieceManager);
        manager.SetActive(true);
        for (int i = 0; i < pieces[r].positions.Length; i++)
        {
            Debug.Log(i);
            manager.GetComponent<PieceManager>().positions.Add(new Vector2(pieces[r].positions[i].x, pieces[r].positions[i].y));
            Debug.Log(i);
        }
    }

	private void Start()
	{
        //spawnNext();
	}
}
