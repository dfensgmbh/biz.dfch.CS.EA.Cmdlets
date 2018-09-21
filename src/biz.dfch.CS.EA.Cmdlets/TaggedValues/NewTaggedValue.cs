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
using System.Data;
using System.Diagnostics.Contracts;
using System.Management.Automation;
using EA;

namespace biz.dfch.CS.EA.Cmdlets.TaggedValues
{
    [Cmdlet(
         VerbsCommon.New, "TaggedValue"
         ,
         ConfirmImpact = ConfirmImpact.Medium
         ,
         DefaultParameterSetName = ParameterSets.DEFAULT
         ,
         SupportsShouldProcess = true
         ,
         HelpUri = "http://dfch.biz/biz/dfch/CS/EA/Cmdlets/New-TaggedValue/"
    )]
    [OutputType(typeof(bool), ParameterSetName = new[] { ParameterSets.DEFAULT })]
    public class NewTaggedValue : EnterpriseArchitectCmdletBase
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

        [Parameter(Mandatory = true, Position = 1)]
        public string Name { get; set; }

        [Parameter(Mandatory = false, Position = 2)]
        public string Value { get; set; }

        protected override void ProcessRecord()
        {
            new SetTaggedValue
            {
                Repository =  Repository,
                ElementGuid = ElementGuid,
                Name = Name,
                Value = Value,
                Force = false,
            }
                .Worker(this);
        }
    }
}
