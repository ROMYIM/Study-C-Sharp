using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace SecurityTest
{
    public class DataProtectionTest
    {
        private readonly ITestOutputHelper _outputHelper;

        public DataProtectionTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Theory]
        [InlineData("password")]
        public void DesTest(string protectField)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            try
            {
                serviceCollection.AddDataProtection()
                    .SetDefaultKeyLifetime(TimeSpan.MaxValue)
                    .DisableAutomaticKeyGeneration()
                    .PersistKeysToFileSystem(new DirectoryInfo(@"I:\yim\Documents\PrivateKeys"))
                    .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration()
                    {
                        EncryptionAlgorithm = EncryptionAlgorithm.AES_128_CBC
                    });
            }
            catch (Exception e)
            {
                _outputHelper.WriteLine(e.ToString());
                if (e is ArgumentException argumentException)
                {
                    _outputHelper.WriteLine($"the argument {argumentException.ParamName} is {argumentException.Source}");
                }
                throw;
            }

            var services = serviceCollection.BuildServiceProvider();
            var protectProvider = services.GetRequiredService<IDataProtectionProvider>();
            var protector = protectProvider.CreateProtector(protectField);

            var testData = Encoding.UTF8.GetBytes("sdfscsdewrwer");
            var encryptedData = protector.Protect(testData);
            var decryptedData = protector.Unprotect(encryptedData);
            
            Assert.Equal(testData, decryptedData);
        }
    }
}
