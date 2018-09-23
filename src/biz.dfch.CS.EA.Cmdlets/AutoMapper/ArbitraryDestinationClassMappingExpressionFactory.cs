/**
 * Copyright 2017 d-fens GmbH
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
using System.Linq.Expressions;
using EA;

namespace biz.dfch.CS.EA.Cmdlets.AutoMapper
{
    public class ArbitrarySourceClass
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class ArbitraryDestinationClass
    {
        public string FullName { get; set; }
    }

    public static class ArbitraryDestinationClassMappingExpressionFactory<TEntity>
        where TEntity : ArbitrarySourceClass
    {
        public static Expression<Func<TEntity, ArbitraryDestinationClass>> Create = src =>
            new ArbitraryDestinationClass
            {
                FullName = $"{src.FirstName} {src.LastName}",
            };
    }

    public static class DiagramObjectMappingExpressionFactory
    {
        public static Expression<Func<DiagramObject, dynamic>> Create = src =>
            default(Expression<Func<DiagramObject, dynamic>>);
    }
}
