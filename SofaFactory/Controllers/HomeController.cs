using SofaFactory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SofaFactory.Data;
using SofaFactory.Models;
using System.Diagnostics;

namespace SofaFactory.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;
        private readonly IEmailService emailService;
        private readonly IOptions<SmtpOptions> option;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context,  IEmailService emailService,
            IOptions<SmtpOptions> option)
        {
            _logger = logger;
            this.context = context;
            this.emailService = emailService;
            this.option = option;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Slider = await context.Slider.Include(x => x.Image).ToListAsync();
            var cats = await context.Categories.Where(x => x.ParentId == null).Include(x => x.Image).Take(7).ToListAsync();
            return View(cats);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactFormDto form)
        {
                 try
            {
              _ = emailService.SendEmailAsyn(
    option.Value.UserName,
    option.Value.SendTo,
    "sofafactory.co.in | Contact Form",
    $@"<html>
        <body>
            <p><strong>First Name:</strong> {form.First}</p>
            <p><strong>Last Name:</strong> {form.Last}</p>
            <p><strong>Email:</strong> {form.Email}</p>
            <p><strong>Mobile Number:</strong> {form.Phone}</p>
            <p><strong>Message:</strong> {form.Message}</p>
        </body>
    </html>");

                return Ok("Email Send!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error sending email: {ex.Message}");

            }
        }


        //Franchisee or Dealership enquery form / /home/EnqueryForm
        [HttpPost]
        public async Task<IActionResult> EnqueryForm(FnDEnqueryForm form)
        {
                 try
            {
             _ = emailService.SendEmailAsyn(
    option.Value.UserName,
    option.Value.SendTo,
    "sofafactory.co.in | Enquiry Form",
    $@"<html>
        <body>
            <p><strong>Name:</strong> {form.Name}</p>
            <p><strong>Email:</strong> {form.Email}</p>
            <p><strong>Phone:</strong> {form.phone}</p>
            <p><strong>Address:</strong> {form.address}</p>
            <p><strong>Town:</strong> {form.town}</p>
            <p><strong>Date of Establishment:</strong> {form.DateOfEstablishment}</p>
            <p><strong>Enquiry:</strong> {form.Enquery}</p>
            <p><strong>Additional Information:</strong> {form.Additional}</p>
        </body>
    </html>");


                return Ok("Email Send!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error sending email: {ex.Message}");

            }
        }
    }
    
}