using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using SchoolAcademicDataManagement.Models;
using SchoolAcademicDataManagement.Models.Academic;

namespace SchoolAcademicDataManagement.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClient _client;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _client = new HttpClient();
        _client.BaseAddress = new Uri("http://localhost:5217");
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ViewMarks(int studentId, int classId)
    {
        try
        {
            // Send a GET request to the specified API endpoint using the HttpClient (_client) and await the response.
            HttpResponseMessage response = await _client.GetAsync($"api/marks/{studentId}/{classId}");
            if (response.IsSuccessStatusCode)
            {
                // If the response is successful, deserialize the JSON content of the response to a MarksheetViewModel object.
                var marksheet = await response.Content.ReadFromJsonAsync<MarksheetViewModel>();
                // Return a view containing the retrieved marksheet data.
                return View(marksheet);
            }
            else
            {
                // Redirect the user to the "Index" view, which might contain an error message indicating the failure.
                ModelState.AddModelError(string.Empty, "Marksheet not found for the given student and class.");
                return View("Index");
            }
        }
        catch (HttpRequestException ex)
        {
            var message = ex.Message;
            ModelState.AddModelError(string.Empty, message);
            // Redirect the user to the "Index" view, which might contain an error message indicating the failure.
            return View("Index");
        }
        
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}