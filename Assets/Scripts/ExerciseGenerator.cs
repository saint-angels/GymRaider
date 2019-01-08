using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExerciseGenerator : SingletonComponent<ExerciseGenerator>
{
    [SerializeField] private Repetition repPrefabTest;

    public Repetition[] SpawnedReps { get; private set; }

    public List<Exercise> availableExercises = new List<Exercise>();

    void Start()
    {
        //Read all the exercises

        TextAsset textData = Resources.Load("Exercises") as TextAsset;
        print(textData.text);



        SpawnedReps = new Repetition[9];

        Exercise testEx = new Exercise(
                ExerciseType.DEADLIFT,
                new Rep[]
                {
                    new Rep (1, .5f),
                    new Rep (2, .6f),
                    new Rep (3, .7f),
                    new Rep (4, .8f),
                }
            );

        Exercise testEx2 = new Exercise(
                ExerciseType.DEADLIFT,
                new Rep[]
                {
                    new Rep (7, 0f),
                    new Rep (5, .1f),
                    new Rep (3, .4f),
                }
            );

        availableExercises.Add(testEx);
        availableExercises.Add(testEx2);


        StartCoroutine(SpawnAvailableExercises());
    }

    IEnumerator SpawnAvailableExercises()
    {
        while(true)
        {
            var randomEx = availableExercises[UnityEngine.Random.Range(0, availableExercises.Count)];
            StartCoroutine(SpawnExercise(randomEx));
            float exerciseDuration = randomEx.Duration();
            yield return new WaitForSeconds(exerciseDuration);
        }
    }

    IEnumerator SpawnExercise(Exercise exercise)
    {
        for (int repIndex = 0; repIndex < exercise.reps.Length; repIndex++)
        {
            var rep = exercise.reps[repIndex];

            yield return new WaitForSeconds(rep.startDelay);

            Vector2 spawnPosition = Board.Instance.BoardMap[rep.tileIndex - 1, 4];
            var repInstance = Instantiate(repPrefabTest, spawnPosition, Quaternion.identity) as Repetition;
            repInstance.Init(rep.lifeDuration);
            SpawnedReps[rep.tileIndex - 1] = repInstance;
            repInstance.OnDeath += OnRepDestroyed;
        }
    }

    void Update()
    {
        
    }

    void OnRepDestroyed(Repetition rep)
    {
        if (rep == null)
        {
            Debug.LogError("Rep destroyed outside of exercise generator!");
        }

        //Is it needed?

        //for (int i = 0; i < spawnedReps.Length; i++)
        //{
        //    if (spawnedReps[i] == rep)
        //    {
        //        spawnedReps[i] = null;
        //    }
        //}        

        Destroy(rep.gameObject);
    }
}

public struct Rep
{
    public float startDelay;
    public float lifeDuration;
    public int tileIndex;

    public Rep(int tileIndex, float startDelay, float lifeDuration = 5f)
    {
        this.startDelay = startDelay;
        this.tileIndex = tileIndex;
        this.lifeDuration = lifeDuration;
    }
}

public struct Exercise
{
    public ExerciseType type;
    public Rep[] reps;

    public Exercise(ExerciseType type, Rep[] reps)
    {
        this.type = type;
        this.reps = reps;
    }

    public float Duration()
    {
        float duration = 0;
        foreach (var rep in reps)
        {
            float repMaxTime = rep.startDelay + rep.lifeDuration;
            if (repMaxTime >= duration)
            {
                duration = repMaxTime;
            }
        }
        return duration;
    }
}

public enum ExerciseType
{
    PULLUP,
    SQUAT,
    DEADLIFT,
    TIME,
    NONE
}