using Microsoft.AspNetCore.Mvc;

public class CookieController : Controller
{
    [Route("Cookie")]
    public IActionResult Cookie()
    {
        ViewBag.Message = (Request.Cookies.TryGetValue("myCookie", out string? cookieValue)) ?
                            $"🍪 value:\n: {cookieValue}" : ViewBag.Message = "There is no 🍪";

        return View();
    }
}
