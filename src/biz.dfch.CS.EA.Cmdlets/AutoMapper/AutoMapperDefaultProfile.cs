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
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.DiagramGUID, opt => opt.Ignore())
                .ForMember(dest => dest.DiagramID, opt => opt.Ignore())
                .ForMember(dest => dest.DiagramLinks, opt => opt.Ignore())
                .ForMember(dest => dest.DiagramObjects, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.SelectedConnector, opt => opt.Ignore())
                .ForMember(dest => dest.SelectedObjects, opt => opt.Ignore())
                ;

            CreateMap<DiagramObject, DiagramObject>(MemberList.Destination)
                .ForMember(dest => dest.DiagramID, opt => opt.Ignore())
                .ForMember(dest => dest.ElementID, opt => opt.Ignore())
                .ForMember(dest => dest.InstanceGUID, opt => opt.Ignore())
                .ForMember(dest => dest.InstanceID, opt => opt.Ignore())
                .ForMember(dest => dest.Sequence, opt => opt.Ignore())
                .ForMember(dest => dest.Style, opt => opt.MapFrom(DiagramObjectMappingExpressionFactory.Create))
                .ForAllMembers(opt => opt.Ignore())
                ;
            //CreateMap<DiagramObject, DiagramObject>(MemberList.Destination)
            //    .ForMember(dest => dest.DiagramID, opt => opt.Ignore())
            //    .ForMember(dest => dest.ElementID, opt => opt.Ignore())
            //    .ForMember(dest => dest.InstanceGUID, opt => opt.Ignore())
            //    .ForMember(dest => dest.InstanceID, opt => opt.Ignore())
            //    ;
            //CreateMap<AuthenticationManager.LoginLogoutParameters, BasicLoginAuthInfo>(MemberList.None)
            //    .ConvertUsing<BasicLoginAuthInfoTypeConverter>();

            //CreateMap<AuthenticationManager.LoginLogoutParameters, NegotiateLoginAuthInfo>(MemberList.None)
            //    .ConvertUsing<NegotiateLoginAuthInfoTypeConverter>();

            //CreateMap<AuthenticationManager.LoginLogoutParameters, BearerAuthInfo>(MemberList.None)
            //    .ConvertUsing<BearerAuthInfoTypeConverter>();
        }
    }
}
