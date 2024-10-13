using UnityEngine;

public enum TileType
{
    Start,
    OS,
    CN,
    DBMS,
    Programming,
    Jail,
    CommunityChest,
    Chance,

}

public enum Difficulty
{
    Easy,
    Medium,
    Hard,
}

public class Tile : MonoBehaviour
{
    public TileType tileType;
    public string tileName;
    public Color propertyColor; 
    public Difficulty difficulty;
}
