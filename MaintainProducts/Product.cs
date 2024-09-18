using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaintainProducts
{
    public class Product
    {
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public DateTime ReleaseDate { get; set; }

        public Product()
        {

        }

        public Product(string productCode, string name, string version, DateTime releaseDate)
        {
            ProductCode = productCode;
            Name = name;
            Version = version;
            ReleaseDate = releaseDate;
        }
        public Product( string name, string version, DateTime releaseDate)
        {
            
            Name = name;
            Version = version;
            ReleaseDate = releaseDate;
        }

        public override string ToString()
        {
            // Format each field with fixed width
            return $"{ProductCode,-15}\t{Name,-20}\t{Version,-10}\t{ReleaseDate:yyyy-MM-dd}";
        }
    }
}
