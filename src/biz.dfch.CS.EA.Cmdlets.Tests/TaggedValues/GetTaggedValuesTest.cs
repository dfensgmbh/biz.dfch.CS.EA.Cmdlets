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
    public class TestCmdlet5Test
    {
        [TestMethod]
        [ExpectParameterBindingException(MessagePattern = @"Repository")]
        public void InvokeWithoutParametersThrowsParameterBindingException()
        {
            var parameters = @";";
            var results = new PsCmdletAssert2().Invoke(typeof(GetTaggedValue), parameters);
        }

        [TestMethod]
        public void InvokeWithRequiredComplexParameterSucceeds()
        {
            var elementGuid = new Guid("deaddead-dead-dead-dead-deaddeaddead");
            var name = "existing-tagged-value-name";
            var expected = "42";

            var taggedValues = new List<TaggedValue>
            {
                new TaggedValueStub {Name = name, Value = expected}
                ,
                new TaggedValueStub {Name = "other-tagged-value-name", Value = "arbitrary-value"}
            };

            var element = Mock.Create<Element>();
            Mock.Arrange(() => element.TaggedValues)
                .Returns(new CollectionStub<TaggedValue>(taggedValues));
            var repository = Mock.Create<Repository>();
            Mock.Arrange(() => repository.ObjectType)
                .Returns(ObjectType.otRepository);
            Mock.Arrange(() => repository.GetElementByGuid(Arg.IsAny<string>()))
                .Returns(element);

            var parameters = new Dictionary<string, object>
            {
                {nameof(GetTaggedValue.Repository), repository}
                ,
                {nameof(GetTaggedValue.ElementGuid), elementGuid}
                ,
                {nameof(GetTaggedValue.Name), name}
                ,
                {nameof(GetTaggedValue.ValueOnly), true}
            };

            var results = new PsCmdletAssert2().Invoke(typeof(GetTaggedValue), parameters);
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);
            var result = results[0].ToString();
            Assert.AreEqual(expected, result);
        }
    }
}
