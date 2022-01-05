using System;
using System.Text;
using System.Threading.Tasks;
using LumiSoft.Net.Mail;
using LumiSoft.Net.POP3.Client;
using MailKit.Net.Pop3;
using Xunit;
using Xunit.Abstractions;

namespace MailKitTest;

public class Pop3Test
{
    private readonly ITestOutputHelper _testOutputHelper;

    public Pop3Test(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private async Task<Pop3Client> CreateClientAsync()
    {
        var pop3Client = new Pop3Client();
        await pop3Client.ConnectAsync(MailHostOptions.Pop3163EnterpriseHost, MailHostOptions.Pop3163EnterprisePort);
        await pop3Client.AuthenticateAsync(MailHostOptions.Enterprise163Account,
            MailHostOptions.Enterprise163Password);

        return pop3Client;
    }

    private POP3_Client CreateLumisoftPop3Client()
    {
        POP3_Client client = new POP3_Client();
        client.Connect(MailHostOptions.Pop3163EnterpriseHost, MailHostOptions.Pop3163EnterprisePort, true);
        client.Login(MailHostOptions.Enterprise163Account, MailHostOptions.Enterprise163Password);
        return client;
    }

    [Fact]
    public async Task TestGetMessageAsync()
    {
        using var pop3Client = await CreateClientAsync();
        var message = await pop3Client.GetMessageAsync(0);
        _testOutputHelper.WriteLine(message.Subject);
        _testOutputHelper.WriteLine(message.MessageId);
        _testOutputHelper.WriteLine(message.Headers["Message-ID"]);

        var messageUid = await pop3Client.GetMessageUidAsync(0);
        _testOutputHelper.WriteLine(messageUid);

        Assert.Equal("AEwA8QBLE1RDbX1i4gWe04qS", messageUid);
    }

    [Fact]
    public void TestLumisoftPop3()
    {
        using var client = CreateLumisoftPop3Client();
        var message = client.Messages[0];
        // _testOutputHelper.WriteLine(message.HeaderToString());
        _testOutputHelper.WriteLine(message.UID);
        
        var bytes = message.MessageToByte();
        var mailMessage = Mail_Message.ParseFromByte(bytes, Encoding.UTF8);
        _testOutputHelper.WriteLine(mailMessage.MessageID);
    }
}