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
using System.Collections;
using System.Collections.Generic;
using EA;

namespace biz.dfch.CS.EA.Cmdlets.Tests.Stubs
{
    public class CollectionStub<T> : Collection
        where T : class
    {
        private readonly IList<T> list;

        public CollectionStub()
            : this(new List<T>())
        {
        }

        public CollectionStub(IList<T> list)
        {
            this.list = list;
        }

        public object GetAt(short index)
        {
            throw new NotImplementedException();
        }

        public void DeleteAt(short index, bool refresh)
        {
            throw new NotImplementedException();
        }

        public string GetLastError()
        {
            throw new NotImplementedException();
        }

        public object GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public object AddNew(string name, string type)
        {
            throw new NotImplementedException();
        }

        public void Delete(short index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IDualCollection.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public short Count { get; }
        public ObjectType ObjectType { get; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
