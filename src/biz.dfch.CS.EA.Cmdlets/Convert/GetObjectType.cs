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
using biz.dfch.CS.EA.Cmdlets.Constants;
using EA;

namespace biz.dfch.CS.EA.Cmdlets.Convert
{
    [Cmdlet(
         VerbsCommon.Get, "ObjectType"
         ,
         ConfirmImpact = ConfirmImpact.None
         ,
         DefaultParameterSetName = ParameterSets.DEFAULT
         ,
         SupportsShouldProcess = false
         ,
         HelpUri = "http://dfch.biz/biz/dfch/CS/EA/Cmdlets/Get-ObjectType/"
    )]
    [OutputType(typeof(ObjectType), ParameterSetName = new[] { ParameterSets.ID, ParameterSets.NAME })]
    [OutputType(typeof(ObjectType[]), ParameterSetName = new[] { ParameterSets.DEFAULT, ParameterSets.NAME })]
    public class GetObjectType : EnterpriseArchitectCmdletBase
    {
        public static class ParameterSets
        {
            public const string DEFAULT = nameof(DEFAULT);
            public const string ID = nameof(ID);
            public const string NAME = nameof(NAME);
        }

        [ValidateRange(0, int.MaxValue)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSets.ID, HelpMessage = Help.GetObjectType.PARAMETER_VALUE)]
        [Alias("Id")]
        public int Value { get; set; }

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSets.NAME, HelpMessage = Help.GetObjectType.PARAMETER_NAME)]
        public string Name { get; set; }

        [PSDefaultValue(Value = true)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSets.DEFAULT, HelpMessage = Help.GetObjectType.PARAMETER_LIST_AVAILABLE)]
        public SwitchParameter ListAvailable { get; set; } = true;

        protected override void ProcessRecord()
        {
            if (ParameterSetName == ParameterSets.DEFAULT)
            {
                var list = Enum.GetNames(typeof(ObjectType))
                    .Select(e => (ObjectType)Enum.Parse(typeof(ObjectType), e))
                    .ToArray();
                WriteObject(list);
                return;
            }

            ObjectType result;
            if (ParameterSetName == ParameterSets.ID)
            {
                var isValidValue = Enum.TryParse(Value.ToString(), true, out result);
                Contract.Assert(isValidValue, Value.ToString());
                Contract.Assert(Enum.GetValues(typeof(ObjectType)).Cast<int>().Contains(Value), Value.ToString());
                WriteObject(result);
                return;
            }

            Contract.Assert(ParameterSets.NAME == ParameterSetName);
            if (Enum.TryParse(Name, true, out result))
            {
                WriteObject(result);
                return;
            }
            
            var names = Enum.GetNames(typeof(ObjectType))
                .Where(e => -1 != e.IndexOf(Name, StringComparison.InvariantCultureIgnoreCase))
                .Select(e => (ObjectType) Enum.Parse(typeof(ObjectType), e))
                .ToArray();
            if (1 == names.Length)
            {
                WriteObject(names[0]);
            }
            else
            {
                WriteObject(names);
            }
        }
    }
}
