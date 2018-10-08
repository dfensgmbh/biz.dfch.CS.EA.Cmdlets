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
using AutoMapper;

namespace biz.dfch.CS.EA.Cmdlets.AutoMapper
{
    /// <summary>
    /// Configuration of AutoMapper
    /// 
    /// For details see https://github.com/AutoMapper/AutoMapper/wiki/Configuration
    /// </summary>
    public class AutoMapperConfig
    {
        private static readonly Lazy<bool> _addProfile = new Lazy<bool>(() =>
        {
            Mapper.Initialize(
                config =>
                {
                    config.AddProfile<AutoMapperDefaultProfile>();
                }
            );

            return true;
        });

        private static readonly Lazy<bool> _compileMappings = new Lazy<bool>(() =>
        {
            // Compile mappings directly to avoid delay by expression compilation on first map
            Mapper.Configuration.CompileMappings();

            return true;
        });


        public static void Initialise()
        {
            Initialise(deferCompilation: true);
        }

        public static void Initialise(bool deferCompilation)
        {
            // ReSharper disable once NotAccessedVariable
            // ReSharper disable once JoinDeclarationAndInitializer
            bool result;

            result = _addProfile.Value;

            if (deferCompilation)
            {
                return;
            }

            result = _compileMappings.Value;
        }
    }
}
