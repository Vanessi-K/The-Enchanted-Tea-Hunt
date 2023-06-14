using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mocking : MonoBehaviour
{
    [SerializeField] private string[] mockingText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int randomValue = UnityEngine.Random.Range(0, 10);
            
            //Adjust how often the owl is mocking the player
            if (randomValue < 4)
            {
                int randomMockingText = UnityEngine.Random.Range(0, mockingText.Length);
                Debug.Log(mockingText[randomMockingText]);
            }
        }
    }
}
