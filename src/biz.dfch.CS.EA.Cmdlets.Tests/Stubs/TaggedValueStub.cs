/**
 * Copyright 2016 d-fens GmbH
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
using EA;

namespace biz.dfch.CS.EA.Cmdlets.Tests.Stubs
{
    public class TaggedValueStub : TaggedValue
    {
        public bool Update()
        {
            throw new NotImplementedException();
        }

        public string GetLastError()
        {
            throw new NotImplementedException();
        }

        public string GetAttribute(string propName)
        {
            throw new NotImplementedException();
        }

        public bool SetAttribute(string propName, string propValue)
        {
            throw new NotImplementedException();
        }

        public bool HasAttributes()
        {
            throw new NotImplementedException();
        }

        public int PropertyID { get; }
        public string PropertyGUID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Notes { get; set; }
        public int ElementID { get; set; }
        public ObjectType ObjectType { get; }
        public int ParentID { get; }
        public string FQName { get; }
    }
}
