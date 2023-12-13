using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    [SerializeField] private float amplitudeMin;
    [SerializeField] private float amplitude;
    [SerializeField] private float frequency;
    [SerializeField] private int oitavas;

    private float[,] heights;
    private int width, height;
    private Terrain terrain;

    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<Terrain>();
        width = terrain.terrainData.heightmapResolution;
        height = terrain.terrainData.heightmapResolution;
        Debug.Log(width + ", " + height);
        heights = new float[width, height];

        oitavas = Random.Range(1, 5);

        switch(oitavas)
        {
            case 2:  //? gelo
            break;
            case 3:  //? montanhas ou floresta
            break;
            case 4:  // deserto com dunas
            amplitude = 0.15f;
            amplitudeMin = -0.15f;
            // terrain texture
            // terrain.terrainData.splatPrototypes[0].texture = Resources.Load<Texture2D>("Textures/Desert");
            break;
            default:
            break;
        }
        for(int i = 0; i < oitavas; i++)
            PerlinNoise(i);

        terrain.terrainData.SetHeights(0, 0, heights);
    }

    void PerlinNoise(int octave)
    {
        float x;
        float y;
        float frequencyAux = frequency + Mathf.Pow(2, octave);
        for(int i = 0; i < width; i++)
            for(int j = 0; j < height; j++)
            {
                x = (float)i / width * frequencyAux;
                y = (float)j / height * frequencyAux;
                heights[i, j] = amplitude * Mathf.PerlinNoise(x, y);
            }
    }

}
