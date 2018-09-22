/**
 * Copyright 2018 d-fens GmbH
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Management.Automation;
using EA;

namespace biz.dfch.CS.EA.Cmdlets.TaggedValues
{
    [Cmdlet(
         VerbsCommon.Set, "TaggedValue"
         ,
         ConfirmImpact = ConfirmImpact.Medium
         ,
         DefaultParameterSetName = ParameterSets.DEFAULT
         ,
         SupportsShouldProcess = true
         ,
         HelpUri = "http://dfch.biz/biz/dfch/CS/EA/Cmdlets/Set-TaggedValue/"
    )]
    [OutputType(typeof(TaggedValue), ParameterSetName = new[] { ParameterSets.DEFAULT })]
    public class SetTaggedValue : EnterpriseArchitectCmdletBase
    {
        public static class ParameterSets
        {
            public const string DEFAULT = nameof(DEFAULT);
        }

        [Parameter(Mandatory = true)]
        public object Repository { get; set; }

        [Parameter(Mandatory = true, Position = 0)]
        [Alias("Id")]
        public Guid ElementGuid { get; set; }

        [Parameter(Mandatory = false, Position = 1)]
        public string Name { get; set; }

        [Parameter(Mandatory = false, Position = 2)]
        public string Value { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Force { get; set; }

        protected override void ProcessRecord()
        {
            Worker();
        }

        private void Worker()
        {
            Worker(this);
        }

        internal void Worker(PSCmdlet caller)
        {
            if (!caller.ShouldProcess(Name))
            {
                return;
            }

            var repository = GetRepository(Repository);

            var element = repository.GetElementByGuid(ElementGuid.ToString("B"));
            Contract.Assert(null != element, ElementGuid.ToString("B"));

            var taggedValue = element.TaggedValues.GetByName(Name) as TaggedValue;
            if(null != taggedValue && !Force)
            {
                var ex = new DuplicateNameException(string.Format(Message.SetTaggedValue_DuplicateNameException, Name));
                caller.WriteError(new ErrorRecord(ex, GetErrorId(ex), ErrorCategory.InvalidData, taggedValue));
                return;
            }

            if (null == taggedValue)
            {
                taggedValue = element.TaggedValues.AddNew(Name, Value) as TaggedValue;
                Contract.Assert(null != taggedValue);
            }
            else
            {
                taggedValue.Value = Value;
            }

            taggedValue.Update();
            element.TaggedValues.Refresh();
            caller.WriteObject(taggedValue);
        }
    }
}
