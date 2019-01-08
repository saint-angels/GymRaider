using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] TextMeshProUGUI fatigueMeter;
    [SerializeField] GameObject testRedPrefab;
    [SerializeField] float fatiguePerRep = 5f;
    [SerializeField] int damage = 10;

    private bool aimStarted = false;
    private int pressedNumpadNum1;

    private float fatigue;

    public float Fatigue
    {
        get
        {
            return fatigue;
        }
        set
        {
            fatigue = value;
            fatigueMeter.text = fatigue.ToString("n2") + "%";
        }
    }
	
	void Update ()
    {
        //Shooting
        if (Input.GetKeyDown(KeyCode.Keypad1)) Aim(0);
        if (Input.GetKeyDown(KeyCode.Keypad2)) Aim(1);
        if (Input.GetKeyDown(KeyCode.Keypad3)) Aim(2);
        if (Input.GetKeyDown(KeyCode.Keypad4)) Aim(3);
        if (Input.GetKeyDown(KeyCode.Keypad5)) Aim(4);
        if (Input.GetKeyDown(KeyCode.Keypad6)) Aim(5);
        if (Input.GetKeyDown(KeyCode.Keypad7)) Aim(6);
        if (Input.GetKeyDown(KeyCode.Keypad8)) Aim(7);
        if (Input.GetKeyDown(KeyCode.Keypad9)) Aim(8);
    }

    private void Aim(int tileIndex)
    {
        ShootAoe(tileIndex);
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
        Vector2 shootPosition = Board.Instance.BoardMap[numpadNumber1 - 1, numpadNumber2 - 1];
        var prefabInstance = Instantiate(testRedPrefab, shootPosition, Quaternion.identity);
        GameObject.Destroy(prefabInstance, .1f);
    }

    private void ShootAoe(int tileIndex)
    {
        for (int i = 0; i < 9; i++)
        {
            Vector2 shootPosition = Board.Instance.BoardMap[tileIndex, i];
            var prefabInstance = Instantiate(testRedPrefab, shootPosition, Quaternion.identity);
            GameObject.Destroy(prefabInstance, .05f);
        }

        var possibleRep = ExerciseGenerator.Instance.SpawnedReps[tileIndex];
        if (possibleRep != null)
        {
            CameraShake.Instance.Shake(.3f);
            //Add fatigue
            Fatigue += Random.Range(0f, fatiguePerRep);

            possibleRep.Damage(damage);
        }
    }
}
