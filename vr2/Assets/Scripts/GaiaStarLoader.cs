using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaiaStarLoader : MonoBehaviour
{

    public TextAsset[] starDataSets; // The CSV file should be attached here in the Inspector
    public GameObject starPrefab; // Your star prefab
    public float universeScale = 100000f;

    void Start()
    {
        foreach(var item in starDataSets)
        {
            // Read the CSV file
            string[] data = item.text.Split(new char[] { '\n' });
            // Scale to make the universe larger
            float minStarSize = 0.05f; // Minimum size for dim stars

            // Loop through the data (skipping the header)
            for (int i = 1; i < data.Length - 1; i++)
            {
                string[] row = data[i].Split(','); // Split each line by commas

                float ra = float.Parse(row[4]); // Right Ascension
                float dec = float.Parse(row[6]); // Declination
                float parallax = float.Parse(row[7]); // Parallax
                float magnitude = float.Parse(row[6]); // Magnitude

                // Avoid division by very small parallax values
                float distance = parallax > 0.001f ? 1.0f / parallax : 100000f;

                // Calculate the position in Cartesian coordinates
                Vector3 position = CalculatePosition(ra, dec, distance) * universeScale;

                // Instantiate the star at the calculated position
                GameObject star = Instantiate(starPrefab, position, Quaternion.identity, this.transform);

                // Scale the star based on magnitude, ensuring a minimum size
                float scaledSize = Mathf.Max(minStarSize, (magnitude / 10)); // Prevent stars from getting too small
                star.transform.localScale = Vector3.one * scaledSize;
            }

        }
       
    }

    Vector3 CalculatePosition(float ra, float dec, float distance)
    {
        // Convert RA and DEC to radians
        float raRad = ra * Mathf.Deg2Rad;
        float decRad = dec * Mathf.Deg2Rad;

        // Perform the spherical to Cartesian conversion
        float x = distance * Mathf.Cos(decRad) * Mathf.Cos(raRad);
        float y = distance * Mathf.Cos(decRad) * Mathf.Sin(raRad);
        float z = distance * Mathf.Sin(decRad);

        return new Vector3(x, y, z);
    }
}
