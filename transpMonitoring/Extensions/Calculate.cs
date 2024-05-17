using System.Diagnostics;
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

        private static int GradeCalc(double value, double firstGradeValue, double secondGradeValue)
        {
            if(value == 0) { return 0; }
            if (value <= firstGradeValue) { return 1; }
            if (value <= secondGradeValue) { return 2; }
            return 3;
        }
        /// <summary>
        /// Calculates the deviation of the sensor readings from the norms set by the maxFuelConsumption and lowerValue parameters
        /// </summary>
        /// <param name="upperValue">The upper value of the norm</param>
        /// <param name="lowerValue">The lower value of the norm</param>
        /// <param name="actualValue">The actual value</param>
        /// <returns>Returns a string with the percentage deviation of the reading from the norm</returns>
        public static (string,int)? StandartSensorCalc(double upperValue,double lowerValue,double actualValue)
        {
            string? content = null;
            int grade = 0;
            if (upperValue < actualValue)
            {
                double difference = PercentageDifference(upperValue, actualValue);
                content = "Показания выше нормы на" + difference.ToString() + " % ";
                grade = GradeCalc(difference, 5, 15);
            }
            if (lowerValue > actualValue)
            {
                double difference = PercentageDifference(lowerValue, actualValue) * -1;
                content="Показания ниже нормы на " + difference.ToString()+" % ";
                grade = GradeCalc(difference, 5, 15);
            }
            if (content == null || grade == 0)
            {
                return null;
            }
            return (content,grade);

        }

        /// <summary>
        /// Speeding check
        /// </summary>
        /// <param name="maxSpeed">maximum permissible speed</param>
        /// <param name="actualSpeed">VehiclePage speed</param>
        /// <returns>Returns a string with the numerical value of the excess or null if there was no excess</returns>
        public static (string, int)? VelocitySensorCalc(double maxSpeed, double actualSpeed)
        {
            string? content = null;
            int grade = 0;
            if (maxSpeed < actualSpeed)
            {
                double difference = actualSpeed - maxSpeed;
                content = "Превышение скорости  на "+ difference.ToString() + "км/ч";
                grade = GradeCalc(difference, 10, 20);
            }
            if (content == null || grade == 0)
            {
                return null;
            }
            return (content, grade);
        }

        /// <summary>
        /// Calculates possible fuel drains based on the average fuel level for the last readings and permissible deviations
        /// </summary>
        /// <param name="sensorValues">List of fuel sensor readings</param>
        /// <param name="maxFuelConsumption">Maximum allowable fuel consumption per measurement</param>
        /// <param name="windowSize">The size of the time window for calculating the average fuel level in the tank (must match the number of incoming readings)</param>
        /// <returns>Returns a message indicating whether a fuel drain was noticed, if there was no drain, returns null</returns>
        public static (string, int)? FuelLevelCalc(List<SensorValue> sensorValues,double maxFuelConsumption,bool ignition, int windowSize= 5)
        {
            if (sensorValues == null || sensorValues.Count == 0)
            {
                return null;
            }
            string? content = null;
            int grade = 0;

            sensorValues.Sort((x, y) => x.CreationTime.CompareTo(y.CreationTime));
            //Calculation of the average fuel level for the last time intervals window Size
            double sum = 0;
            for (int i = sensorValues.Count - windowSize; i < sensorValues.Count; i++)
            {
                sum += sensorValues[i].Value;
            }
            double average = sum / windowSize;
            SensorValue lastSensorValue = sensorValues[sensorValues.Count - 1];
            double difference = average- lastSensorValue.Value ;
            if ((difference > maxFuelConsumption)|| (difference>0 && !ignition))
            {
                content = "Обнаружен возможный слив топлива";
                grade = GradeCalc(difference, maxFuelConsumption, maxFuelConsumption * 1.5);
            }
            if (content == null || grade == 0)
            {
                return null;
            }
            return (content,grade);
        }

        /// <summary>
        /// Calculates possible accidents based on sudden acceleration of transport
        /// </summary>
        /// <param name="actualAcceleration"></param>
        /// <param name="maxAcceleration"></param>
        /// <returns>Returns a message indicating whether a sudden acceleration/deceleration was noticed, if not detected, returns null</returns>
        public static (string, int)? AccelerationSensorСalc(double actualAcceleration,double maxAcceleration)
        {
            string? content = null;
            int grade = 0;
            if (Math.Abs(actualAcceleration) > maxAcceleration)
            {
                content= "Обнаружено возможное происшествие";
                grade = GradeCalc(actualAcceleration, maxAcceleration, maxAcceleration * 2);
            }
            if (content == null || grade == 0)
            {
                return null;
            }
            return (content, grade);
        }


    }
}