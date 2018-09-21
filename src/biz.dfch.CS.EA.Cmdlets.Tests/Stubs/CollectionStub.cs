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

        public Func<string, string, T> AddNewFunc { get; set; }
        public Func<string, T> GetByNameFunc { get; set; }

        public virtual object GetAt(short index)
        {
            throw new NotImplementedException();
        }

        public virtual void DeleteAt(short index, bool refresh)
        {
            throw new NotImplementedException();
        }

        public virtual string GetLastError()
        {
            throw new NotImplementedException();
        }

        public virtual object GetByName(string name)
        {
            if(null == GetByNameFunc) throw new NotImplementedException();
            return GetByNameFunc.Invoke(name);
        }

        public virtual void Refresh()
        {
            return;
        }

        public virtual object AddNew(string name, string type)
        {
            if (null == AddNewFunc) throw new NotImplementedException();
            return AddNewFunc(name, type);
        }

        public virtual void Delete(short index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IDualCollection.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public virtual short Count { get; }
        public virtual ObjectType ObjectType { get; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
