using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Metadata;

namespace CustomControlInjector.Plugin.HelperModels
{
    public class ExistingCustomControlsEntityHelper
    {
        public string LogicalName { get; set; }
        public EntityMetadata Metadata { get; set; }
        public List<ExistingCustomControlsFieldHelper> ExistingCustomControlFields { get; set; }

        public ExistingCustomControlsEntityHelper()
        {
            ExistingCustomControlFields = new List<ExistingCustomControlsFieldHelper>();
        }
    }
}
