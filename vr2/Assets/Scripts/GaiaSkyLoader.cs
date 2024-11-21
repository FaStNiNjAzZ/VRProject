using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class GaiaSkyLoader : MonoBehaviour
{
    [Header("Star Data CSV File")]
    public TextAsset[] starDataSets; // The CSV files containing star data

    [System.Serializable]
    public class Star
    {
        public string name;
        public float ra;         // Right Ascension in degrees
        public float dec;        // Declination in degrees
        public float dist;       // Distance in light-years
        public float pmra;       // Proper Motion in RA
        public float pmdec;      // Proper Motion in DEC
        public float rv;         // Radial velocity

        // Convert RA, DEC, and Distance to Cartesian coordinates
        public Vector3 GetPosition()
        {
            float raRad = ra * Mathf.Deg2Rad;   // Convert RA to radians
            float decRad = dec * Mathf.Deg2Rad; // Convert DEC to radians

            // Divide distance by 100 for scaling down
            float scaledDist = dist / 100.0f;

            float x = scaledDist * Mathf.Cos(decRad) * Mathf.Cos(raRad);
            float y = scaledDist * Mathf.Cos(decRad) * Mathf.Sin(raRad);
            float z = scaledDist * Mathf.Sin(decRad);

            return new Vector3(x, y, z);
        }
    }

    [Header("Star Prefab")]
    public GameObject starPrefab; // Assign a prefab for each star

    void Start()
    {
        foreach (var item in starDataSets)
        {
            // Parse the CSV data
            string[] dataLines = item.text.Split('\n');

            // Start loop at 1 to skip the header line
            for (int i = 1; i < dataLines.Length; i++)
            {
                string[] values = dataLines[i].Split(',');

                if (values.Length < 7) continue; // Ensure there are enough values in the line

                // Parse each field from the CSV line
                Star star = new Star
                {
                    name = values[0],
                    ra = float.Parse(values[1]),
                    dec = float.Parse(values[2]),
                    dist = float.Parse(values[3]),
                    pmra = float.Parse(values[4]),
                    pmdec = float.Parse(values[5]),
                    rv = float.Parse(values[6])
                };

                // Calculate the star's position and instantiate it
                Vector3 starPosition = star.GetPosition();
                GameObject newStar = Instantiate(starPrefab, starPosition, Quaternion.identity, this.transform);

                // Optionally, set the star's name or other attributes
                newStar.name = star.name;
            }
        }
    }
}
