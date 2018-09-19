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

using AutoMapper;
using EA;

namespace biz.dfch.CS.EA.Cmdlets.AutoMapper
{
    public class AutoMapperDefaultProfile : Profile
    {
        public AutoMapperDefaultProfile()
        {
            // By default, AutoMapper uses the destination type to validate members.
            // It assumes that all destination members need to be mapped. 
            // To modify this behavior, use the CreateMap overload to specify which member list to validate against:
            //
            // config.CreateMap<Source, Destination>(MemberList.Source);
            // config.CreateMap<Source2, Destination2>(MemberList.None);

            // Configuration
            CreateMap<ArbitrarySourceClass, ArbitraryDestinationClass>(MemberList.None)
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(ArbitraryDestinationClassMappingExpressionFactory<ArbitrarySourceClass>.Create))
                .ReverseMap();

            CreateMap<Diagram, Diagram>(MemberList.Destination)
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.DiagramGUID, opt => opt.MapFrom(src => src.DiagramGUID))
                .ForMember(dest => dest.DiagramID, opt => opt.MapFrom(src => src.DiagramID))
                .ForMember(dest => dest.DiagramLinks, opt => opt.MapFrom(src => src.DiagramLinks))
                .ForMember(dest => dest.DiagramObjects, opt => opt.MapFrom(src => src.DiagramObjects))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.ModifiedDate))
                .ForMember(dest => dest.SelectedConnector, opt => opt.MapFrom(src => src.SelectedConnector))
                .ForMember(dest => dest.SelectedObjects, opt => opt.MapFrom(src => src.SelectedObjects))
                ;
            //CreateMap<AuthenticationManager.LoginLogoutParameters, BasicLoginAuthInfo>(MemberList.None)
            //    .ConvertUsing<BasicLoginAuthInfoTypeConverter>();

            //CreateMap<AuthenticationManager.LoginLogoutParameters, NegotiateLoginAuthInfo>(MemberList.None)
            //    .ConvertUsing<NegotiateLoginAuthInfoTypeConverter>();

            //CreateMap<AuthenticationManager.LoginLogoutParameters, BearerAuthInfo>(MemberList.None)
            //    .ConvertUsing<BearerAuthInfoTypeConverter>();
        }
    }
}
