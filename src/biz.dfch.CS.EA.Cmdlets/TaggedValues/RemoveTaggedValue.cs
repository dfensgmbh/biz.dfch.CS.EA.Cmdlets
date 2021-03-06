﻿/**
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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Management.Automation;
using EA;

namespace biz.dfch.CS.EA.Cmdlets.TaggedValues
{
    [Cmdlet(
         VerbsCommon.Remove, "TaggedValue"
         ,
         ConfirmImpact = ConfirmImpact.High
         ,
         DefaultParameterSetName = ParameterSets.DEFAULT
         ,
         SupportsShouldProcess = true
         ,
         HelpUri = "http://dfch.biz/biz/dfch/CS/EA/Cmdlets/Remove-TaggedValue/"
    )]
    [OutputType(typeof(bool), ParameterSetName = new[] { ParameterSets.DEFAULT })]
    public class RemoveTaggedValue : EnterpriseArchitectCmdletBase
    {
        public static class ParameterSets
        {
            public const string DEFAULT = nameof(DEFAULT);
            public const string ID = nameof(ID);
        }

        [Parameter(Mandatory = true)]
        public object Repository { get; set; }

        [Parameter(Mandatory = true, Position = 0)]
        [Alias("Id")]
        public Guid ElementGuid { get; set; }

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = ParameterSets.DEFAULT)]
        public string Name { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSets.ID)]
        public Guid PropertyGuid { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (!ShouldProcess(Name))
            {
                return;
            }

            var repository = GetRepository(Repository);

            var element = repository.GetElementByGuid(ElementGuid.AsString());
            Contract.Assert(null != element, ElementGuid.AsString());

            if (ParameterSets.ID == ParameterSetName)
            {
                foreach (TaggedValue elementTaggedValue in element.TaggedValues)
                {
                    var propertyGuid = new Guid(elementTaggedValue.PropertyGUID);
                    if (PropertyGuid != propertyGuid) continue;

                    Name = elementTaggedValue.Name;
                    break;
                }
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                var ex = new KeyNotFoundException(string.Format(Message.RemoveTaggedValue_PropertyGuidFoundException, Name));
                WriteError(new ErrorRecord(ex, GetErrorId(ex), ErrorCategory.ObjectNotFound, Name));
                WriteObject(false);
                return;
            }

            var taggedValue = element.TaggedValues.GetByName(Name) as TaggedValue;
            if (null == taggedValue)
            {
                var ex = new KeyNotFoundException(string.Format(Message.RemoveTaggedValue_NameNotFoundException, Name));
                WriteError(new ErrorRecord(ex, GetErrorId(ex), ErrorCategory.ObjectNotFound, Name));
                WriteObject(false);
                return;
            }

            for (var c = (short)(element.TaggedValues.Count -1); c >= 0; c--)
            {
                taggedValue = element.TaggedValues.GetAt(c) as TaggedValue;
                Contract.Assert(null != taggedValue);

                if (Name != taggedValue.Name)
                {
                    continue;
                }

                element.TaggedValues.DeleteAt(c, true);
                element.TaggedValues.Refresh();
                WriteObject(true);
                return;
            }
        }
    }
}
