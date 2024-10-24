using ShopApp.WebUI.EmailServices;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class SmtpEmailSender : IEmailSender
{
    private readonly string _host;
    private readonly int _port;
    private readonly bool _enableSSL;
    private readonly string _userName;
    private readonly string _password;

    public SmtpEmailSender(string host, int port, bool enableSSL, string userName, string password)
    {
        _host = host;
        _port = port;
        _enableSSL = enableSSL;
        _userName = userName;
        _password = password;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        if (string.IsNullOrEmpty(_userName))
        {
            throw new ArgumentNullException(nameof(_userName), "Sender email address cannot be null or empty.");
        }

        var client = new SmtpClient(_host, _port)
        {
            Credentials = new NetworkCredential(_userName, _password),
            EnableSsl = _enableSSL,
            DeliveryMethod = SmtpDeliveryMethod.Network
        };
        
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_userName),
            Subject = subject,
            Body = message,
            IsBodyHtml = true
        };

        mailMessage.To.Add(email);

        await client.SendMailAsync(mailMessage);
    }
}