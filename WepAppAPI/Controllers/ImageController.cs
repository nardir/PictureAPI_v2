using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WepAppAPI.Models;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Drawing.Imaging;

namespace WepAppAPI.Controllers
{
    [RoutePrefix("api/v1/images")]
    public class ImageController : ApiController
    {
        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateImage()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }

            MultipartMemoryStreamProvider provider = new MultipartMemoryStreamProvider();

            provider = await Request.Content.ReadAsMultipartAsync();

            string uri = default(string);

            foreach (HttpContent content in provider.Contents)
            {
                Stream stream = await content.ReadAsStreamAsync();
                Image image = Image.FromStream(stream);
                
                //var testName = content.Headers.ContentDisposition.Name;

                // Retrieve storage account from connection string.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("StorageConnectionString"));

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve a reference to a container. 
                CloudBlobContainer container = blobClient.GetContainerReference("images");

                // Create the container if it doesn't already exist.
                container.CreateIfNotExists();

                container.SetPermissions(new BlobContainerPermissions
                {
                    PublicAccess =
                        BlobContainerPublicAccessType.Blob
                });

                CloudBlockBlob blockBlob = container.GetBlockBlobReference("testimage2.jpg");
                using (var imageStream = new MemoryStream())
                { 
                    image.Save(imageStream, ImageFormat.Jpeg);
                    imageStream.Position = 0;

                    //await blockBlob.UploadFromStreamAsync(imageStream);

                    //using (Stream file = System.IO.File.OpenRead(@"C:\Users\Nardi\Pictures\Screenshots\147_3.jpg"))
                    //{
                    //    blockBlob.UploadFromStream(file);
                    //}

                    blockBlob.UploadFromStream(imageStream, imageStream.Length);
                }

                uri = blockBlob.Uri.AbsolutePath;
            }

            return Created<ProductImageInfo>(uri, new ProductImageInfo { Id = 100, uri = uri });
        }
    }
}
