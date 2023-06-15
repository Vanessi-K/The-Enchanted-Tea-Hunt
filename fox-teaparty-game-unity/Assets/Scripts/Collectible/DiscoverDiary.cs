using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoverDiary : MonoBehaviour
{
    [SerializeField] private GameObject diary;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            diary.SetActive(true);
        }
    }
}
