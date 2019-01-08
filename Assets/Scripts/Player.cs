using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] GameObject testRedPrefab;

    private Vector2[,] aimMap = new Vector2[9, 9];
    private bool aimStarted = false;
    private int pressedNumpadNum1;

    void Start ()
    {
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
                        print(string.Format("{0}:{1}", bigTileIndex, smallTileIndex));
                        aimMap[bigTileIndex, smallTileIndex] = new Vector2(smallTileX, smallTileY);
                        smallTileIndex++;
                    }
                }
                bigTileIndex++;
            }
        }
    }
	
	void Update ()
    {
        //Shooting
        if (Input.GetKeyDown(KeyCode.Keypad1)) Aim(1);
        if (Input.GetKeyDown(KeyCode.Keypad2)) Aim(2);
        if (Input.GetKeyDown(KeyCode.Keypad3)) Aim(3);
        if (Input.GetKeyDown(KeyCode.Keypad4)) Aim(4);
        if (Input.GetKeyDown(KeyCode.Keypad5)) Aim(5);
        if (Input.GetKeyDown(KeyCode.Keypad6)) Aim(6);
        if (Input.GetKeyDown(KeyCode.Keypad7)) Aim(7);
        if (Input.GetKeyDown(KeyCode.Keypad8)) Aim(8);
        if (Input.GetKeyDown(KeyCode.Keypad9)) Aim(9);
    }

    private void Aim(int numpadNumber)
    {
        ShootAoe(numpadNumber);
        return;
        //if (aimStarted)
        //{
        //    PrecisionShoot(pressedNumpadNum1, numpadNumber);
        //    aimStarted = false;
        //}
        //else
        //{
        //    aimStarted = true;
        //    pressedNumpadNum1 = numpadNumber;
        //}
    }

    private void PrecisionShoot(int numpadNumber1, int numpadNumber2)
    {
        Vector2 shootPosition = aimMap[numpadNumber1 - 1, numpadNumber2 - 1];
        var prefabInstance = Instantiate(testRedPrefab, shootPosition, Quaternion.identity);
        GameObject.Destroy(prefabInstance, .1f);
    }

    private void ShootAoe(int numpadNumber)
    {
        int tileIndex = numpadNumber - 1;
        for (int i = 0; i < 9; i++)
        {
            Vector2 shootPosition = aimMap[tileIndex, i];
            var prefabInstance = Instantiate(testRedPrefab, shootPosition, Quaternion.identity);
            GameObject.Destroy(prefabInstance, .5f);
        }
    }
}
