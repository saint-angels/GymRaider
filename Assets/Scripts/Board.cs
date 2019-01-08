using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : SingletonComponent<Board>
{
    public Vector2[,] BoardMap { get; private set; }


    void Start()
    {
        BoardMap = new Vector2[9, 9];

        //Generate aim map
        float bigTileSize = 3f;
        float smallTileSize = 1f;

        int bigTileIndex = 0;
        for (int yBig = 0; yBig < 3; yBig++)
        {
            for (int xBig = 0; xBig < 3; xBig++)
            {
                float bigTileX = (xBig % 3) * bigTileSize;
                float bigTileY = (yBig % 3) * bigTileSize;
                //print(new Vector2(bigTileX, bigTileY));
                int smallTileIndex = 0;
                for (int ySmall = 0; ySmall < 3; ySmall++)
                {
                    for (int xSmall = 0; xSmall < 3; xSmall++)
                    {
                        float smallTileX = LevelGenerator.Instance.MinX + bigTileX + (xSmall % 3) * smallTileSize;
                        float smallTileY = LevelGenerator.Instance.MinY + bigTileY + (ySmall % 3) * smallTileSize;
                        //aimMap[3 * xBig + xSmall, 3 * yBig + ySmall] = new Vector2(smallTileX, smallTileY);
                        //Instantiate(testRedPrefab, new Vector2(smallTileX, smallTileY), Quaternion.identity);
                        //print(string.Format("{0}:{1}", bigTileIndex, smallTileIndex));
                        BoardMap[bigTileIndex, smallTileIndex] = new Vector2(smallTileX, smallTileY);
                        smallTileIndex++;
                    }
                }
                bigTileIndex++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
