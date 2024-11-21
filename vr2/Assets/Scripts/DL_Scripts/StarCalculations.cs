using UnityEngine;
using System;

public class StarCalculations : MonoBehaviour
{
    // Example
    double distanceParsecs = 685.400959561343;
    double apparentMagnitude = 7.99137782950583f; // Apparent Magnitude is phot_g_mean_mag from GAIA

    private void Start()
    {
        OutputToThing();
    }

    double CalculateMass(double distanceParsecs, double apparentMagnitude)
    {
        // Calculate Star's Absolute Magnitude using M = m-5*(log10(d)-1)
        double absoluteMagnitude = apparentMagnitude - 5 * (Math.Log10(distanceParsecs) - 1);

        // Luminosity
        // Variables
        const double LUMINOSTIY_OF_SUN = 3.828e26;
        const double STELLAR_MASS = 1.989e30;
        const double SUN_ABSOLUTE_MAGNITUDE = 4.83;

        // Luminosity Calculation
        double luminosity = LUMINOSTIY_OF_SUN * Math.Pow(10, (SUN_ABSOLUTE_MAGNITUDE - absoluteMagnitude) / 2.5); // Calculate luminosity

        //Calculate Mass
        double mass = STELLAR_MASS * Math.Pow((luminosity / LUMINOSTIY_OF_SUN), (1.0 / 3.5));

        return mass;
    }



    double CalculateRadius(double distanceParsecs, double apparentMagnitude)
    {
        const double STELLAR_MASS = 1.989e30;
        double mass = CalculateMass(distanceParsecs, apparentMagnitude);
        double radius;

        if (mass >= STELLAR_MASS)
        {
            radius = Math.Pow(mass / STELLAR_MASS, 0.57) * 6.955e8;
            return radius;
        }
        else // if (mass < stellarMass)
        {
            radius = Math.Pow(mass / STELLAR_MASS, 0.8) * 6.955e8;
            return radius;
        }
    }

    double CalculateDiameterInKM(double distanceParsecs, double apparentMagnitude)
    {
        double diameter = CalculateRadius(distanceParsecs, apparentMagnitude) * 2 / 1000; // In Km
        return diameter;
    }

    double CalculateGravity(double distanceParsecs, double apparentMagnitude)
    {
        // Needed Parameters
        const double GRAVITATIONAL_CONSTANT = 6.674e-11;
        double mass = CalculateMass(distanceParsecs, apparentMagnitude);
        double radius = CalculateRadius(distanceParsecs, apparentMagnitude);

        // Calculate Gravity
        double gravity = (GRAVITATIONAL_CONSTANT * mass) / (radius * radius);

        return gravity;
    }

    void OutputToThing()
    {
        Debug.Log("Mass: " + (CalculateMass(distanceParsecs, apparentMagnitude)).ToString() + " kg");
        Debug.Log("Gravity: " + (CalculateGravity(distanceParsecs, apparentMagnitude)).ToString() + " m^3/kg*s^2");
        Debug.Log("Diameter: " + (CalculateDiameterInKM(distanceParsecs, apparentMagnitude)).ToString() + " km");
    }
}
