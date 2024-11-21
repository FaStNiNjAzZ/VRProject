using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCStarLoader : MonoBehaviour
{
    public TextAsset starDataSet; // The CSV files should be attached here in the Inspector
    public GameObject starPrefab; // Your star prefab
    public float universeScale = 1f; // Scale to make the universe larger

    public List<GameObject> stars;  

    void Start()
    {
        // Read the CSV file
        string[] data = starDataSet.text.Split(new char[] { '\n' });

        // Loop through the data (skipping the header)
        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(','); // Split each line by commas

            string starType = row[0].Trim();
            string starName = row[1].Trim(); // Adjust index for star name
            float distance = ParseFloat(row[4].Trim(), i, "Distance");
            float starX = ParseFloat(row[5].Trim(), i, "X");
            float starY = ParseFloat(row[6].Trim(), i, "Y");
            float starZ = ParseFloat(row[7].Trim(), i, "Z");
            float radius = ParseFloat(row[8].Trim(), i, "Radius");
            string starAge = row[9].Trim(); // Example: might be used for additional properties

            // Create a new position vector for the star
            Vector3 position = new Vector3(starX, starZ, starY);

            // Instantiate the star at the calculated position
            GameObject star = Instantiate(starPrefab, position, Quaternion.identity, this.transform);
            stars.Add(star);
            star.name = starName; // Optional: Name the star object for easy identification
            star.GetComponent<StarGameObject>().SetValues(starAge, radius, starType, distance);
            star.GetComponent<StarGameObject>().particleGameobject.transform.localScale = new Vector3(radius, radius, radius);
            star.transform.localScale = new Vector3(radius, radius, radius);
        }
    }

    private float ParseFloat(string value, int rowIndex, string valueType)
    {
        try
        {
            return float.Parse(value);
        }
        catch (FormatException)
        {
           // Debug.LogError($"FormatException at row {rowIndex}: Unable to parse {valueType} value '{value}'.");
            return 0f; // Return a default value or handle it as needed
        }
    }
}
