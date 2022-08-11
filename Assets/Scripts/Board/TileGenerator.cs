using Sirenix.OdinInspector;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    #region Attributes
    [Header("Spawn Prefabs")]
    [SerializeField]
    [Required]
    private GameObject tilePrefab;
    #endregion


    #region Properties
    #endregion

    private Board _board;
    private Board board => _board ? _board : _board = GetComponent<Board>();


    #region Unity Methods
    #endregion


    #region Custom Methods
    public BoardTile[,] SpawnTiles()
    {
        BoardTile[,] tiles = new BoardTile[Board.SIZE,Board.SIZE];

        for (int y = 0; y < Board.SIZE; y++)
        {
            for (int x = 0; x < Board.SIZE; x++)
            {
                Vector3 position = new Vector3(x * board.TileSize, 0, -y * board.TileSize);

                GameObject tileObject = Instantiate(tilePrefab, position, Quaternion.identity, board.TileTransform);
                BoardTile tile = tileObject.GetComponent<BoardTile>();

                tile.Init(new Coordinates(x, y));

                tiles[y, x] = tile;
            }
        }

        return tiles;
    }
    #endregion
}
