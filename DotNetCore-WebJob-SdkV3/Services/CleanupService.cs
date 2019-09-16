﻿using Microsoft.ApplicationInsights;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCore_WebJob_SdkV3.Services
{
    public class CleanupService : ICleanupService
    {
        private readonly TelemetryClient _telemetry;

        public CleanupService(
            TelemetryClient telemetry)
        {
            _telemetry = telemetry;
        }

        public Task CleanupAsync(CancellationToken cancellationToken)
        {
            _telemetry.TrackEvent("Service executed!");

            return Task.CompletedTask;
        }
    }
}
