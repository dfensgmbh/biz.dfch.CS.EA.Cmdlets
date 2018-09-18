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
using System.Diagnostics.Contracts;
using System.Linq;
using System.Management.Automation;
using EA;

namespace biz.dfch.CS.EA.Cmdlets.TaggedValues
{
    [Cmdlet(
         VerbsCommon.Get, "TaggedValue"
         ,
         ConfirmImpact = ConfirmImpact.Low
         ,
         DefaultParameterSetName = ParameterSets.DEFAULT
         ,
         SupportsShouldProcess = true
         ,
         HelpUri = "http://dfch.biz/biz/dfch/CS/EA/Cmdlets/Get-TaggedValue/"
    )]
    [OutputType(typeof(string[]), ParameterSetName = new [] {ParameterSets.DEFAULT})]
    [OutputType(typeof(string), ParameterSetName = new [] {ParameterSets.NAME})]
    public class GetTaggedValue : EnterpriseArchitectCmdletBase
    {
        public static class ParameterSets
        {
            public const string DEFAULT = nameof(DEFAULT);
            public const string NAME = nameof(NAME);
        }

        [Parameter(Mandatory = true)]
        public object Repository { get; set; }

        [Parameter(Mandatory = true, Position = 0)]
        [Alias("Id")]
        public Guid ElementGuid { get; set; }

        [Parameter(Mandatory = false, Position = 1, ParameterSetName = ParameterSets.NAME)]
        public string Name { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter ValueOnly { get; set; }

        //protected override void BeginProcessing()
        //{
        //    base.BeginProcessing();

        //    TraceSource.TraceInformation("BeginProcessing. ParameterSetName '{0}'.", ParameterSetName);
        //}

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (!ShouldProcess(ParameterSetName))
            {
                return;
            }

            var repository = GetRepository(Repository);

            var element = repository.GetElementByGuid(ElementGuid.ToString("B"));
            Contract.Assert(null != element, ElementGuid.ToString("B"));

            if (ParameterSetName.Equals(ParameterSets.NAME, StringComparison.InvariantCultureIgnoreCase))
            {
                var taggedValue = element.TaggedValues
                    .Cast<TaggedValue>()
                    .FirstOrDefault(e => e.Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase));
                if (ValueOnly)
                {
                    WriteObject(taggedValue?.Value);
                }
                else
                {
                    WriteObject(taggedValue);
                }
            }
            else
            {
                if (ValueOnly)
                {
                    var taggedValues = element.TaggedValues.Cast<TaggedValue>().Select(e => e.Value).ToList();
                    WriteObject(taggedValues);
                }
                else
                {
                    var taggedValues = element.TaggedValues.Cast<TaggedValue>().ToList();
                    WriteObject(taggedValues);
                }
            }
        }

        //protected override void EndProcessing()
        //{
        //    base.EndProcessing();

        //    TraceSource.TraceInformation("EndProcessing. ParameterSetName '{0}'.", ParameterSetName);
        //}

        //protected override void StopProcessing()
        //{
        //    base.StopProcessing();

        //    TraceSource.TraceInformation("StopProcessing. ParameterSetName '{0}'.", ParameterSetName);
        //}
    }
}
