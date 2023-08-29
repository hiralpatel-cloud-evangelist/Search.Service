using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchService.DTO.Request
{
    public class BlobMetadataModel
    {
        public string BlobConnectionString { get; set; }
        public string BlobContainer { get; set; }
    }
}
