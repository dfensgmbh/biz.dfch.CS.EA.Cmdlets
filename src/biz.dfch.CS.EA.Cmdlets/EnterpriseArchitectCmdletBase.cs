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
using System.Management.Automation;
using biz.dfch.CS.Commons.Diagnostics;
using EA;

namespace biz.dfch.CS.EA.Cmdlets
{
    public abstract class EnterpriseArchitectCmdletBase : PSCmdlet
    {
        public const string TRACESOURCE_NAME = "biz.dfch.CS.EA.Cmdlets";
        private static readonly Lazy<TraceSource> _traceSource = new Lazy<TraceSource>(() => Logger.Get(TRACESOURCE_NAME));
        private readonly TraceSource traceSource;
        protected TraceSource TraceSource => traceSource ?? _traceSource.Value;

        static EnterpriseArchitectCmdletBase()
        {
            AutoMapper.AutoMapperConfig.Initialise();
        }

        protected EnterpriseArchitectCmdletBase()
        {
            // N/A
        }

        protected EnterpriseArchitectCmdletBase(TraceSource traceSource)
        {
            Contract.Requires(null != traceSource);

            this.traceSource = traceSource;
        }

        protected Repository GetRepository(object repositoryAsObject)
        {
            Contract.Requires(null != repositoryAsObject);
            Contract.Ensures(null != Contract.Result<Repository>());
            Contract.Ensures(ObjectType.otRepository == Contract.Result<Repository>().ObjectType);

            var repository = repositoryAsObject as Repository;
            if (null == repository)
            {
                if (repositoryAsObject is PSObject psObject)
                {
                    repository = psObject.BaseObject as Repository;
                }
            }

            return repository;
        }

        protected string GetErrorId(Exception exception)
        {
            var result = $"{exception.GetType().Name},{GetType().FullName}";
            return result;
        }
    }
}
