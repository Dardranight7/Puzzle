using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleGenerator : MonoBehaviour
{
    [SerializeField] Sprite sourceImage;
    [SerializeField] Transform destParent;
    [SerializeField] List<Sprite> slicedSprites;
    [SerializeField] List<Sprite> jointSprites;
    [SerializeField] List<List<int>> slicedParts = new List<List<int>>();
    [SerializeField] List<PuzzlePart> plugs = new List<PuzzlePart>();

    [System.Serializable]
    class PuzzlePart
    {
        public PuzzlePart(int index)
        {
            this.index = index;
        }
        int index = 0;
        public List<int> up = new List<int>();
        public List<int> down = new List<int>();
        public List<int> left = new List<int>();
        public List<int> right = new List<int>();
    }

    [ContextMenu("SplitPuzzle")]
    public void SplitPuzzle()
    {
        int cutCount = 5;
        int newTextureSize = sourceImage.texture.width / cutCount;
        slicedParts = CreateSplitArray(cutCount);
        for (int i = 0; i < slicedParts.Count; i++)
        {
            for (int ii = 0; ii < slicedParts[i].Count; ii++)
            {
                Texture2D newTexture = new Texture2D(newTextureSize, newTextureSize);
                Color[] colorArray = sourceImage.texture.GetPixels(ii * newTextureSize, i * newTextureSize, newTexture.width, newTexture.height);
                newTexture.SetPixels(colorArray);
                newTexture.Apply();
                Sprite ResultSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0, 0));
                GameObject newGameObject = new GameObject("PuzzlePart");
                Image newImage = newGameObject.AddComponent<Image>();
                Debug.Log(i + ii);
                newImage.sprite = ResultSprite;
                newGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(newTexture.width, newTexture.height);
                newImage.transform.SetParent(destParent);
                newImage.gameObject.transform.position = new Vector3(ii * newTextureSize, i * newTextureSize, 0);
                slicedSprites.Add(ResultSprite);
            }
        }
    }

    public List<List<int>> CreateSplitArray(float sliceCount)
    {
        List<List<int>> parts = new List<List<int>>();
        for (int i = 0; i < sliceCount; i++)
        {
            List<int> part = new List<int>();
            for (int ii = 0; ii < sliceCount; ii++)
            {
                part.Add(ii + i);
            }
            parts.Add(part);
        }
        for (int i = 0; i < parts.Count; i++)
        {
            for (int ii = 0; ii < parts[i].Count; ii++)
            {
                PuzzlePart newPuzzlePart = new PuzzlePart(i+ii);
                //Down part
                if (i - 1 >= 0)
                {
                    newPuzzlePart.down.Add(i - 1);
                }

                //Up part
                if (i + 1 < parts.Count)
                {
                    newPuzzlePart.up.Add(i + 1);
                }

                //Left part
                if (ii - 1 >= 0)
                {
                    newPuzzlePart.left.Add(ii - 1);
                }

                //Right part
                if (ii + 1 < parts[i].Count)
                {
                    newPuzzlePart.right.Add(ii + 1);
                }
                plugs.Add(newPuzzlePart);
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
}
