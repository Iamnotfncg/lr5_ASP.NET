using Microsoft.AspNetCore.Mvc;

public class FormController : Controller
{
    [HttpPost]
    [Route("/process_form")]
    public IActionResult ProcessForm([FromBody] FormData formData)
    {
        // Зчитування даних з форми
        string value = formData.Value;
        DateTime dateTime = DateTime.Parse(formData.DateTime);

        // Запис даних в Cookies з встановленням дати старіння
        Response.Cookies.Append("myCookie", value, new Microsoft.AspNetCore.Http.CookieOptions
        {
            Expires = dateTime,
            HttpOnly = true
        });

        // Повернення відповіді на клієнтський бік
        return Json(new { success = true });
    }

    public class FormData
    {
        public string? Value { get; set; }
        public string? DateTime { get; set; }
    }
}