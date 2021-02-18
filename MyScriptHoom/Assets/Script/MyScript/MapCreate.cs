using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MapCreate : MonoBehaviour
{
    public int width = 30;
    public int height = 18;
    public float Radius = 0.7f;
    public float scale = 5.0f;

    [Header("RandomParamerter")]
    public int seed = 0;
    public float BasePerlin = 1;
    public float BaseAffect = 1;
    public float MountenPerlin = 1;
    public float MountenAffect = 1;
    public float SlantPerlin = 1;
    public float SlantAffect = 1;
    

    const float TriangleHeight = 0.86660254f;
    const float TriangleHeightDouble = 1.7320508f;

    Mesh mesh;
    PerlinNoise perlin;
    MeshCollider mc;

    void Start()
    {
        System.Random r = new System.Random();
        seed = r.Next();
        makeGround();
    }

    [ContextMenu("生成")]
    void makeGround()
    {
        mc = GetComponent<MeshCollider>();

        mesh = new Mesh();
        int p;

        mesh.Clear();

        perlin = new PerlinNoise(seed);

        var vertices = new Vector3[((width + 1) * 2 + 1) * (height + 1) + width + 1];
        var uv = new Vector2[((width + 1) * 2 + 1) * (height + 1) + width + 1];
        var triangles = new int[(width * 2 + 1) * (height * 2 + 2) * 3];

        // メッシュ作成
        // 初段
        for (p = 0; p <= width; p++)
        {
            vertices[p].x = p * scale;
            vertices[p].z = 0f;
            vertices[p].y = HightPerlin(vertices[p].x, 0, vertices[p].z);
            
            uv[p].x = (float)p / width;
            uv[p].y = 0f;
        }
        for (int i = 0; i <= height; i++)
        {
            // 左端処理
            vertices[p].x = 0f;
            vertices[p].z = i * TriangleHeightDouble * scale + TriangleHeight * scale;
            vertices[p].y = HightPerlin(vertices[p].x,0,vertices[p].z);

            uv[p].x = 0f;
            uv[p].y = (i * TriangleHeightDouble + TriangleHeight) / (TriangleHeightDouble * height);
            p++;

            for (int j = 0; (j <= width - 1); j++)
            {
                vertices[p].x = j * scale + 0.5f * scale;
                vertices[p].z = i * TriangleHeightDouble * scale + TriangleHeight * scale;
                vertices[p].y = HightPerlin(vertices[p].x,0, vertices[p].z)*scale;

                uv[p].x = ((float)j + 0.5f) / width;
                uv[p].y = (i * TriangleHeightDouble + TriangleHeight) / (TriangleHeightDouble * height);

                p++;
            }

            // 右端処理
            vertices[p].x = width * scale;
            vertices[p].z = i * TriangleHeightDouble * scale + TriangleHeight * scale;
            vertices[p].y = HightPerlin(vertices[p].x,0, vertices[p].z)*scale;

            uv[p].x = 1f;
            uv[p].y = (i * TriangleHeightDouble + TriangleHeight) / (TriangleHeightDouble * height);

            p++;

            // 縦列はワンループで2つの三角形がペアです。
            for (int j = 0; j <= width; j++)
            {
                vertices[p].x = j * scale;
                vertices[p].z = (i + 1f) * TriangleHeightDouble * scale;
                vertices[p].y = HightPerlin(vertices[p].x,0, vertices[p].z)*scale;

                uv[p].x = (float)j / width;
                uv[p].y = (i + 1f) / height;

                p++;
            }
        }

        p = 0;
        // メッシュ順作成
        for (int i = 0; i <= height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                // 三角形4個を一組にして定義していきます。
                triangles[p + 0] = j + (((width + 1) * 2 + 1) * i);
                triangles[p + 1] = j + (width + 1) * (i * 2 + 1) + i;
                triangles[p + 2] = j + (width + 1) * (i * 2 + 1) + i + 1;

                triangles[p + 3] = j + (((width + 1) * 2 + 1) * i);  // 
                triangles[p + 4] = j + (width + 1) * (i * 2 + 1) + i + 1;
                triangles[p + 5] = j + (((width + 1) * 2 + 1) * i) + 1;

                triangles[p + 6] = j + (width + 1) * (i * 2 + 1) + i;
                triangles[p + 7] = j + (((width + 1) * 2 + 1) * (i + 1));
                triangles[p + 8] = j + (width + 1) * (i * 2 + 1) + i + 1;

                triangles[p + 9] = j + (width + 1) * (i * 2 + 1) + i + 1;
                triangles[p + 10] = j + (((width + 1) * 2 + 1) * (i + 1));
                triangles[p + 11] = j + (((width + 1) * 2 + 1) * (i + 1)) + 1;

                p += 12;
            }
            // 右恥と、左端処理
            triangles[p + 0] = width + (((width + 1) * 2 + 1) * i);
            triangles[p + 1] = width + (width + 1) * (i * 2 + 1) + i;
            triangles[p + 2] = width + (width + 1) * (i * 2 + 1) + i + 1;

            triangles[p + 3] = width + (width + 1) * (i * 2 + 1) + i;
            triangles[p + 4] = width + (((width + 1) * 2 + 1) * (i + 1));
            triangles[p + 5] = width + (width + 1) * (i * 2 + 1) + i + 1;

            p += 6;
        }
        
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        mc.sharedMesh = mesh;

        mesh.RecalculateNormals();
        var filter = GetComponent<MeshFilter>();
        filter.sharedMesh = mesh;
    }
    float HightPerlin(float x, float y, float z)
    {
        Vector2 MeshPos = new Vector2(x, z);
        perlin = new PerlinNoise(seed);
        float hy = perlin.OctavePerlin(x * BasePerlin, 0, z * BasePerlin) * BaseAffect;

        Vector2 center = new Vector2(width / (2 + Radius) * scale, width / (2 + Radius) * scale);
        float Rp = (width / (2 + Radius) * scale - Vector2.Distance(center, MeshPos)) / (width / (2 + Radius)*scale);
        hy += perlin.OctavePerlin(x * MountenPerlin, 0, z * MountenPerlin) * MountenAffect * Rp;

        if (hy < 0.15f * MountenAffect)
        {
            hy *= 0.7f;
        }

        hy += perlin.OctavePerlin(x * SlantPerlin, 0, z * SlantPerlin) * SlantAffect;

        return hy;
    }
}
