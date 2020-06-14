using System;

namespace Api.Core.Weather
{
    public class HistoricalData
    {
        public DateTime DateTime { get; set; }

        public string ZipCode { get; set; }

        public double TempF { get; set; }
    }
}