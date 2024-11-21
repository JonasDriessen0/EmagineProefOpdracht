using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text.RegularExpressions;

public class CSVReader : MonoBehaviour
{
    [SerializeField] private TextAsset dataFile;
    private List<List<string>> datalists;

    private void Awake()
    {
        ReadCSV();
    }

    void ReadCSV()
    {
        datalists = new List<List<string>>();
        var lines = dataFile.text.Split('\n');
        var columns = 0;
        string pattern = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        
        for(int i = 0; i < lines.Length; i++)
        {
            string[] data = Regex.Split(lines[i], pattern);
            var list = new List<string>(data);
            datalists.Add(list);
            columns = Mathf.Max(columns, data.Length);
        }
        
        Debug.Log(datalists.First()[0]);
    }

    public string GetCell(int row, int column)
    {
        if (datalists == null)
        {
            Debug.LogError("CSV data not loaded!");
            return "";
        }
        
        if (row < 0 || row >= datalists.Count)
        {
            Debug.LogError($"Row {row} is out of bounds. Total rows: {datalists.Count}");
            return "";
        }
        
        if (column < 0 || column >= datalists[row].Count)
        {
            Debug.LogError($"Column {column} is out of bounds for row {row}. Columns in this row: {datalists[row].Count}");
            return "";
        }
        
        return datalists[row][column].Replace("\"", "");
    }
    
    public int GetRowCount()
    {
        return datalists?.Count ?? 0;
    }
}
