using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class PaletteGenerator : MonoBehaviour
{
    public Texture2D sourceTexture;
    public int paletteSize = 16;

    private void Start()
    {
        List<Color> palette = GeneratePalette(sourceTexture, paletteSize);
        SavePaletteToFile(palette, "Assets/palette.txt");
    }

    private List<Color> GeneratePalette(Texture2D texture, int paletteSize)
    {
        Color[] pixels = texture.GetPixels();
        Dictionary<Color, int> colorCounts = new Dictionary<Color, int>();

        foreach (Color color in pixels)
        {
            if (colorCounts.ContainsKey(color))
            {
                colorCounts[color]++;
            }
            else
            {
                colorCounts[color] = 1;
            }
        }

        List<KeyValuePair<Color, int>> sortedColors = new List<KeyValuePair<Color, int>>(colorCounts);
        sortedColors.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

        List<Color> palette = new List<Color>();

        for (int i = 0; i < Mathf.Min(paletteSize, sortedColors.Count); i++)
        {
            palette.Add(sortedColors[i].Key);
        }

        return palette;
    }

    private void SavePaletteToFile(List<Color> palette, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (Color color in palette)
            {
                writer.WriteLine(ColorUtility.ToHtmlStringRGBA(color));
            }
        }

        Debug.Log("Palette saved to " + filePath);
    }
}