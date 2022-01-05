using MailKit;
using Xunit;
using Xunit.Abstractions;

namespace MailKitTest;

public class UniqueIdTest
{
    private readonly ITestOutputHelper _outputHelper;

    public UniqueIdTest(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Theory]
    [InlineData("1ad669fb6908410e85fddbfe19f6cb72.20211213.1639380747@qcloudmail.com")]
    [InlineData("1639382181750_19243_18513_2162.sc-10_9_179_197-inbound0@shmail.ibeisen.com")]
    [InlineData("467303F70004.117.1543541438.818213.2")]
    [InlineData("78354733.2852123.1639377513343.JavaMail.admin@aciplaw.com")]
    [InlineData("7A6203F70004.35.1543541710.877624.1")]
    [InlineData("4A7B03F70004.107.1543542085.461716.1")]
    [InlineData("6D303F70004.131.1543542358.297898.1")]
    [InlineData("35B503F70004.145.1543543095.370026.1")]
    [InlineData("6CD003F70004.113.1543543352.881809.1")]
    public void TestConvertMessageIdToUniqueId(string messageId)
    {
        Assert.True(UniqueId.TryParse(messageId, out var uniqueId));
    }
}