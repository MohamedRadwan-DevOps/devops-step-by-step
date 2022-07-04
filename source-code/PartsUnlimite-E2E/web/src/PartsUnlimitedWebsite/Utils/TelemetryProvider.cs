using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;

namespace PartsUnlimited.Utils
{
    public class TelemetryProvider : ITelemetryProvider
    {
        public TelemetryClient AppInsights { get; set; }

        public TelemetryProvider()
        {
            AppInsights = new TelemetryClient();
        }

        public void TrackEvent(string message)
        {
            AppInsights.TrackEvent(message);
        }

        public void TrackEvent(string message, Dictionary<string, string> properties, Dictionary<string, double> measurements)
        {
            AppInsights.TrackEvent(message, properties, measurements);
        }

        public void TrackTrace(string message)
        {
            AppInsights.TrackTrace(message);
        }

        public void TrackException (Exception exception)
        {
            AppInsights.TrackException(exception);
        }
    }
}