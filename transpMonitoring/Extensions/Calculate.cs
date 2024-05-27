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
        /// Calculates the deviation of the sensor readings from the norms set by the _maxFuelConsumptionPerMinute and lowerValue parameters
        /// </summary>
        /// <param name="upperValue">The upper value of the norm</param>
        /// <param name="lowerValue">The lower value of the norm</param>
        /// <param name="actualValue">The actual value</param>
        /// <returns>Returns a string with the percentage deviation of the reading from the norm</returns>
        public static (string,int)? StandartSensorCalc(double actualValue,double? upperValue,double? lowerValue)
        {
            if(upperValue == null || lowerValue == null)
            {
               return ("ОШИБКА В Установке Крайних значений ", 3);
            }
            string? content = null;
            int grade = 0;
            if (upperValue < actualValue)
            {
                double difference = PercentageDifference((double)upperValue, actualValue);
                content = "Показания выше нормы на " + (difference / 100d).ToString("P2");
                grade = GradeCalc(difference, 5, 15);
            }
            if (lowerValue > actualValue)
            {
                double difference = PercentageDifference((double)lowerValue, actualValue) * -1;
                content="Показания ниже нормы на " + (difference/100d).ToString("P2");
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
        /// <param name="_maxSpeed">maximum permissible speed default = 90km/h</param>
        /// <param name="actualSpeed">VehiclePage speed</param>
        /// <returns>Returns a string with the numerical value of the excess or null if there was no excess</returns>
        public static (string, int)? VelocitySensorCalc(double actualSpeed,double? maxSpeed)
        {
            double _maxSpeed = maxSpeed ?? 90;
            string? content = null;
            int grade = 0;
            if (_maxSpeed < actualSpeed)
            {
                double difference = actualSpeed - _maxSpeed;
                content = "Превышение скорости  на "+ difference.ToString("F2") + "км/ч";
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
        /// <param name="maxFuelConsumptionPerMinute">Maximum allowable fuel consumption per measurement</param>
        /// <param name="windowSize">The size of the time window for calculating the average fuel level in the tank (must match the number of incoming readings)</param>
        /// <returns>Returns a message indicating whether a fuel drain was noticed, if there was no drain, returns null</returns>
        public static (string, int)? FuelLevelCalc(List<SensorValue> sensorValues, bool ignition, double? maxFuelConsumptionPerMinute, int windowSize= 5)
        {
            //ИЗМНИТЬ ПОД ПРОВЕРКУ ПО ВРЕМНИ 
            if (sensorValues == null || sensorValues.Count == 0)
            {
                return null;
            }
            double _maxFuelConsumptionPerMinute = maxFuelConsumptionPerMinute ?? 0.08;
            string? content = null;
            int grade = 0;
            sensorValues = sensorValues.OrderBy(u => u.CreationTime).ToList();
            if (sensorValues.Count-1 < windowSize)
            {
                windowSize = sensorValues.Count-1;
            }
            //Calculation of the average fuel level for the last time intervals window Size
            double sum = 0;
            for (int i = 0; i < windowSize; i++)
            {
                sum += sensorValues[i].Value;
            }
            double average = sum / windowSize;
            SensorValue lastSensorValue = sensorValues[sensorValues.Count - 1];

            // Время между последним значением и значением из окна
            TimeSpan timeDifference = lastSensorValue.CreationTime - sensorValues[sensorValues.Count-2].CreationTime;
            double SecondDifference = Math.Min(timeDifference.TotalSeconds,20);

            if (SecondDifference <= 0)
            {
                return ("ОШИБКА В АНАЛИЗЕ ", 3);
            }
            double allowableFuelConsumption = _maxFuelConsumptionPerMinute/60d * SecondDifference;
            double difference = average- lastSensorValue.Value ;
            if ((difference > allowableFuelConsumption) || (difference>0 && !ignition))
            {
                content = "Обнаружен возможный слив топлива ";
                grade = GradeCalc(difference, allowableFuelConsumption, allowableFuelConsumption * 1.5);
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
        /// <param name="maxAcceleration">Default 2.5G </param>
        /// <returns>Returns a message indicating whether a sudden acceleration/deceleration was noticed, if not detected, returns null</returns>
        public static (string, int)? AccelerationSensorСalc(double actualAcceleration,double? maxAcceleration)
        {
            double _maxAcceleration = maxAcceleration ?? 2.5;
            string? content = null;
            int grade = 0;
            if (Math.Abs(actualAcceleration) > _maxAcceleration)
            {
                content= "Обнаружено возможное происшествие ";
                grade = GradeCalc(actualAcceleration, _maxAcceleration, _maxAcceleration * 2);
            }
            if (content == null || grade == 0)
            {
                return null;
            }
            return (content, grade);
        }


    }
}