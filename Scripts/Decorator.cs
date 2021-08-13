using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;


public class Decorator : MonoBehaviour
{
    //for (int x = -11; x< 11; x++)
    //{
    //    for (int y = -4; y< 8; y++)
    //    {

    //    }
    //}
    public GameObject cam;

    public Tilemap house;
    public Tilemap decoration;
    public Tilemap environment;

    public Tile[] relax;
    public Tile[] fun;
    public Tile[] love;
    public Tile relaxTile;
    public Tile funTile;
    public Tile loveTile;
    public RuleTile wall;
    public RuleTile roof;

    public int relaxValue;
    public int funValue;
    public int loveValue;
    public int totalvalue;
    public int value;


    public Text loveText;
    public Text funText;
    public Text relaxText;

    public bool gameOver;

    public GameObject pieceManager;

    public GameObject spawner;
    public GameObject popup;
    public GameObject wave1;
    public GameObject wave2;

    void Start()
    {
        
    }

    void Update()
    {
        //Debug.Log()
        if(gameOver)
        {
            cam.GetComponent<Animation>().Play("ZoomOut");
            Decorate();
            //magnifier.SetActive(true);
        }
    }

    public void Decorate()
    {
        //23*13

        gameOver = false;
        //relaxValue = Random.Range(0, 100);

        //loveValue = Random.Range(0, 100);

        //funValue = Random.Range(0, 100);
        totalvalue = funValue + loveValue + relaxValue;

        for (int x = -11; x < 11; x++)
        {
            for (int y = -4; y < 16; y++)
            {
                if (house.GetTile(new Vector3Int(x, y - 1, 0)) == null && house.GetTile(new Vector3Int(x, y, 0)) == wall && Random.Range(0,3) >=1) 
                {
                    value = Random.Range(0, totalvalue);

                    if(value < relaxValue)
                    {
                        decoration.SetTile(new Vector3Int(x, y, 0), relax[(int)Random.Range(0, relax.Length)]);
                    }
                    else if (value < relaxValue + loveValue)
                    {
                        decoration.SetTile(new Vector3Int(x, y, 0), love[(int)Random.Range(0, love.Length)]);


                    }
                    else if (value < totalvalue)
                    {
                        decoration.SetTile(new Vector3Int(x, y, 0), fun[(int)Random.Range(0, fun.Length)]);

                    }
                }
            }
        }

        PlaceRoof();
    }

    public void PlaceRoof()
    {
        //relaxValue = Random.Range(0, 100);
        //relaxText.text = "" + relaxValue;
        //loveValue = Random.Range(0, 100);
        //loveText.text = "" + loveValue;
        //funValue = Random.Range(0, 100);
        //funText.text = "" + funValue;
        for (int x = -11; x< 11; x++)
        {
            for (int y = -4; y< 16; y++)
            {
                bool place = true;
                if(house.GetTile(new Vector3Int(x, y - 1, 0)) == wall && house.GetTile(new Vector3Int(x, y, 0)) == null)
                {
                    
                    for (int i = y; i < 16; i++)
                    {
                        //environment.SetTile(new Vector3Int(x, i, 0), roof);
                        if(house.GetTile(new Vector3Int(x, i , 0)) != null)
                        {
                            place = false;
                            break;
                        }
                    }
                    if(place)
                    {
                        house.SetTile(new Vector3Int(x, y, 0), roof);

                    }

                }
            }
        }

        CalculateScore();
    }
    public void CalculateScore()
    {
        float total = 0;
        float loveCount = 0;
        float relaxCount = 0;
        float funCount = 0;
        float tileCount = 0;

        for (int x = -11; x< 11; x++)
        {
            for (int y = -4; y< 16; y++)
            {
                if(house.GetTile(new Vector3Int(x, y, 0)) == wall)
                {
                    tileCount++;
                }
                for (int i = 0; i < love.Length; i++)
                {
                    if(decoration.GetTile(new Vector3Int(x, y, 0)) == love[i])
                    {
                        loveCount++;
                        total++;
                        break;
                    }
                }
                for (int i = 0; i < fun.Length; i++)
                {
                    if (decoration.GetTile(new Vector3Int(x, y, 0)) == fun[i])
                    {
                        funCount++;
                        total++;
                        break;
                    }
                }
                for (int i = 0; i < relax.Length; i++)
                {
                    if (decoration.GetTile(new Vector3Int(x, y, 0)) == relax[i])
                    {
                        relaxCount++;
                        total++;
                        break;
                    }
                }


            }
        }
        //Debug.Log("love: " + loveCount);
        //Debug.Log("fun: " + funCount);
        //Debug.Log("relax: " + relaxCount);
        //Debug.Log("total: " + total);
        //loveText.color = new Color(1 - loveCount / total, loveCount / total, 0);
        //funText.color = new Color(1 - funCount / total, funCount / total, 0);
        //relaxText.color = new Color(1 - relaxCount / total, relaxCount / total, 0);

        loveText.text = "" + (int)(tileCount * 5 * loveCount / total);
        funText.text = "" + (int)(tileCount * 5 * funCount / total);
        relaxText.text = "" + (int)(tileCount * 5 * relaxCount / total);
    }

    private IEnumerator WaitForAnimation()
    {
        //cam.GetComponent<Animation>().Play("ZoomIn");
        yield return new WaitForSeconds(0.000001f);
        Application.LoadLevel(Application.loadedLevel);

    }

    public void Restart()
    {
        StartCoroutine("WaitForAnimation");
    }

    public void SetRelax(float value)
    {
        relaxValue = (int)value;
    }

    public void SetLove(float value)
    {
        loveValue = (int)value;
    }

    public void SetFun(float value)
    {
        funValue = (int)value;
    }
    public void StartGame()
    {
        PieceManager.placedFirstBlock = false;
        if(funValue + loveValue + relaxValue > 0)
        {
            wave1.SetActive(false);
            wave2.SetActive(true);
            StartCoroutine("StartGame2");
        }

    }
    IEnumerator StartGame2()
    {
        yield return new WaitForSeconds(3);
        popup.SetActive(false);
        spawner.GetComponent<Spawner>().spawnNext();

    }
}
