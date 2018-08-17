using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KilyCore.WEB.Model
{
    public class ExportModelBase
    {
        public List<FileText> data { get; set; }
    }
    public class FileText 
    {
        public int No { get; set; }
        public string FileTitle { get; set; }
    }
}
