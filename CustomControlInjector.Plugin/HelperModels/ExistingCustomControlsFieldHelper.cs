using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CustomControlInjector.Plugin.HelperModels
{
    public class ExistingCustomControlsFieldHelper
    {
        public string FieldId { get; set; }
        public string DataFieldName { get; set; }
        public List<ExistingCustomControlsConfigHelper> CustomControlFields { get; set; }
        public ExistingCustomControlsFieldHelper()
        {
            CustomControlFields = new List<ExistingCustomControlsConfigHelper>();
        }

    }
}
