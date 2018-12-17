using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace POC.JOB
{
    public class DynamicWCF
    {
        public DynamicWCF(string url, string contractName, string operationName, object[] operationParameters)
        {
            // Define the metadata address, contract name, operation name, and parameters. 
            // You can choose between MEX endpoint and HTTP GET by changing the address and enum value.
            Uri mexAddress = new Uri(url + "?wsdl");

            // For MEX endpoints use a MEX address and a mexMode of .MetadataExchange
            MetadataExchangeClientMode mexMode = MetadataExchangeClientMode.HttpGet;

            // Get the metadata file from the service.
            MetadataExchangeClient mexClient = new MetadataExchangeClient(mexAddress, mexMode);
            mexClient.ResolveMetadataReferences = true;
            MetadataSet metaSet = mexClient.GetMetadata();

            // Import all contracts and endpoints
            WsdlImporter importer = new WsdlImporter(metaSet);
            Collection<ContractDescription> contracts = importer.ImportAllContracts();
            ServiceEndpointCollection allEndpoints = importer.ImportAllEndpoints();

            // Generate type information for each contract
            ServiceContractGenerator generator = new ServiceContractGenerator();
            var endpointsForContracts = new Dictionary<string, IEnumerable<ServiceEndpoint>>();

            foreach (ContractDescription contract in contracts)
            {
                generator.GenerateServiceContractType(contract);
                // Keep a list of each contract’s endpoints
                endpointsForContracts[contract.Name] = allEndpoints.Where(
                    se => se.Contract.Name == contract.Name).ToList();
            }

            if (generator.Errors.Count != 0)
                throw new Exception("There were errors during code compilation.");

            // Generate a code file for the contracts 
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("C#");

            // Compile the code file to an in-memory assembly
            // Don’t forget to add all WCF-related assemblies as references
            CompilerParameters compilerParameters = new CompilerParameters(
                new string[] { 
                       "System.dll", "System.ServiceModel.dll", 
                       "System.Runtime.Serialization.dll" });
            compilerParameters.GenerateInMemory = true;

            CompilerResults results = codeDomProvider.CompileAssemblyFromDom(
                compilerParameters, generator.TargetCompileUnit);

            if (results.Errors.Count > 0)
            {
                throw new Exception("There were errors during generated code compilation");
            }
            else
            {
                // Find the proxy type that was generated for the specified contract
                // (identified by a class that implements the contract and ICommunicationbject)
                Type clientProxyType = results.CompiledAssembly.GetTypes().First(
                    t => t.IsClass &&
                        t.GetInterface(contractName) != null &&
                        t.GetInterface(typeof(ICommunicationObject).Name) != null);

                // Get the first service endpoint for the contract
                ServiceEndpoint se = endpointsForContracts[contractName].First();

                // Create an instance of the proxy
                // Pass the endpoint’s binding and address as parameters
                // to the ctor
                object instance = results.CompiledAssembly.CreateInstance(
                    clientProxyType.Name,
                    false,
                    System.Reflection.BindingFlags.CreateInstance,
                    null,
                    new object[] { se.Binding, se.Address },
                    CultureInfo.CurrentCulture, null);
                                
                // Get the operation’s method, invoke it, and get the return value
                MethodInfo method = instance.GetType().GetMethod(operationName);

                var type = method.GetParameters().First().ParameterType;
                var args = (object[]) operationParameters.Select(p => Convert.ChangeType(p, type)).ToArray();


                object retVal = method.Invoke(instance, args);

                Console.WriteLine(retVal.ToString());
            }
        }
    }
}
