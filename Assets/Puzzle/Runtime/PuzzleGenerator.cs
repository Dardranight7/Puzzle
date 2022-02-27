using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleGenerator : MonoBehaviour
{
    [SerializeField] Sprite sourceImage;
    [SerializeField] Canvas destCanvas;
    [SerializeField] List<Sprite> slicedSprites;
    [SerializeField] List<Sprite> jointSprites;
    [SerializeField] List<List<int>> slicedParts = new List<List<int>>();
    Dictionary<string, bool> plugs = new Dictionary<string, bool>();

    [ContextMenu("SplitPuzzle")]
    public void SplitPuzzle()
    {
        int cutCount = 4;
        int newTextureSize = sourceImage.texture.width / cutCount;
        slicedParts = CreateSplitArray(cutCount);
        for (int i = 0; i < slicedParts.Count; i++)
        {
            for (int ii = 0; ii < slicedParts[i].Count; ii++)
            {
                Texture2D newTexture = new Texture2D(newTextureSize, newTextureSize);
                Color[] colorArray = sourceImage.texture.GetPixels(ii * newTextureSize, i * newTextureSize, newTextureSize, newTextureSize);
                newTexture.SetPixels(colorArray);
                newTexture.Apply();
                Sprite ResultSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0, 0));
                GameObject newGameObject = new GameObject("PuzzlePart");
                Image newImage = newGameObject.AddComponent<Image>();
                newImage.sprite = ResultSprite;
                newGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(newTextureSize, newTextureSize);
                newImage.transform.SetParent(destCanvas.transform);
                newImage.gameObject.transform.position = new Vector3(ii * newTextureSize, i * newTextureSize, 0);
                slicedSprites.Add(ResultSprite);
            }
        }
    }

    public List<List<int>> CreateSplitArray(float sliceCount)
    {
        List<List<int>> parts = new List<List<int>>();
        int globalIndex = 0;
        for (int i = 0; i < sliceCount; i++)
        {
            List<int> part = new List<int>();
            for (int ii = 0; ii < sliceCount; ii++)
            {
                part.Add(ii + globalIndex);
                globalIndex++;
            }
            parts.Add(part);
        }
        for (int i = 0; i < parts.Count; i++)
        {
            for (int ii = 0; ii < parts[i].Count; ii++)
            {
                //Up part
                if (ii - 1 > 0)
                {
                    if (!plugs.ContainsKey(PlugCode(parts[i][ii], parts[i][ii - 1])))
                    {
                        int number = Random.Range(0, 2);
                        plugs.Add(parts[i][ii] + "" + parts[i][ii - 1], 1 - number == 1);
                        plugs.Add(parts[i][ii - 1] + "" + parts[i][ii], number == 1);
                    }
                }

                //Down part
                if (ii + 1 < parts[i].Count)
                {
                    if (!plugs.ContainsKey(PlugCode(parts[i][ii], parts[i][ii + 1])))
                    {
                        int number = Random.Range(0, 2);
                        plugs.Add(parts[i][ii] + "" + parts[i][ii + 1], 1 - number == 1);
                        plugs.Add(parts[i][ii + 1] + "" + parts[i][ii], number == 1);
                    }
                }

                //Left part
                if (i - 1 > 0)
                {
                    if (!plugs.ContainsKey(PlugCode(parts[i][ii], parts[i - 1][ii])))
                    {
                        int number = Random.Range(0, 2);
                        plugs.Add(parts[i][ii] + "" + parts[i - 1][ii], 1 - number == 1);
                        plugs.Add(parts[i - 1][ii] + "" + parts[i][ii], number == 1);
                    }
                }

                //Right part
                if (i + 1 < parts.Count)
                {
                    if (!plugs.ContainsKey(PlugCode(parts[i][ii], parts[i + 1][ii])))
                    {
                        int number = Random.Range(0, 2);
                        plugs.Add(parts[i][ii] + "" + parts[i + 1][ii], 1 - number == 1);
                        plugs.Add(parts[i + 1][ii] + "" + parts[i][ii], number == 1);
                    }
                }
            }
        }
        return parts;
    }

    public string PlugCode(int a, int b)
    {
        string outString = "";
        if (a > b)
        {
            outString += b + a;
        }
        else
        {
            outString += a + b;
        }

        return outString;
    }

    void Start()
    {
        SplitPuzzle();
    }
}
