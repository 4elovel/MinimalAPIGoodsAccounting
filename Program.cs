namespace MinimalAPIGoodsAccounting;

public class Program
{

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();
        GoodsHandler handler = new(new List<Good>() { new Good("Apple"), new Good("Banana"), new Good("orange") });


        app.Use((context, next) =>
        {
            context.Items["Handler"] = handler;
            var responce = context.Response;
            var request = context.Request;
            return next(context);
        });
        app.UseMiddleware<MyRoutingMiddleware>();
        app.Run(async (context) =>
        {
            var Handler = (GoodsHandler)context.Items["Handler"];
            foreach (var i in handler.GetGoods())
            {
                var fullpath = $"/wwwroot/uploads/{i.id}.jpg";
                if (context.Request.Path == fullpath)
                {
                    await context.Response.SendFileAsync($"{Directory.GetCurrentDirectory()}\\wwwroot\\uploads\\{i.id}.jpg");
                }
            }
        });
        app.Run();
    }
}
