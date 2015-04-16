using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using Picture.Model;
using System.Security.Policy;

namespace PictureAPI_v2.Controllers
{
    [RoutePrefix("api/v1/images")]
    public class ProductImageController : ApiController
    {
        static private string imagesPath = HostingEnvironment.MapPath("~/Images/");
        static private Dictionary<int, ProductImage> images;
        static private string versionHeader = "ax-version";

        static ProductImageController()
        {
            images = new Dictionary<int, ProductImage>();

            foreach(var filePath in Directory.GetFiles(imagesPath, "*.jpg"))
            {
                var productImage = new ProductImage()
                {
                    ProductImageId = int.Parse(Path.GetFileNameWithoutExtension(filePath))
                };

                productImage.Description = string.Format("Image {0}", Path.GetFileNameWithoutExtension(filePath));

                images.Add(productImage.ProductImageId, productImage);
            }
        }

        [Route("")]
        public IEnumerable<ProductImage> GetProductImages([FromUri(Name="app-version")] string appVersion = "")
        {
            if (Request.Headers.Contains(versionHeader))
            {
                var version = Request.Headers.GetValues(versionHeader);
            }

            foreach(var productImage in images.Values)
            {
                productImage.Location = Url.Link("GetProductImage", new { id = productImage.ProductImageId });
            }

            return images.Values.ToArray();
        }

        [Route("{id:int}", Name = "GetProductImage")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetProductImage(int Id)
        {
            HttpResponseMessage response;

            if (images.ContainsKey(Id))
            {
                String filePath = images[Id].Path(imagesPath);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    using (Image image = Image.FromStream(fileStream))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            image.Save(memoryStream, ImageFormat.Jpeg);

                            response = new HttpResponseMessage(HttpStatusCode.OK);

                            response.Content = new ByteArrayContent(memoryStream.ToArray());
                            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                        }
                    }
                }
            }
            else
                response = new HttpResponseMessage(HttpStatusCode.NotFound);

            return await Task.FromResult(response);
        }
    }
}
