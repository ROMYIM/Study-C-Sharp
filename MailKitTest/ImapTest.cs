using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using Xunit;

namespace MailKitTest;

public class ImapTest
{
    public static async Task<ImapClient> CreateClientAndConnectAsync()
    {
        var imapClient = new ImapClient();
        await imapClient.ConnectAsync(MailHostOptions.Imap163Host, MailHostOptions.Imap163Port);
        await imapClient.AuthenticateAsync(MailHostOptions.Imap163Account, MailHostOptions.QqPassword);
        var clientImplementation = new ImapImplementation()
        {
            Name = "Study-C-Sharp",
            Version = "1.0.0",
            OS = "Windows",
            OSVersion = "21H2"
        };
        await imapClient.IdentifyAsync(clientImplementation);
        return imapClient;
    }

    [Fact]
    public async Task TestConnectMailHost()
    {
        using var imapClient = await CreateClientAndConnectAsync();
        // var folders = await imapClient.GetFoldersAsync();
        var inbox = imapClient.Inbox;
        await inbox.OpenAsync(FolderAccess.ReadOnly);
        Assert.Equal(28, inbox.Count);
    }

    [Fact]
    public async Task TestParseMessageUid()
    {
        using var imapClient = await CreateClientAndConnectAsync();
        var inbox = imapClient.Inbox;
        await inbox.OpenAsync(FolderAccess.ReadOnly);

        foreach (var summary in await inbox.FetchAsync (0, 2, MessageSummaryItems.Full))
        {
            var mailId = summary.EmailId;
            var uniqueId = summary.UniqueId;
            var message = await inbox.GetMessageAsync(uniqueId);

            if (UniqueId.TryParse(mailId, out var parsedUniqueId))
            {
                Assert.Equal(uniqueId, parsedUniqueId);
            }
            else if (UniqueId.TryParse(message.MessageId, out parsedUniqueId))
            {
                Assert.Equal(uniqueId, parsedUniqueId);
            }
            
            Assert.True(false);
        }

    }
}