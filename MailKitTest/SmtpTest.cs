using System;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Search;
using MimeKit;
using Xunit;
using Xunit.Abstractions;

namespace MailKitTest;

public class SmtpTest
{
    private readonly ITestOutputHelper _outputHelper;

    public SmtpTest(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    private static async Task<SmtpClient> CreateClientAndConnectAsync()
    {
        var smtpClient = new SmtpClient();
        await smtpClient.ConnectAsync(MailHostOptions.SmtpQqHost, MailHostOptions.SmtpQqPort);
        await smtpClient.AuthenticateAsync(MailHostOptions.QqAccount, MailHostOptions.QqPassword);
        return smtpClient;
    }

    [Fact]
    public async Task TestSendEmailAsync()
    {
        
        var imapClient = await ImapTest.CreateClientAndConnectAsync();
        var inbox = imapClient.Inbox;
        await inbox.OpenAsync(FolderAccess.ReadOnly);
        var query = SearchQuery.Recent;
        var uids = await inbox.SearchAsync(query);
        await imapClient.DisconnectAsync(true);
        imapClient.Dispose();
        _outputHelper.WriteLine(uids.Count.ToString());

        while (true)
        {
            foreach (var uniqueId in uids)
            {
                _outputHelper.WriteLine(uniqueId.Id.ToString());
                imapClient = await ImapTest.CreateClientAndConnectAsync();
                inbox = imapClient.Inbox;
                await inbox.OpenAsync(FolderAccess.ReadOnly);
                var message = await inbox.GetMessageAsync(uniqueId);
                _outputHelper.WriteLine("邮件号：{0}\n 邮件主题：{1}", message.MessageId, message.Subject);
                var sendMessage = new MimeMessage();
                sendMessage.From.Add(new MailboxAddress("test", MailHostOptions.QqAccount));
                sendMessage.To.Add(new MailboxAddress("yanjr", "yanjr@aciplaw.com"));
                sendMessage.Subject = message.Subject;
                sendMessage.Body = message.Body;
                using var smtpClient = await CreateClientAndConnectAsync();
                await smtpClient.SendAsync(sendMessage);
                await Task.Delay(TimeSpan.FromMinutes(5));
                await imapClient.DisconnectAsync(true);
                imapClient.Dispose();
            }
        }
    }
}