using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using CustomControlInjector.Plugin.HelperModels;
using XrmToolBox.Extensibility;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using McTools.Xrm.Connection;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Extensions;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace CustomControlInjector.Plugin
{
    public partial class CustomControlInjectorControl : PluginControlBase
    {
        private Settings mySettings;
        private List<string> EntityTypes { get; set; }
        private List<string> BpfFieldNames { get; set; }
        private XElement BpfFullXml { get; set; }
        private Guid BpfId { get; set; }
        private string BpfUniqueName { get; set; }
        private List<ExistingCustomControlsEntityHelper> ExistingCustomControls { get; set; }

        public CustomControlInjectorControl()
        {
            InitializeComponent();
            ExistingCustomControls = new List<ExistingCustomControlsEntityHelper>();
            EntityTypes = new List<string>();
            BpfFieldNames = new List<string>();
        }

        private void MyPluginControl_Load(object sender, EventArgs e)
        {
            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
            {
                mySettings = new Settings();

                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
                LogInfo("Settings found and loaded");
            }
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }


        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            // Before leaving, save the settings
            SettingsManager.Instance.Save(GetType(), mySettings);
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            if (mySettings != null && detail != null)
            {
                mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
            }
        }

        private void loadBPF_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadBusinessProcessFlows);
        }

        private void LoadBusinessProcessFlows()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Load Business Process Flows",
                Work = (worker, args) =>
                {
                    var workflows = new QueryExpression("workflow") {ColumnSet = new ColumnSet("name", "uniquename")};
                    workflows.Criteria.AddCondition("category", ConditionOperator.Equal, 4);
                    workflows.Criteria.AddCondition("ismanaged", ConditionOperator.Equal, false);
                    args.Result = Service.RetrieveMultiple(workflows);
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (args.Result is EntityCollection result)
                    {
                        bpfList.Items.Clear();

                        foreach (var bpf in result.Entities)
                        {
                            var item = new ComboBoxItem(bpf.GetAttributeValue<string>("name"), bpf.Id)
                            {
                                UniqueName = bpf.GetAttributeValue<string>("uniquename")

                            };
                            bpfList.Items.Add(item);
                        }

                        bpfList.SelectedIndex = 0;
                        bpfList.Enabled = true;
                    }
                }
            });
        }

        private void loadBPFDetails_Click(object sender, EventArgs e)
        {
            var selectedItem = bpfList.SelectedItem;
            if (selectedItem == null)
                return;

            var selectedSolution = (ComboBoxItem) selectedItem;
            var name = selectedSolution.UniqueName;
            BpfUniqueName = selectedSolution.UniqueName;

            ExecuteMethod(LoadBusinessProcessFlowDetails, name);
        }

        private void LoadBusinessProcessFlowDetails(string bpfName)
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Load Business Process Flow details...",
                Work = (worker, args) =>
                {
                    var filteredFormsRequest = new RetrieveFilteredFormsRequest
                    {
                        EntityLogicalName = bpfName,
                        FormType = new OptionSetValue(2)
                    };

                    args.Result = (RetrieveFilteredFormsResponse) Service.Execute(filteredFormsRequest);
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (args.Result is RetrieveFilteredFormsResponse result)
                    {
                        var filteredFormsResponse = result;
                        foreach (var form in filteredFormsResponse.SystemForms)
                        {
                            var response = Service.Retrieve(form.LogicalName, form.Id, new ColumnSet(true));
                            var formXml = response.GetAttributeValue<string>("formxml");
                            BpfId = form.Id;

                            if (!string.IsNullOrEmpty(formXml))
                            {
                                var fullXml = XElement.Parse(formXml);
                                BpfFullXml = fullXml;
                                var controls = fullXml.Descendants("control");
                                foreach (var control in controls)
                                {
                                    var fieldId = control.FirstAttribute.Value;
                                    var entityName = fieldId.Substring(4, fieldId.IndexOf($"_{bpfName}", StringComparison.InvariantCultureIgnoreCase) - 4);
                                    if (!EntityTypes.Contains(entityName))
                                        EntityTypes.Add(entityName);

                                    var dataFieldName = control.Attribute("datafieldname")?.Value;
                                    if(!string.IsNullOrEmpty(dataFieldName) && !BpfFieldNames.Contains(dataFieldName))
                                        BpfFieldNames.Add(dataFieldName);
                                }
                            }
                        }

                        bpfFieldList.Items.Clear();
                        foreach (var bpfFieldName in BpfFieldNames)
                        {
                            bpfFieldList.Items.Add(bpfFieldName);
                        }

                        bpfFieldList.Enabled = true;

                        LoadEntityData();
                    }
                }
            });
        }

        private void LoadEntityData()
        {
            var logicalName = EntityTypes.FirstOrDefault();
            if (logicalName == null)
            {
                FinalizeLoading();
                return;
            }

            WorkAsync(new WorkAsyncInfo
            {
                Message = $"Loading extra details ({logicalName})...",
                Work = (worker, args) =>
                {
                    var result = new EntityMetadataHelper()
                    {
                        MetaData = Service.GetEntityMetadata(logicalName)
                    };

                    var systemFormRequest = new QueryExpression("systemform") {ColumnSet = new ColumnSet("name", "formxml") };
                    systemFormRequest.Criteria.AddCondition("type", ConditionOperator.Equal, 2);
                    systemFormRequest.Criteria.AddCondition("objecttypecode", ConditionOperator.Equal, logicalName);
                    result.FormResponse = Service.RetrieveMultiple(systemFormRequest);

                    EntityTypes.Remove(logicalName);
                    args.Result = result;
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (args.Result is EntityMetadataHelper result)
                    {
                        var helper = ExistingCustomControls.FirstOrDefault(e => e.LogicalName == result.MetaData.LogicalName);
                        if (helper == null)
                        {
                            helper = new ExistingCustomControlsEntityHelper() {LogicalName = result.MetaData.LogicalName, Metadata = result.MetaData};
                            ExistingCustomControls.Add(helper);
                        }

                        var formResponse = result.FormResponse;
                        foreach (var form in formResponse.Entities)
                        {
                            var formXml = form.GetAttributeValue<string>("formxml");
                            var fullXml = XElement.Parse(formXml);
                            var controls = fullXml.Descendants("customControl");

                            foreach (var control in controls)
                            {
                                var customControl = control.Attribute("name");
                                if (customControl != null && control.Parent != null)
                                {
                                    var forControl = control.Parent.Attribute("forControl")?.Value;
                                    var matchingControl = fullXml.Descendants("control").FirstOrDefault(a => a.Attribute("uniqueid")?.Value == forControl);
                                    if (matchingControl?.Attribute("datafieldname")?.Value != null)
                                    {
                                        var matchingFieldHelper = helper.ExistingCustomControlFields.FirstOrDefault(ec => ec.FieldId == forControl);
                                        if (matchingFieldHelper == null)
                                        {
                                            matchingFieldHelper = new ExistingCustomControlsFieldHelper()
                                            {
                                                DataFieldName = matchingControl?.Attribute("datafieldname")?.Value + " (" + form.GetAttributeValue<string>("name") + ")",
                                                FieldId = forControl,

                                            };
                                            helper.ExistingCustomControlFields.Add(matchingFieldHelper);
                                        }

                                        if (matchingFieldHelper.CustomControlFields.All(c => c.Name != customControl.Value))
                                        {
                                            var existingControlConfig = new ExistingCustomControlsConfigHelper
                                            {
                                                Name = customControl.Value,
                                                FormFactor = control.Attribute("formFactor")?.Value,
                                                Parameters = control.Descendants("parameters").FirstOrDefault(),
                                                FormId = form.Id
                                            };
                                            matchingFieldHelper.CustomControlFields.Add(existingControlConfig);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    LoadEntityData();
                }
            });
        }

        private void FinalizeLoading()
        {
            entityComboBox.Items.Clear();
            foreach (var helper in ExistingCustomControls.Where(e => e.ExistingCustomControlFields.Any()))
            {
                var item = new ComboBoxItem(helper.Metadata.DisplayName.UserLocalizedLabel.Label, helper.LogicalName);
                entityComboBox.Items.Add(item);
            }

            entityComboBox.Enabled = true;
        }

        private void entityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = (ComboBoxItem)entityComboBox.SelectedItem;
            var matchingHelper = ExistingCustomControls.First(en => en.LogicalName == (string)selectedItem.Value);

            fieldsComboBox.Items.Clear();
            foreach (var field in matchingHelper.ExistingCustomControlFields)
            {
                var item = new ComboBoxItem(field.DataFieldName, field.CustomControlFields);
                fieldsComboBox.Items.Add(item);
            }

            fieldsComboBox.Enabled = true;
        }

        private void fieldsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = (ComboBoxItem)fieldsComboBox.SelectedItem;
            var existingControls = (List<ExistingCustomControlsConfigHelper>)selectedItem.Value;

            customControlComboBox.Items.Clear();
            foreach (var existingControl in existingControls)
            {
                var item = new ComboBoxItem(existingControl.Name, existingControl);
                customControlComboBox.Items.Add(item);
            }

            customControlComboBox.Enabled = true;
        }

        private void copyAllButton_Click(object sender, EventArgs e)
        {
            CopyCustomControl(new []{0,1,2});
        }

        private void copyPhoneButton_Click(object sender, EventArgs e)
        {
        }

        private void copyTabletButton_Click(object sender, EventArgs e)
        {
        }
        private void copyWebButton_Click(object sender, EventArgs e)
        {
        }

        private void CopyCustomControl(int[] formFactors)
        {
            var uniqueId = Guid.NewGuid();

            var customControlItem = (ComboBoxItem)customControlComboBox.SelectedItem;
            var config = (ExistingCustomControlsConfigHelper) customControlItem.Value;

            var mappedField = (string) bpfFieldList.SelectedItem;
            var selectedItem = (ComboBoxItem)entityComboBox.SelectedItem;
            var matchingHelper = ExistingCustomControls.First(en => en.LogicalName == (string)selectedItem.Value);
            var matchingField = matchingHelper.ExistingCustomControlFields.FirstOrDefault(f => f.DataFieldName == mappedField);

            if(matchingField != null)
                uniqueId = new Guid(matchingField.FieldId);

            var existingControls = BpfFullXml.Descendants("control").ToList();
            foreach (var existingControl in existingControls)
            {
                if (!string.IsNullOrEmpty(existingControl.Attribute("datafieldname")?.Value) &&
                    existingControl.Attribute("datafieldname")?.Value == mappedField &&
                    !string.IsNullOrEmpty(existingControl.Attribute("uniqueid")?.Value))
                {
                    uniqueId = new Guid(existingControl.Attribute("uniqueid")?.Value);
                    break;
                }
            }

            foreach (var existingControl in existingControls)
            {
                if (!string.IsNullOrEmpty(existingControl.Attribute("datafieldname")?.Value) &&
                    existingControl.Attribute("datafieldname")?.Value == mappedField)
                {
                    if(existingControl.Attribute("uniqueid") == null)
                    {
                        existingControl.Add(new XAttribute("uniqueid", uniqueId.ToString("B")));
                    }
                    else
                    {
                        existingControl.Attribute("uniqueid").SetValue(uniqueId.ToString("B"));
                    }
                }
            }

            var existingControlDescriptions = BpfFullXml.Elements("controlDescription");
            var matchingControlDescriptions = existingControlDescriptions.Where(c =>
                !string.IsNullOrEmpty(c.Attribute("uniqueid")?.Value) && 
                c.Attribute("uniqueid")?.Value == uniqueId.ToString("B")).ToList();

            if (matchingControlDescriptions.Any())
            {
                foreach (var matchingControlDescription in matchingControlDescriptions)
                {
                    matchingControlDescription.Remove();
                }
            }

            if(BpfFullXml.Element("controlDescriptions") == null)
                BpfFullXml.Add(new XElement("controlDescriptions"));

            var controlDescriptionsElement = BpfFullXml.Element("controlDescriptions");
            var controlDescriptionElement = new XElement("controlDescription");
            controlDescriptionElement.Add(new XAttribute("forControl", uniqueId.ToString("B")));
            controlDescriptionsElement.Add(controlDescriptionElement);

            var possibleFormFactors = new int[] {0, 1, 2};
            foreach (var possibleFormFactor in possibleFormFactors)
            {
                if (formFactors.Contains(possibleFormFactor))
                {
                    var controlElement = new XElement("customControl");
                    controlElement.Add(new XAttribute("formFactor", possibleFormFactor.ToString()));
                    controlElement.Add(new XAttribute("name", config.Name));
                    controlElement.Add(config.Parameters);

                    controlDescriptionElement.Add(controlElement);
                }
            }

            var systemFormEntity = Service.Retrieve("systemform", BpfId, new ColumnSet("formxml"));
            systemFormEntity["formxml"] = BpfFullXml.ToString();
            Service.Update(systemFormEntity);
        }
    }
}


/************************************************
 * 
 * Web: formFactor: 2
 * Phone: formFactor: 0
 * Tablet: formFator: 1
 * 
 * *********************************************/

//  <control id="bpf_opportunity_cr006_custombusinessprocessflow:budgetstatus"