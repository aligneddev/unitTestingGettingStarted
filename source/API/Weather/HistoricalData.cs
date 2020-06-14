using System;

namespace Api.Weather
{
    public class HistoricalData
    {
        public DateTime DateTime { get; set; }

        public string ZipCode { get; set; }

        public double TempF { get; set; }
    }
}