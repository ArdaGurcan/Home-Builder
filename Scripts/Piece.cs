using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Piece", menuName = "Piece")]
public class Piece : ScriptableObject
{
    public string blockName;
    public int numberOfBlocks;
    public Vector2[] positions;
    public Color blockColor;







}
