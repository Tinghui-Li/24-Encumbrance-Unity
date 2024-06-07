using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class RandomSceneManager
{
    // the number of levels of weight
    public static int scenarios = 3;

    public static List<int> GenerateRandomList(int length)
    {
        List<int> randomList = new List<int>();

        for (int i = 0; i < scenarios; i++)
        {
            List<int> tempList = new List<int>();

            while (tempList.Count < length)
            {
                int nextRandom = Random.Range(1, 5); // Generates a number from 1 to 4 inclusive

                if (!tempList.Contains(nextRandom))
                {
                    tempList.Add(nextRandom);
                }
            }

            randomList.AddRange(tempList);
            randomList.Add(5);
        }

        return randomList;
    }

    public static List<int> sceneList = GenerateRandomList(4);
    public static int currentIndex { get; set; }

    public static int getSceneNum()
    {
        return sceneList[currentIndex];
    }
}
