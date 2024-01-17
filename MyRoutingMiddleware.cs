namespace MinimalAPIGoodsAccounting;

public class MyRoutingMiddleware
{
    private readonly RequestDelegate _next;
    public bool IsAdmin = false;
    public MyRoutingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {

        var responce = context.Response;
        var request = context.Request;
        GoodsHandler handler = (GoodsHandler)context.Items["Handler"];


        if (request.Path == "/" && request.Method == "POST")
        {

            if (request.Form["login"] == "admin" && request.Form["password"] == "admin") IsAdmin = true;
        }
        if (request.Path == "/")
        {
            responce.ContentType = "text/html; charset= utf-8";
            await context.Response.SendFileAsync("auth.html");
        }

        if ((request.Path == "/form") && request.Method == "POST")
        {

            string name = request.Form["name"];
            var formFile = request.Form.Files["fl"];

            if (name != null && name.Length > 4 && name.Length < 20)
            {
                handler.AddGood(new Good(name, formFile));
                var bufGoods = handler.GetGoods();
                var bufgood = bufGoods.LastOrDefault();
                Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}\\wwwroot\\uploads");

                var fullpath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\uploads\\{bufgood.id}.jpg";
                using (var filestream = new FileStream(fullpath, FileMode.Create))
                {
                    await formFile.CopyToAsync(filestream);
                }

            };

        }
        if (context.Request.Path == "/form")
        {
            responce.ContentType = "text/html; charset= utf-8";
            await context.Response.SendFileAsync("base.html");
        }
        if (request.Path == "/about" && IsAdmin)
        {
            responce.ContentType = "text/html; charset= utf-8";
            string res = "";
            foreach (Good good in handler.GetGoods())
            {
                res = $"<br><h2>{good.id} - {good.name}</h2>";
                if (good.photo != null) res += $" <a href=\"wwwroot/uploads/{good.id}.jpg\" target=\"wwwroot/uploads/{good.id}.jpg\">{good.id}.jpg</a>";

                await context.Response.WriteAsync(res);
                if (good.photo != null)
                {
                    //var fileProvider = new PhysicalFileProvider($"{Directory.GetCurrentDirectory()}\\uploads");
                    //var fileinfo = fileProvider.GetFileInfo($"{good.id}.jpg");
                    //await responce.SendFileAsync(fileinfo);
                    //await responce.SendFileAsync($"{Directory.GetCurrentDirectory()}\\uploads\\{good.id}.jpg");
                }
            }

        }
        await _next(context);
    }
}
