using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGenerator : MonoBehaviour {

    public int width, length;
    public int widthOffset, lengthOffset;
    public int widthDelta, lengthDelta;

    public ColorType colorType;

    [Range(1, 25)]
    public int samples;
    [Range(8, 2048)]
    public int sampleSize;

    [Range(0, 2)]
    public float noiseScale;
    [Range(1, 20)]
    public int noiseFactor;

    private void OnDrawGizmos()
    {
        Color[,] colorMap = GetColorMap();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                Gizmos.color = colorMap[x, y];
                Gizmos.DrawCube(new Vector3(x, 0, y), Vector3.one);
            }
        }
    }

    public float[,] GetNoiseMap()
    {
        float[,] noiseMap = new float[width, length];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                float xcoord = Mathf.Lerp(0, 1, Mathf.Abs((float)((x + widthOffset) % sampleSize) / (sampleSize)));
                float ycoord = Mathf.Lerp(0, 1, Mathf.Abs((float)((y + lengthOffset) % sampleSize) / (sampleSize)));

                float noiseValue = 0;

                for (int i = 0; i < samples; i++)
                {
                    noiseValue += Mathf.Pow(Mathf.PerlinNoise(xcoord * (widthDelta * (i + 1)), ycoord * (lengthDelta * (i + 1))) * noiseScale, noiseFactor + i);
                }

                noiseMap[x, y] = noiseValue;
            }
        }

        return noiseMap;
    }

    public Color[,] GetColorMap()
    {
        Color[,] colorMap = new Color[width, length];
        float[,] noiseMap = GetNoiseMap();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                colorMap[x, y] = GetColorValue(noiseMap[x, y]);
            }
        }

        return colorMap;
    }

    public Color GetColorValue(float noiseValue)
    {
        Color color;

        switch (colorType)
        {
            case ColorType.RedYellowGreen:
                {
                    color = Color.Lerp(Color.red, Color.green, noiseValue);
                    break;
                }
            case ColorType.RedBlue:
                {
                    color = Color.Lerp(Color.red, Color.blue, noiseValue);
                    break;
                }
            case ColorType.Spectrum:
                {
                    color = Color.HSVToRGB(noiseValue, 1, 1);
                    break;
                }
            case ColorType.GrayScale:
            default:
                {
                    color = new Color(255, 255, 255, 1 - noiseValue);
                    break;
                }
        }

        return color;
    }

    public enum ColorType
    {
        GrayScale,
        RedYellowGreen,
        RedBlue,
        Spectrum
    }

}
