using EventBooking.Storage.Protos;

namespace EventBooking.Event.Features;

public class Test : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/test", async (HttpRequest request, ImageStorage.ImageStorageClient client) =>
        {
            try
            {
                var form = await request.ReadFormAsync();
                var file = form.Files.GetFile("image");
                var category = form["category"].ToString() ?? "misc";

                if (file == null || file.Length == 0)
                    return Results.BadRequest("No file uploaded");

                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);
                var imageData = ms.ToArray();

                var grpcRequest = new UploadImageRequest
                {
                    FileName = file.FileName,
                    Category = category,
                    ImageData = Google.Protobuf.ByteString.CopyFrom(imageData)
                };

                var response = await client.UploadImageAsync(grpcRequest);
                return Results.Ok(new { imageUrl = response.ImageUrl });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        });
    }
}