using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picture.Model
{
    public class ProductImage
    {
        public int ProductImageId {  get ; set;}
        public string Description { get; set; }
        public string Location { get; set; }

        public string Path(string imagePath)
        {
            return string.Format("{0}{1}.jpg", imagePath, ProductImageId);
        }
    }
}
