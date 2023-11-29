﻿// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV3s;

namespace Slimforge.Api.Momo.Infrastructure.Build
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var adoNetClient = new ADotNetClient();

            var githubPipeline = new GithubPipeline
            {
                Name = "Slimforge.Api.Momo Build",


                OnEvents = new Events
                {
                    Push = new PushEvent
                    {
                        Branches = new string[] { "main" }
                    },

                    PullRequest = new PullRequestEvent
                    {
                        Branches = new string[] { "main" }
                    }
                },

                Jobs = new Dictionary<string, Job>
                {
                    {
                        "build",
                        new Job
                        {
                            EnvironmentVariables = new Dictionary<string, string>
                            {
                                { "ApiUser", "${{ secrets.API_USER }}" },
                                { "ApiKey", "${{ secrets.API_KEY }}" },
                                { "SubscriptionKey", "${{ secrets.SUBSCRIPTION_KEY }}" },
                                { "BaseUrl", "${{ vars.BASE_URL }}" },
                                { "TargetEnvironment", "${{ vars.TARGET_ENVIRONMENT }}" }
                            }, 

                            RunsOn = BuildMachines.WindowsLatest,

                            Steps = new List<GithubTask>
                            {
                                new CheckoutTaskV3
                                {
                                    Name = "Pulling Code"
                                },

                                new SetupDotNetTaskV3
                                {
                                    Name = "Installing .NET",

                                    With = new TargetDotNetVersionV3
                                    {
                                        DotNetVersion = "7.0.201"
                                    }
                                },

                                new RestoreTask
                                {
                                    Name = "Restoring Packages"
                                },

                                new DotNetBuildTask
                                {
                                    Name = "Building Solution"
                                },

                                new TestTask
                                {
                                    Name = "Running Tests"
                                }
                            }
                        }
                    }
                }
            };

            //string buildScriptPath = "../../../../.github/workflows/dotnet.yml";
            string buildScriptPath = "../.github/workflows/dotnet.yml";
            string directoryPath = Path.GetDirectoryName(buildScriptPath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            adoNetClient.SerializeAndWriteToFile(
                adoPipeline: githubPipeline,
                path: buildScriptPath);
        }
    }
}