using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap16 : MonoBehaviour
{
    //en este scrips pueden ir los nemigos, los objetos , el tamaño, etc
    Mesh mesh;
    MeshRenderer meshRenderer;
    MeshFilter meshFilter;

    List<Vector3> vertices;
    List<int> triangles;
    List<Vector2> uv;
    List<Color> colors;

    Color[] colorsArray = {  Color.white, Color.red, Color.green, Color.cyan, Color.yellow };

    int triangleCounter = 0;

    [SerializeField] Material material;

    const float TILE_SIZE = 0.0625f;
    Vector2 offset = Vector2.zero;

    VoidFunc3Int[] RotateUVs = { };

    void Awake()
    {
        InitMesh();
        ClearMesh();
        UpdateMesh();
    }

    public void ClearMesh()
    {
        mesh.Clear();
        vertices.Clear();
        uv.Clear();
        triangles.Clear();
        colors.Clear();

        triangleCounter = 0;
    }
    public void AddTile (Vector3 v, int id = 0, int idColor = 0) //agrgar mas datos
    {
        AddTile(v.x, v.y, id, idColor);
    }
    public void AddTile(float x, float y, int id = 0, int idColor = 0)
    {
        Vector3 pos = Vector3.zero;
        pos.x = x;
        pos.y = y;

        vertices.Add(Vector3.zero + pos);
        vertices.Add(Vector3.up + pos);
        vertices.Add(Vector3.right + pos);
        vertices.Add(Vector3.right + Vector3.up + pos);

        colors.Add(colorsArray[idColor]);
        colors.Add(colorsArray[idColor]);
        colors.Add(colorsArray[idColor]);
        colors.Add(colorsArray[idColor]);
        colors.Add(colorsArray[idColor]);

        triangles.AddRange(new int[]
        {
            0 + triangleCounter,
            1 + triangleCounter,
            2 + triangleCounter

        });

        triangles.AddRange(new int[]
        {
            2 + triangleCounter,
            1 + triangleCounter,
            3 + triangleCounter
        });
        triangleCounter += 4;

        offset = Vector2.zero;

        offset.x += id * TILE_SIZE;
        uv.Add(Vector2.zero + offset); //0
        uv.Add(Vector2.up + offset);//1
        uv.Add(Vector2.right * TILE_SIZE + offset); //2
        uv.Add(Vector2.right * TILE_SIZE + Vector2.up + offset);

    }
    


}
