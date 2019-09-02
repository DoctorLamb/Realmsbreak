using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    #region Generation Variables
    //Public Generation Variables
    public int width = 8;
    public int height = 8;

    Tile[] tiles;
    //Private Generation Variables
    #endregion

    #region Objects
    //Public Object Variables
    public Tile tilePrefab;
    public Text labelPrefab;

    //Private Object Variables
    Canvas b_Canvas;
    BoardMesh b_Mesh;
    #endregion

    public Color wColor = Color.white;
    public Color gColor = Color.grey;

    void Awake()
    {
        //Initialize board
        tiles = new Tile[height * width];
        b_Canvas = GetComponentInChildren<Canvas>();
        b_Mesh = GetComponentInChildren<BoardMesh>();

        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateTile(x, z, i++);
            }
        }
    }

    void Start() {
        b_Mesh.Triangulate(tiles);
    }

    public void EditTile(Vector3 position, Color color)
    {
        position = transform.InverseTransformPoint(position);
        TileCoordinates coordinates = TileCoordinates.FromPosition(position);
        int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
        Tile t = tiles[index];
        t.color = color;
        b_Mesh.Triangulate(tiles);
    }

    void CreateTile(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (Metrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (Metrics.outerRadius * 1.5f);

        Tile t = tiles[i] = Instantiate<Tile>(tilePrefab);
        t.transform.SetParent(transform, false);
        t.transform.localPosition = position;
        t.coordinates = TileCoordinates.FromOffset(x, z);
        if (i % 2 == 0)
        {
            t.color = wColor;
        }
        else {
            t.color = gColor;
        }

        Text label = Instantiate<Text>(labelPrefab);
        label.rectTransform.SetParent(b_Canvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        label.text = t.coordinates.ToStringSeparateLines();
    }
}
