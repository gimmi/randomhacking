using System;
using System.Linq;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace SpikeDockerDotNet
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var endpoint = new Uri("tcp://localhost:2375");
            var dockerClient = new DockerClientConfiguration(endpoint)
                .CreateClient();

            var progress = new Progress<JSONMessage>();
            progress.ProgressChanged += (sender, e) => Console.WriteLine(e.Status);

            var imageName = "mcr.microsoft.com/mssql/server";
            var imageTag = "2017-latest";
            var containerName = "nice_noether";


            // await dockerClient.Images.CreateImageAsync(new ImagesCreateParameters {FromImage = imageName, Tag = imageTag}, null, progress);

            var containerListResponses = await dockerClient.Containers.ListContainersAsync(new ContainersListParameters {
                All = true,
                Filters = {
                    ["name"] = {
                        [containerName] = true
                    }
                }
            });
            var containerListResponse = containerListResponses.SingleOrDefault();
            if (containerListResponse != null)
            {
                var containerInspectResponse = await dockerClient.Containers.InspectContainerAsync(containerListResponse.ID);
                await Console.Out.WriteLineAsync("Running: " + containerInspectResponse.State.Running);
            }
        }
    }
}
