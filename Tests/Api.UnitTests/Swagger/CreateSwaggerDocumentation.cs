// ***********************************************************************
// Assembly         : Api.UnitTests
// Author           : MartinG
// Created          : 05-11-2018
//
// Last Modified By : MartinG
// Last Modified On : 05-21-2018
// ***********************************************************************
// <copyright file="CreateSwaggerDocumentation.cs" company="Callcredit Information Group.">
// Copyright (c) 2018 Callcredit Information Group. All rights reserved.
// </copyright>
// <summary>Defines unit test for the Swagger documentation generator class.</summary>
// ***********************************************************************
using System;
using System.Collections.ObjectModel;
using System.Fabric;
using System.Fabric.Description;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Callcredit.AspNetCore.IntegrationTesting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Api.UnitTests.Swagger
{
    /// <summary>
    /// Class CreateSwaggerDocumentation.
    /// </summary>
    [TestClass]
    public class CreateSwaggerDocumentation
    {
        /// <summary>
        /// Creates the documentation.
        /// </summary>
        /// <returns>Task.</returns>
        [TestMethod]
        [Ignore("Unable to find package Microsoft.CodeAnalysis. No packages exist with this id in source(s): http://pllwinmxfsngt01/FsNuGetServer/nuget, http://pllwinmxfsngt01/ThirdPartyDependenciesNuGetServer/nuget/")]
        public async Task CreateDocumentation()
        {
            var outputDirectory = Environment.CurrentDirectory;

            var codePackageActivationContext = new Mock<ICodePackageActivationContext>();
            codePackageActivationContext.Setup(context => context.GetEndpoints())
                .Returns(new Mock<KeyedCollection<string, EndpointResourceDescription>>().Object);

            using (var sut = SystemUnderTest.ForStartup<Api.Startup>("Callcredit.Insolvencies.Service/Api/"))
            {
                sut.ConfigureServices(services =>
                {
                    services.AddSingleton(codePackageActivationContext.Object);
                    services.AddSingleton(new Uri("fabric:/uk.mastered-data.insolvency-orders/api"));
                });

                var swaggerJson = await sut.GenerateSwaggerJson(_ =>
                {
                    _.ApiVersion = "v1";
                    _.ApiTitle = "Insolvencies Mastered Data";
                    _.ApiDescription = "Adds resources in the Insolvencies domain to the overall Callcredit Api.";
                    _.IncludeXmlComments(Path.Combine(outputDirectory, "Api.xml"));
                    _.OutputToFile(outputDirectory);
                });

                Assert.IsFalse(swaggerJson.Length == 0);
                Assert.IsTrue(File.Exists(Path.Combine(outputDirectory, "swagger.json")));
            }
        }

        /// <summary>
        /// Checks the path combine.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the Solution folder cannot be found</exception>
        [TestMethod]
        public void CheckPathCombine()
        {
            const string solutionRelativePath = "Callcredit.Insolvencies.Service/Api/";
            var applicationBasePath = AppContext.BaseDirectory;
            const string solutionName = "*.sln";

            DirectoryInfo directoryInfo = new DirectoryInfo(applicationBasePath);
            while (Directory.EnumerateFiles(directoryInfo.FullName, solutionName).FirstOrDefault<string>() == null)
            {
                directoryInfo = directoryInfo.Parent;
                if (directoryInfo.Parent == null)
                {
                    throw new InvalidOperationException($"Solution root could not be located using application root {applicationBasePath}.");
                }
            }

            string actual = Path.GetFullPath(Path.Combine(directoryInfo.FullName, solutionRelativePath));

            Assert.IsTrue(actual.EndsWith("Callcredit.Insolvencies.Service\\Api\\"));
        }
    }
}
