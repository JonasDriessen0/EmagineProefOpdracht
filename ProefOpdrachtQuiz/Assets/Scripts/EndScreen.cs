using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private CSVReader csvReader;

    private void Update()
    {
        print(csvReader.GetRowCount());
    }
}
