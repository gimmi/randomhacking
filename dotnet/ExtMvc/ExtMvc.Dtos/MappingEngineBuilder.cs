using System;
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

						Mapper.CreateMap<ExtMvc.Domain.Category, CategoryDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Category>>().ToString(s)));
						Mapper.CreateMap<ExtMvc.Domain.Category, CategoryReferenceDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Category>>().ToString(s)))
							.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
						
						Mapper.CreateMap<ExtMvc.Domain.CustomerDemographic, CustomerDemographicDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.CustomerDemographic>>().ToString(s)));
						Mapper.CreateMap<ExtMvc.Domain.CustomerDemographic, CustomerDemographicReferenceDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.CustomerDemographic>>().ToString(s)))
							.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
						
						Mapper.CreateMap<ExtMvc.Domain.Customer, CustomerDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Customer>>().ToString(s)));
						Mapper.CreateMap<ExtMvc.Domain.Customer, CustomerReferenceDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Customer>>().ToString(s)))
							.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
						
						Mapper.CreateMap<ExtMvc.Domain.Employee, EmployeeDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Employee>>().ToString(s)));
						Mapper.CreateMap<ExtMvc.Domain.Employee, EmployeeReferenceDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Employee>>().ToString(s)))
							.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
						
						Mapper.CreateMap<ExtMvc.Domain.OrderDetail, OrderDetailDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.OrderDetail>>().ToString(s)));
						Mapper.CreateMap<ExtMvc.Domain.OrderDetail, OrderDetailReferenceDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.OrderDetail>>().ToString(s)))
							.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
						
						Mapper.CreateMap<ExtMvc.Domain.Order, OrderDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Order>>().ToString(s)));
						Mapper.CreateMap<ExtMvc.Domain.Order, OrderReferenceDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Order>>().ToString(s)))
							.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
						
						Mapper.CreateMap<ExtMvc.Domain.Product, ProductDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Product>>().ToString(s)));
						Mapper.CreateMap<ExtMvc.Domain.Product, ProductReferenceDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Product>>().ToString(s)))
							.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
						
						Mapper.CreateMap<ExtMvc.Domain.Region, RegionDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Region>>().ToString(s)));
						Mapper.CreateMap<ExtMvc.Domain.Region, RegionReferenceDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Region>>().ToString(s)))
							.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
						
						Mapper.CreateMap<ExtMvc.Domain.Shipper, ShipperDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Shipper>>().ToString(s)));
						Mapper.CreateMap<ExtMvc.Domain.Shipper, ShipperReferenceDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Shipper>>().ToString(s)))
							.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
						
						Mapper.CreateMap<ExtMvc.Domain.Supplier, SupplierDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Supplier>>().ToString(s)));
						Mapper.CreateMap<ExtMvc.Domain.Supplier, SupplierReferenceDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Supplier>>().ToString(s)))
							.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
						
						Mapper.CreateMap<ExtMvc.Domain.Territory, TerritoryDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Territory>>().ToString(s)));
						Mapper.CreateMap<ExtMvc.Domain.Territory, TerritoryReferenceDto>()
							.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Territory>>().ToString(s)))
							.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
						
		}

		private static void DtoToDomain()
		{
			var sl = Microsoft.Practices.ServiceLocation.ServiceLocator.Current;

						Mapper.CreateMap<CategoryDto, ExtMvc.Domain.Category>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Category>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Category>>().FromString(s.StringId))
											.ForMember(d => d.CategoryId, o => o.Ignore())
															.ForMember(d => d.Picture, o => o.Ignore())
															.ForMember(d => d.Products, o => o.Ignore())
											;
						Mapper.CreateMap<CategoryReferenceDto, ExtMvc.Domain.Category>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Category>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Category>>().FromString(s.StringId))
							.ForAllMembers(o => o.Ignore());
						
						Mapper.CreateMap<CustomerDemographicDto, ExtMvc.Domain.CustomerDemographic>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.CustomerDemographic>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.CustomerDemographic>>().FromString(s.StringId))
											.ForMember(d => d.CustomerTypeId, o => o.Ignore())
											;
						Mapper.CreateMap<CustomerDemographicReferenceDto, ExtMvc.Domain.CustomerDemographic>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.CustomerDemographic>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.CustomerDemographic>>().FromString(s.StringId))
							.ForAllMembers(o => o.Ignore());
						
						Mapper.CreateMap<CustomerDto, ExtMvc.Domain.Customer>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Customer>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Customer>>().FromString(s.StringId))
											.ForMember(d => d.CustomerId, o => o.Ignore())
															.ForMember(d => d.Orders, o => o.Ignore())
											;
						Mapper.CreateMap<CustomerReferenceDto, ExtMvc.Domain.Customer>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Customer>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Customer>>().FromString(s.StringId))
							.ForAllMembers(o => o.Ignore());
						
						Mapper.CreateMap<EmployeeDto, ExtMvc.Domain.Employee>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Employee>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Employee>>().FromString(s.StringId))
											.ForMember(d => d.EmployeeId, o => o.Ignore())
															.ForMember(d => d.Photo, o => o.Ignore())
															.ForMember(d => d.RelatedEmployee, o => o.Ignore())
															.ForMember(d => d.Employees, o => o.Ignore())
															.ForMember(d => d.Territories, o => o.Ignore())
															.ForMember(d => d.Orders, o => o.Ignore())
											;
						Mapper.CreateMap<EmployeeReferenceDto, ExtMvc.Domain.Employee>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Employee>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Employee>>().FromString(s.StringId))
							.ForAllMembers(o => o.Ignore());
						
						Mapper.CreateMap<OrderDetailDto, ExtMvc.Domain.OrderDetail>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.OrderDetail>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.OrderDetail>>().FromString(s.StringId))
											.ForMember(d => d.OrderId, o => o.Ignore())
															.ForMember(d => d.ProductId, o => o.Ignore())
											;
						Mapper.CreateMap<OrderDetailReferenceDto, ExtMvc.Domain.OrderDetail>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.OrderDetail>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.OrderDetail>>().FromString(s.StringId))
							.ForAllMembers(o => o.Ignore());
						
						Mapper.CreateMap<OrderDto, ExtMvc.Domain.Order>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Order>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Order>>().FromString(s.StringId))
											.ForMember(d => d.OrderId, o => o.Ignore())
															.ForMember(d => d.Shipper, o => o.Ignore())
											;
						Mapper.CreateMap<OrderReferenceDto, ExtMvc.Domain.Order>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Order>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Order>>().FromString(s.StringId))
							.ForAllMembers(o => o.Ignore());
						
						Mapper.CreateMap<ProductDto, ExtMvc.Domain.Product>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Product>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Product>>().FromString(s.StringId))
											.ForMember(d => d.ProductId, o => o.Ignore())
															.ForMember(d => d.Supplier, o => o.Ignore())
											;
						Mapper.CreateMap<ProductReferenceDto, ExtMvc.Domain.Product>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Product>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Product>>().FromString(s.StringId))
							.ForAllMembers(o => o.Ignore());
						
						Mapper.CreateMap<RegionDto, ExtMvc.Domain.Region>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Region>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Region>>().FromString(s.StringId))
											.ForMember(d => d.RegionId, o => o.Ignore())
															.ForMember(d => d.Territories, o => o.Ignore())
											;
						Mapper.CreateMap<RegionReferenceDto, ExtMvc.Domain.Region>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Region>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Region>>().FromString(s.StringId))
							.ForAllMembers(o => o.Ignore());
						
						Mapper.CreateMap<ShipperDto, ExtMvc.Domain.Shipper>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Shipper>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Shipper>>().FromString(s.StringId))
											.ForMember(d => d.ShipperId, o => o.Ignore())
															.ForMember(d => d.Orders, o => o.Ignore())
											;
						Mapper.CreateMap<ShipperReferenceDto, ExtMvc.Domain.Shipper>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Shipper>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Shipper>>().FromString(s.StringId))
							.ForAllMembers(o => o.Ignore());
						
						Mapper.CreateMap<SupplierDto, ExtMvc.Domain.Supplier>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Supplier>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Supplier>>().FromString(s.StringId))
											.ForMember(d => d.SupplierId, o => o.Ignore())
															.ForMember(d => d.Products, o => o.Ignore())
											;
						Mapper.CreateMap<SupplierReferenceDto, ExtMvc.Domain.Supplier>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Supplier>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Supplier>>().FromString(s.StringId))
							.ForAllMembers(o => o.Ignore());
						
						Mapper.CreateMap<TerritoryDto, ExtMvc.Domain.Territory>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Territory>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Territory>>().FromString(s.StringId))
											.ForMember(d => d.TerritoryId, o => o.Ignore())
															.ForMember(d => d.Employees, o => o.Ignore())
											;
						Mapper.CreateMap<TerritoryReferenceDto, ExtMvc.Domain.Territory>()
							.ConstructUsing(s => string.IsNullOrEmpty(s.StringId) ? sl.GetInstance<Nexida.Infrastructure.IFactory<ExtMvc.Domain.Territory>>().Create() : sl.GetInstance<Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Territory>>().FromString(s.StringId))
							.ForAllMembers(o => o.Ignore());
						
		}
	}
}