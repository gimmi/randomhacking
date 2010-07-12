using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using AutoMapper;

namespace ExtMvc.Dtos
{
	public static class MappingEngineBuilder
	{
		public static IMappingEngine Build()
		{
			Mapper.Reset();
			DomainToDto();
			DtoToDomain();
			Mapper.AssertConfigurationIsValid();
		    return Mapper.Engine;
		}

		private static void DomainToDto()
		{
			var sl = Microsoft.Practices.ServiceLocation.ServiceLocator.Current;

						Mapper.CreateMap<ExtMvc.Domain.Ns.Category, ExtMvc.Dtos.Ns.CategoryDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Ns.Category>>().ToString(s)));
						Mapper.CreateMap<ExtMvc.Domain.Ns.Category, ExtMvc.Dtos.Ns.CategoryReferenceDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Ns.Category>>().ToString(s)))
							.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
						
						Mapper.CreateMap<ExtMvc.Domain.Ns.CustomerDemographic, ExtMvc.Dtos.Ns.CustomerDemographicDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Ns.CustomerDemographic>>().ToString(s)));
						Mapper.CreateMap<ExtMvc.Domain.Ns.CustomerDemographic, ExtMvc.Dtos.Ns.CustomerDemographicReferenceDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Ns.CustomerDemographic>>().ToString(s)))
							.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
						
						Mapper.CreateMap<ExtMvc.Domain.Ns.Customer, ExtMvc.Dtos.Ns.CustomerDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Ns.Customer>>().ToString(s)));
						Mapper.CreateMap<ExtMvc.Domain.Ns.Customer, ExtMvc.Dtos.Ns.CustomerReferenceDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Ns.Customer>>().ToString(s)))
							.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
						
						Mapper.CreateMap<ExtMvc.Domain.Ns.Employee, ExtMvc.Dtos.Ns.EmployeeDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Ns.Employee>>().ToString(s)));
						Mapper.CreateMap<ExtMvc.Domain.Ns.Employee, ExtMvc.Dtos.Ns.EmployeeReferenceDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Ns.Employee>>().ToString(s)))
							.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
						
						Mapper.CreateMap<ExtMvc.Domain.OrderDetail, ExtMvc.Dtos.OrderDetailDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.OrderDetail>>().ToString(s)));
						Mapper.CreateMap<ExtMvc.Domain.OrderDetail, ExtMvc.Dtos.OrderDetailReferenceDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.OrderDetail>>().ToString(s)))
							.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
						
						Mapper.CreateMap<ExtMvc.Domain.Order, ExtMvc.Dtos.OrderDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Order>>().ToString(s)));
						Mapper.CreateMap<ExtMvc.Domain.Order, ExtMvc.Dtos.OrderReferenceDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Order>>().ToString(s)))
							.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
						
						Mapper.CreateMap<ExtMvc.Domain.Product, ExtMvc.Dtos.ProductDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Product>>().ToString(s)));
						Mapper.CreateMap<ExtMvc.Domain.Product, ExtMvc.Dtos.ProductReferenceDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Product>>().ToString(s)))
							.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
						
						Mapper.CreateMap<ExtMvc.Domain.Region, ExtMvc.Dtos.RegionDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Region>>().ToString(s)));
						Mapper.CreateMap<ExtMvc.Domain.Region, ExtMvc.Dtos.RegionReferenceDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Region>>().ToString(s)))
							.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
						
						Mapper.CreateMap<ExtMvc.Domain.Shipper, ExtMvc.Dtos.ShipperDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Shipper>>().ToString(s)));
						Mapper.CreateMap<ExtMvc.Domain.Shipper, ExtMvc.Dtos.ShipperReferenceDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Shipper>>().ToString(s)))
							.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
						
						Mapper.CreateMap<ExtMvc.Domain.Supplier, ExtMvc.Dtos.SupplierDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Supplier>>().ToString(s)));
						Mapper.CreateMap<ExtMvc.Domain.Supplier, ExtMvc.Dtos.SupplierReferenceDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Supplier>>().ToString(s)))
							.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
						
						Mapper.CreateMap<ExtMvc.Domain.Territory, ExtMvc.Dtos.TerritoryDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Territory>>().ToString(s)));
						Mapper.CreateMap<ExtMvc.Domain.Territory, ExtMvc.Dtos.TerritoryReferenceDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Territory>>().ToString(s)))
							.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
						
						Mapper.CreateMap<ExtMvc.Domain.Address, ExtMvc.Dtos.AddressDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Address>>().ToString(s)));
						Mapper.CreateMap<ExtMvc.Domain.Address, ExtMvc.Dtos.AddressReferenceDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Address>>().ToString(s)))
							.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
						
		}

		private static void DtoToDomain()
		{
			var sl = Microsoft.Practices.ServiceLocation.ServiceLocator.Current;

						Mapper.CreateMap<ExtMvc.Dtos.Ns.CategoryDto, ExtMvc.Domain.Ns.Category>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Ns.Category>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Ns.Category>>().FromString(s.StringId))
																							.ForMember(d => d.Picture, o => o.Ignore())

															.ForMember(d => d.Products, o => o.Ignore())

											;
						Mapper.CreateMap<ExtMvc.Dtos.Ns.CategoryReferenceDto, ExtMvc.Domain.Ns.Category>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Ns.Category>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Ns.Category>>().FromString(s.StringId))
							.ForAllMembers(o => o.Ignore());
						
						Mapper.CreateMap<ExtMvc.Dtos.Ns.CustomerDemographicDto, ExtMvc.Domain.Ns.CustomerDemographic>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Ns.CustomerDemographic>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Ns.CustomerDemographic>>().FromString(s.StringId))
																			.ForMember(d => d.Customers, o => o.Ignore())

											;
						Mapper.CreateMap<ExtMvc.Dtos.Ns.CustomerDemographicReferenceDto, ExtMvc.Domain.Ns.CustomerDemographic>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Ns.CustomerDemographic>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Ns.CustomerDemographic>>().FromString(s.StringId))
							.ForAllMembers(o => o.Ignore());
						
						Mapper.CreateMap<ExtMvc.Dtos.Ns.CustomerDto, ExtMvc.Domain.Ns.Customer>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Ns.Customer>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Ns.Customer>>().FromString(s.StringId))
																																																							.ForMember(d => d.Customerdemographics, o => o.Ignore())
											.AfterMap(delegate(ExtMvc.Dtos.Ns.CustomerDto s, ExtMvc.Domain.Ns.Customer d) {
												foreach(var item in d.Customerdemographics.ToArray())
												{
													ExtMvc.Domain.CustomerCustomerDemoAssociationSynchronizer.Disassociate(d, item)
							;
												}
												var items = Mapper.Map<ExtMvc.Dtos.Ns.CustomerDemographicDto[], ExtMvc.Domain.Ns.CustomerDemographic[]>(s.Customerdemographics);
												foreach(var item in items)
												{
													ExtMvc.Domain.CustomerCustomerDemoAssociationSynchronizer.Associate(d, item)
							;
												}
											})
															.ForMember(d => d.Orders, o => o.Ignore())

											;
						Mapper.CreateMap<ExtMvc.Dtos.Ns.CustomerReferenceDto, ExtMvc.Domain.Ns.Customer>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Ns.Customer>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Ns.Customer>>().FromString(s.StringId))
							.ForAllMembers(o => o.Ignore());
						
						Mapper.CreateMap<ExtMvc.Dtos.Ns.EmployeeDto, ExtMvc.Domain.Ns.Employee>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Ns.Employee>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Ns.Employee>>().FromString(s.StringId))
																																																																			.ForMember(d => d.Photo, o => o.Ignore())

																							.ForMember(d => d.RelatedEmployee, o => o.Ignore())

															.ForMember(d => d.Employees, o => o.Ignore())

															.ForMember(d => d.Territories, o => o.Ignore())

															.ForMember(d => d.Orders, o => o.Ignore())

											;
						Mapper.CreateMap<ExtMvc.Dtos.Ns.EmployeeReferenceDto, ExtMvc.Domain.Ns.Employee>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Ns.Employee>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Ns.Employee>>().FromString(s.StringId))
							.ForAllMembers(o => o.Ignore());
						
						Mapper.CreateMap<ExtMvc.Dtos.OrderDetailDto, ExtMvc.Domain.OrderDetail>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.OrderDetail>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.OrderDetail>>().FromString(s.StringId))
																											;
						Mapper.CreateMap<ExtMvc.Dtos.OrderDetailReferenceDto, ExtMvc.Domain.OrderDetail>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.OrderDetail>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.OrderDetail>>().FromString(s.StringId))
							.ForAllMembers(o => o.Ignore());
						
						Mapper.CreateMap<ExtMvc.Dtos.OrderDto, ExtMvc.Domain.Order>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Order>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Order>>().FromString(s.StringId))
																																																							.ForMember(d => d.Customer, o => o.Ignore())
											.AfterMap((s, d) => ExtMvc.Domain.FkOrdersCustomersAssociationSynchronizer.Associate(d, Mapper.Map<ExtMvc.Dtos.Ns.CustomerReferenceDto, ExtMvc.Domain.Ns.Customer>(s.Customer))
							)								.ForMember(d => d.Employee, o => o.Ignore())
											.AfterMap((s, d) => ExtMvc.Domain.FkOrdersEmployeesAssociationSynchronizer.Associate(d, Mapper.Map<ExtMvc.Dtos.Ns.EmployeeReferenceDto, ExtMvc.Domain.Ns.Employee>(s.Employee))
							)								.ForMember(d => d.Shipper, o => o.Ignore())

											;
						Mapper.CreateMap<ExtMvc.Dtos.OrderReferenceDto, ExtMvc.Domain.Order>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Order>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Order>>().FromString(s.StringId))
							.ForAllMembers(o => o.Ignore());
						
						Mapper.CreateMap<ExtMvc.Dtos.ProductDto, ExtMvc.Domain.Product>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Product>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Product>>().FromString(s.StringId))
																																											.ForMember(d => d.Category, o => o.Ignore())
											.AfterMap((s, d) => ExtMvc.Domain.FkProductsCategoriesAssociationSynchronizer.Associate(d, Mapper.Map<ExtMvc.Dtos.Ns.CategoryReferenceDto, ExtMvc.Domain.Ns.Category>(s.Category))
							)								.ForMember(d => d.Supplier, o => o.Ignore())

											;
						Mapper.CreateMap<ExtMvc.Dtos.ProductReferenceDto, ExtMvc.Domain.Product>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Product>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Product>>().FromString(s.StringId))
							.ForAllMembers(o => o.Ignore());
						
						Mapper.CreateMap<ExtMvc.Dtos.RegionDto, ExtMvc.Domain.Region>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Region>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Region>>().FromString(s.StringId))
																			.ForMember(d => d.Territories, o => o.Ignore())

											;
						Mapper.CreateMap<ExtMvc.Dtos.RegionReferenceDto, ExtMvc.Domain.Region>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Region>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Region>>().FromString(s.StringId))
							.ForAllMembers(o => o.Ignore());
						
						Mapper.CreateMap<ExtMvc.Dtos.ShipperDto, ExtMvc.Domain.Shipper>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Shipper>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Shipper>>().FromString(s.StringId))
																							.ForMember(d => d.Orders, o => o.Ignore())

											;
						Mapper.CreateMap<ExtMvc.Dtos.ShipperReferenceDto, ExtMvc.Domain.Shipper>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Shipper>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Shipper>>().FromString(s.StringId))
							.ForAllMembers(o => o.Ignore());
						
						Mapper.CreateMap<ExtMvc.Dtos.SupplierDto, ExtMvc.Domain.Supplier>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Supplier>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Supplier>>().FromString(s.StringId))
																																																											.ForMember(d => d.Products, o => o.Ignore())

											;
						Mapper.CreateMap<ExtMvc.Dtos.SupplierReferenceDto, ExtMvc.Domain.Supplier>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Supplier>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Supplier>>().FromString(s.StringId))
							.ForAllMembers(o => o.Ignore());
						
						Mapper.CreateMap<ExtMvc.Dtos.TerritoryDto, ExtMvc.Domain.Territory>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Territory>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Territory>>().FromString(s.StringId))
																			.ForMember(d => d.Employees, o => o.Ignore())

															.ForMember(d => d.Region, o => o.Ignore())
											.AfterMap((s, d) => ExtMvc.Domain.FkTerritoriesRegionAssociationSynchronizer.Associate(d, Mapper.Map<ExtMvc.Dtos.RegionReferenceDto, ExtMvc.Domain.Region>(s.Region))
							)				;
						Mapper.CreateMap<ExtMvc.Dtos.TerritoryReferenceDto, ExtMvc.Domain.Territory>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Territory>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Territory>>().FromString(s.StringId))
							.ForAllMembers(o => o.Ignore());
						
						Mapper.CreateMap<ExtMvc.Dtos.AddressDto, ExtMvc.Domain.Address>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Address>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Address>>().FromString(s.StringId))
																															;
						Mapper.CreateMap<ExtMvc.Dtos.AddressReferenceDto, ExtMvc.Domain.Address>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Address>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Address>>().FromString(s.StringId))
							.ForAllMembers(o => o.Ignore());
						
		}
	}
}