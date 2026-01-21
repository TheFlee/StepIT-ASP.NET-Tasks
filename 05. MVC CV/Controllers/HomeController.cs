using _05._MVC_CV.Models;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Diagnostics;
using System.Net.Mail;

namespace _05._MVC_CV.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _config;

    public HomeController(ILogger<HomeController> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Skills()
    {
        var skills = new List<string>
        {
            "C#",
            "ASP.NET Core",
            "MVC",
            "Entity Framework",
            "SQL",
            "JavaScript",
            "HTML/CSS",
            "Python",
            "C++",
            "ADO.NET",
            "React"
        };

        return View(skills);
    }
    public IActionResult Projects()
    {
        var projects = new List<Project>
        {
            new Project
            {
                Name = "MetaBlog Site",
                Description = "React ile yazilmish blog sayti",
                Url = "https://github.com/TheFlee/StepIT-React-FinalProject"
            },
            new Project
            {
                Name = "Quiz App",
                Description = "ADO.NET istifade ederek yazilmish console application",
                Url = "https://github.com/TheFlee/StepIT-React-FinalProject"
            },
            new Project
            {
                Name = "Ecommerce Site",
                Description = "JavaScript ve HTML ile yazilmish sayt",
                Url = "https://github.com/TheFlee/StepIT-JS-FinalProject"
            },
        };
        return View(projects);
    }
    public IActionResult AboutMe()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ContactMe()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ContactMe(ContactForm model)
    {
        SendMail(model);
        SendAutoReply(model);

        return View();
    }

    void SendMail(ContactForm model)
    {
        var pass = _config["Smtp:Pass"];
        var client = new MailKit.Net.Smtp.SmtpClient();
        client.Connect("smtp.gmail.com", 587);
        client.Authenticate("firidunhasanli@gmail.com", pass);

        var message = new MimeKit.MimeMessage();

        message.From.Add(new MimeKit.MailboxAddress(model.Name, model.Email));

        message.To.Add(new MimeKit.MailboxAddress("Firidun Hasanli", "firidunhasanli@gmail.com"));

        message.Subject = "New Contact Message";

        message.Body = new MimeKit.TextPart("html")
        {
            Text = $"""
        <html>
        <body style="font-family:Arial; background:#f4f4f4; padding:20px;">
            <div style="max-width:600px; margin:auto; background:white; padding:20px; border-radius:8px;">
                <h2>New Contact Message</h2>
                <p><strong>Name:</strong> {model.Name}</p>
                <p><strong>Email:</strong> {model.Email}</p>
                <p><strong>Message:</strong></p>
                <p>{model.Message}</p>
            </div>
        </body>
        </html>
        """
        };

        client.Send(message);
        client.Disconnect(true);
    }

    void SendAutoReply(ContactForm model)
    {
        var pass = _config["Smtp:Pass"];
        var client = new MailKit.Net.Smtp.SmtpClient();
        client.Connect("smtp.gmail.com", 587);
        client.Authenticate("firidunhasanli@gmail.com", pass);

        var message = new MimeKit.MimeMessage();

        message.From.Add(new MimeKit.MailboxAddress("Firidun Hasanli", "firidunhasanli@gmail.com"));

        message.To.Add(new MimeKit.MailboxAddress(model.Name, model.Email));

        message.Subject = "Thank you for contacting me";

        message.Body = new MimeKit.TextPart("html")
        {
            Text = $"""
        <html>
        <body style="font-family:Arial; background:#0b0d17; color:#ffffff; padding:30px;">
            <div style="max-width:600px; margin:auto; background:#1c1f2a; padding:20px; border-radius:10px;">
                <h2 style="color:#00bfff;">Thank you, {model.Name}!</h2>
                <p>I have received your message and will get back to you soon.</p>
                <p style="margin-top:30px; font-size:12px; color:#aaaaaa;">
                    © 2025 Firidun Hasanli
                </p>
            </div>
        </body>
        </html>
        """
        };

        client.Send(message);
        client.Disconnect(true);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
