﻿// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using Slimforge.Api.Momo.Clients.MomoApis;
using Slimforge.Api.Momo.Models.Configurations;

namespace Slimforge.Api.Momo.Tests.Integration.APIs.Remittances
{
    public partial class RemittancesApiTests
    {
        private readonly IMomoClient momoClient;

        public RemittancesApiTests()
        {
            var momoConfigurations = new MomoConfigurations
            {
                ApiUser = Environment.GetEnvironmentVariable("ApiUser"),
                ApiKey = Environment.GetEnvironmentVariable("ApiKey"),
                SubscriptionKey = Environment.GetEnvironmentVariable("SubscriptionKey"),
                BaseUrl = "https://sandbox.momodeveloper.mtn.com/",
                TargetEnvironment = "sandbox"
            };

            this.momoClient = new MomoClient(momoConfigurations);
        }
    }
}