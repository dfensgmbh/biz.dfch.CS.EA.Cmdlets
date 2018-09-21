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
using System.Collections.Generic;
using System.Linq;
using biz.dfch.CS.EA.Cmdlets.TaggedValues;
using biz.dfch.CS.EA.Cmdlets.Tests.Stubs;
using biz.dfch.CS.Testing.Attributes;
using biz.dfch.CS.Testing.PowerShell;
using EA;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace biz.dfch.CS.EA.Cmdlets.Tests.TaggedValues
{
    [TestClass]
    public class NewTaggedValueTest
    {
        [TestMethod]
        [ExpectParameterBindingException(MessagePattern = @"Repository")]
        public void InvokeWithoutParametersThrowsParameterBindingException()
        {
            var parameters = @";";
            var results = new PsCmdletAssert2().Invoke(typeof(NewTaggedValue), parameters);
        }

        [TestMethod]
        public void AddingNewTaggedValueReturnsTrue()
        {
            var elementGuid = new Guid("deaddead-dead-dead-dead-deaddeaddead");
            var name = "new-tagged-value-name";
            var value = "42";

            var newTaggedValue = new TaggedValueStub { Name = name, Value = value };
            var taggedValues = new List<TaggedValue>
            {
                newTaggedValue
                ,
                new TaggedValueStub {Name = "other-tagged-value-name", Value = "arbitrary-value"}
            };

            var taggedValuesCollection = new CollectionStub<TaggedValue>(taggedValues)
            {
                GetByNameFunc = s => default(TaggedValue),
                AddNewFunc = (s1, s2) => newTaggedValue,
            };
            var element = Mock.Create<Element>();
            Mock.Arrange(() => element.TaggedValues)
                .Returns(taggedValuesCollection);
            var repository = Mock.Create<Repository>();
            Mock.Arrange(() => repository.ObjectType)
                .Returns(ObjectType.otRepository);
            Mock.Arrange(() => repository.GetElementByGuid(Arg.IsAny<string>()))
                .Returns(element);

            var parameters = new Dictionary<string, object>
            {
                {nameof(NewTaggedValue.Repository), repository},
                {nameof(NewTaggedValue.ElementGuid), elementGuid},
                {nameof(NewTaggedValue.Name), name},
                {nameof(NewTaggedValue.Value), value},
            };

            var results = new PsCmdletAssert2().Invoke(typeof(NewTaggedValue), parameters);
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);
            var result = (TaggedValue)results[0].BaseObject;
            Assert.AreEqual(newTaggedValue.Name, result.Name);
        }


        [TestMethod]
        public void AddingExistingTaggedValueReturnsVoid()
        {
            var elementGuid = new Guid("deaddead-dead-dead-dead-deaddeaddead");
            var name = "new-tagged-value-name";
            var value = "42";

            var newTaggedValue = new TaggedValueStub { Name = name, Value = value };
            var taggedValues = new List<TaggedValue>
            {
                newTaggedValue
                ,
                new TaggedValueStub {Name = "other-tagged-value-name", Value = "arbitrary-value"}
            };

            var taggedValuesCollection = new CollectionStub<TaggedValue>(taggedValues)
            {
                GetByNameFunc = s => newTaggedValue,
                AddNewFunc = (s1, s2) => newTaggedValue,
            };
            var element = Mock.Create<Element>();
            Mock.Arrange(() => element.TaggedValues)
                .Returns(taggedValuesCollection);
            var repository = Mock.Create<Repository>();
            Mock.Arrange(() => repository.ObjectType)
                .Returns(ObjectType.otRepository);
            Mock.Arrange(() => repository.GetElementByGuid(Arg.IsAny<string>()))
                .Returns(element);

            var parameters = new Dictionary<string, object>
            {
                {nameof(NewTaggedValue.Repository), repository},
                {nameof(NewTaggedValue.ElementGuid), elementGuid},
                {nameof(NewTaggedValue.Name), name},
                {nameof(NewTaggedValue.Value), value},
            };

            var results = new PsCmdletAssert2().Invoke(typeof(NewTaggedValue), parameters);
            Assert.IsNotNull(results);
            Assert.IsFalse(results.Any());
        }
    }
}
