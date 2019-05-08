using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CustomControlInjector.Plugin.HelperModels
{
    public class ExistingCustomControlsConfigHelper
    {
        public string Name { get; set; }
        public string FormFactor { get; set; }
        public XElement Parameters { get; set; }
        public Guid FormId { get; set; }
    }
}
