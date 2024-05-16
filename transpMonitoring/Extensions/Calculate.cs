using VehicleMonitoring.Domain.Entities;

namespace VehicleMonitoring.mvc.Extensions
{
    public static class Calculate
    {
        /// <summary>
        /// Calculates the percentage difference between the initial and final number
        /// </summary>
        /// <param name="initialValue"></param>
        /// <param name="finalValue"></param>
        /// <returns>Percentage number from 0 to 100</returns>
        private static double PercentageDifference(double initialValue, double finalValue)
        {
            return (finalValue - initialValue) / initialValue * 100;
        }
        /// <summary>
        /// Calculates the deviation of the sensor readings from the norms set by the maxFuelConsumption and lowerValue parameters
        /// </summary>
        /// <param name="upperValue">The upper value of the norm</param>
        /// <param name="lowerValue">The lower value of the norm</param>
        /// <param name="actualValue">The actual value</param>
        /// <returns>Returns a string with the percentage deviation of the reading from the norm</returns>
        public static string? StandartSensorCalc(double upperValue,double lowerValue,double actualValue)
        {
            if (upperValue < actualValue)
            {
                return ("Показания выше нормы на {.00}%" ,PercentageDifference(upperValue, actualValue)).ToString();  
            }
            if (lowerValue > actualValue)
            {
                return ("Показания ниже нормы на {.00}%", PercentageDifference(lowerValue, actualValue)*-1).ToString();
            }
            return null;

        }

        /// <summary>
        /// Speeding check
        /// </summary>
        /// <param name="maxSpeed">maximum permissible speed</param>
        /// <param name="actualSpeed">Vehicle speed</param>
        /// <returns>Returns a string with the numerical value of the excess or null if there was no excess</returns>
        public static string? VelocitySensorCalc(double maxSpeed, double actualSpeed)
        {

            if (maxSpeed < actualSpeed)
            {
                return ("Превышение скорости  на {.0}% км/ч", actualSpeed-maxSpeed).ToString();
            }
            return null;
        }

        /// <summary>
        /// Calculates possible fuel drains based on the average fuel level for the last readings and permissible deviations
        /// </summary>
        /// <param name="sensorValues">List of fuel sensor readings</param>
        /// <param name="maxFuelConsumption">Maximum allowable fuel consumption per measurement</param>
        /// <param name="windowSize">The size of the time window for calculating the average fuel level in the tank (must match the number of incoming readings)</param>
        /// <returns>Returns a message indicating whether a fuel drain was noticed, if there was no drain, returns null</returns>
        public static string? FuelLevelCalc(List<SensorValue> sensorValues,double maxFuelConsumption,bool ignition, int windowSize= 5)
        {
            if (sensorValues == null || sensorValues.Count == 0)
            {
                return null;
            }
            sensorValues.Sort((x, y) => x.CreationTime.CompareTo(y.CreationTime));
            //Calculation of the average fuel level for the last time intervals window Size
            double sum = 0;
            for (int i = sensorValues.Count - windowSize; i < sensorValues.Count; i++)
            {
                sum += sensorValues[i].Value;
            }
            double average = sum / windowSize;
            SensorValue lastSensorValue = sensorValues[sensorValues.Count - 1];
            if ((lastSensorValue.Value < average - maxFuelConsumption)|| (lastSensorValue.Value < average && !ignition))
            {
                return "Обнаружен возможный слив топлива";
            }
            return null;
        }

        /// <summary>
        /// Calculates possible accidents based on sudden acceleration of transport
        /// </summary>
        /// <param name="actualAcceleration"></param>
        /// <param name="maxAcceleration"></param>
        /// <returns>Returns a message indicating whether a sudden acceleration/deceleration was noticed, if not detected, returns null</returns>
        public static string? AccelerationSensorСalc(double actualAcceleration,double maxAcceleration)
        {
            if (Math.Abs(actualAcceleration) > maxAcceleration)
            {
                return "Обнаружено возможное происшествие";
            }
            return null;
        }


    }
}