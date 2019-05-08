using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace CustomControlInjector.Plugin.HelperModels
{
    public class EntityMetadataHelper
    {
        public EntityMetadata MetaData { get; set; }
        public EntityCollection FormResponse { get; set; }
    }
}
