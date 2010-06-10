using AutoMapper;
using ExtMvc.Domain;
using Microsoft.Practices.ServiceLocation;
using Nexida.Infrastructure;

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
			IServiceLocator sl = ServiceLocator.Current;

			Mapper.CreateMap<Category, CategoryDto>()
				.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<IStringConverter<Category>>().ToString(s)));
			Mapper.CreateMap<Category, CategoryReferenceDto>()
				.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<IStringConverter<Category>>().ToString(s)))
				.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));

			Mapper.CreateMap<CustomerDemographic, CustomerDemographicDto>()
				.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<IStringConverter<CustomerDemographic>>().ToString(s)));
			Mapper.CreateMap<CustomerDemographic, CustomerDemographicReferenceDto>()
				.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<IStringConverter<CustomerDemographic>>().ToString(s)))
				.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));

			Mapper.CreateMap<Customer, CustomerDto>()
				.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<IStringConverter<Customer>>().ToString(s)));
			Mapper.CreateMap<Customer, CustomerReferenceDto>()
				.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<IStringConverter<Customer>>().ToString(s)))
				.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));

			Mapper.CreateMap<Employee, EmployeeDto>()
				.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<IStringConverter<Employee>>().ToString(s)));
			Mapper.CreateMap<Employee, EmployeeReferenceDto>()
				.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<IStringConverter<Employee>>().ToString(s)))
				.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));

			Mapper.CreateMap<OrderDetail, OrderDetailDto>()
				.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<IStringConverter<OrderDetail>>().ToString(s)));
			Mapper.CreateMap<OrderDetail, OrderDetailReferenceDto>()
				.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<IStringConverter<OrderDetail>>().ToString(s)))
				.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));

			Mapper.CreateMap<Order, OrderDto>()
				.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<IStringConverter<Order>>().ToString(s)));
			Mapper.CreateMap<Order, OrderReferenceDto>()
				.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<IStringConverter<Order>>().ToString(s)))
				.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));

			Mapper.CreateMap<Product, ProductDto>()
				.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<IStringConverter<Product>>().ToString(s)));
			Mapper.CreateMap<Product, ProductReferenceDto>()
				.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<IStringConverter<Product>>().ToString(s)))
				.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));

			Mapper.CreateMap<Region, RegionDto>()
				.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<IStringConverter<Region>>().ToString(s)));
			Mapper.CreateMap<Region, RegionReferenceDto>()
				.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<IStringConverter<Region>>().ToString(s)))
				.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));

			Mapper.CreateMap<Shipper, ShipperDto>()
				.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<IStringConverter<Shipper>>().ToString(s)));
			Mapper.CreateMap<Shipper, ShipperReferenceDto>()
				.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<IStringConverter<Shipper>>().ToString(s)))
				.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));

			Mapper.CreateMap<Supplier, SupplierDto>()
				.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<IStringConverter<Supplier>>().ToString(s)));
			Mapper.CreateMap<Supplier, SupplierReferenceDto>()
				.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<IStringConverter<Supplier>>().ToString(s)))
				.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));

			Mapper.CreateMap<Territory, TerritoryDto>()
				.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<IStringConverter<Territory>>().ToString(s)));
			Mapper.CreateMap<Territory, TerritoryReferenceDto>()
				.ForMember(d => d.StringId, o => o.MapFrom(s => sl.GetInstance<IStringConverter<Territory>>().ToString(s)))
				.ForMember(d => d.Description, o => o.MapFrom(s => s.ToString()));
		}

		private static void DtoToDomain()
		{
			IServiceLocator sl = ServiceLocator.Current;

			Mapper.CreateMap<CategoryDto, Category>()
				.ConstructUsing(s => string.IsNullOrWhiteSpace(s.StringId) ? sl.GetInstance<IFactory<Category>>().Create() : sl.GetInstance<IStringConverter<Category>>().FromString(s.StringId))
				.ForMember(d => d.CategoryId, o => o.Ignore())
				.ForMember(d => d.Picture, o => o.Ignore())
				.ForMember(d => d.Products, o => o.Ignore())
				;
			Mapper.CreateMap<CategoryReferenceDto, Category>()
				.ConstructUsing(s => string.IsNullOrWhiteSpace(s.StringId) ? sl.GetInstance<IFactory<Category>>().Create() : sl.GetInstance<IStringConverter<Category>>().FromString(s.StringId))
				.ForAllMembers(o => o.Ignore());

			Mapper.CreateMap<CustomerDemographicDto, CustomerDemographic>()
				.ConstructUsing(s => string.IsNullOrWhiteSpace(s.StringId) ? sl.GetInstance<IFactory<CustomerDemographic>>().Create() : sl.GetInstance<IStringConverter<CustomerDemographic>>().FromString(s.StringId))
				.ForMember(d => d.CustomerTypeId, o => o.Ignore())
				;
			Mapper.CreateMap<CustomerDemographicReferenceDto, CustomerDemographic>()
				.ConstructUsing(s => string.IsNullOrWhiteSpace(s.StringId) ? sl.GetInstance<IFactory<CustomerDemographic>>().Create() : sl.GetInstance<IStringConverter<CustomerDemographic>>().FromString(s.StringId))
				.ForAllMembers(o => o.Ignore());

			Mapper.CreateMap<CustomerDto, Customer>()
				.ConstructUsing(s => string.IsNullOrWhiteSpace(s.StringId) ? sl.GetInstance<IFactory<Customer>>().Create() : sl.GetInstance<IStringConverter<Customer>>().FromString(s.StringId))
				.ForMember(d => d.CustomerId, o => o.Ignore())
				.ForMember(d => d.Orders, o => o.Ignore())
				;
			Mapper.CreateMap<CustomerReferenceDto, Customer>()
				.ConstructUsing(s => string.IsNullOrWhiteSpace(s.StringId) ? sl.GetInstance<IFactory<Customer>>().Create() : sl.GetInstance<IStringConverter<Customer>>().FromString(s.StringId))
				.ForAllMembers(o => o.Ignore());

			Mapper.CreateMap<EmployeeDto, Employee>()
				.ConstructUsing(s => string.IsNullOrWhiteSpace(s.StringId) ? sl.GetInstance<IFactory<Employee>>().Create() : sl.GetInstance<IStringConverter<Employee>>().FromString(s.StringId))
				.ForMember(d => d.EmployeeId, o => o.Ignore())
				.ForMember(d => d.Photo, o => o.Ignore())
				.ForMember(d => d.RelatedEmployee, o => o.Ignore())
				.ForMember(d => d.Employees, o => o.Ignore())
				.ForMember(d => d.Territories, o => o.Ignore())
				.ForMember(d => d.Orders, o => o.Ignore())
				;
			Mapper.CreateMap<EmployeeReferenceDto, Employee>()
				.ConstructUsing(s => string.IsNullOrWhiteSpace(s.StringId) ? sl.GetInstance<IFactory<Employee>>().Create() : sl.GetInstance<IStringConverter<Employee>>().FromString(s.StringId))
				.ForAllMembers(o => o.Ignore());

			Mapper.CreateMap<OrderDetailDto, OrderDetail>()
				.ConstructUsing(s => string.IsNullOrWhiteSpace(s.StringId) ? sl.GetInstance<IFactory<OrderDetail>>().Create() : sl.GetInstance<IStringConverter<OrderDetail>>().FromString(s.StringId))
				.ForMember(d => d.OrderId, o => o.Ignore())
				.ForMember(d => d.ProductId, o => o.Ignore())
				;
			Mapper.CreateMap<OrderDetailReferenceDto, OrderDetail>()
				.ConstructUsing(s => string.IsNullOrWhiteSpace(s.StringId) ? sl.GetInstance<IFactory<OrderDetail>>().Create() : sl.GetInstance<IStringConverter<OrderDetail>>().FromString(s.StringId))
				.ForAllMembers(o => o.Ignore());

			Mapper.CreateMap<OrderDto, Order>()
				.ConstructUsing(s => string.IsNullOrWhiteSpace(s.StringId) ? sl.GetInstance<IFactory<Order>>().Create() : sl.GetInstance<IStringConverter<Order>>().FromString(s.StringId))
				.ForMember(d => d.OrderId, o => o.Ignore())
				.ForMember(d => d.Shipper, o => o.Ignore())
				;
			Mapper.CreateMap<OrderReferenceDto, Order>()
				.ConstructUsing(s => string.IsNullOrWhiteSpace(s.StringId) ? sl.GetInstance<IFactory<Order>>().Create() : sl.GetInstance<IStringConverter<Order>>().FromString(s.StringId))
				.ForAllMembers(o => o.Ignore());

			Mapper.CreateMap<ProductDto, Product>()
				.ConstructUsing(s => string.IsNullOrWhiteSpace(s.StringId) ? sl.GetInstance<IFactory<Product>>().Create() : sl.GetInstance<IStringConverter<Product>>().FromString(s.StringId))
				.ForMember(d => d.ProductId, o => o.Ignore())
				.ForMember(d => d.Supplier, o => o.Ignore())
				;
			Mapper.CreateMap<ProductReferenceDto, Product>()
				.ConstructUsing(s => string.IsNullOrWhiteSpace(s.StringId) ? sl.GetInstance<IFactory<Product>>().Create() : sl.GetInstance<IStringConverter<Product>>().FromString(s.StringId))
				.ForAllMembers(o => o.Ignore());

			Mapper.CreateMap<RegionDto, Region>()
				.ConstructUsing(s => string.IsNullOrWhiteSpace(s.StringId) ? sl.GetInstance<IFactory<Region>>().Create() : sl.GetInstance<IStringConverter<Region>>().FromString(s.StringId))
				.ForMember(d => d.RegionId, o => o.Ignore())
				.ForMember(d => d.Territories, o => o.Ignore())
				;
			Mapper.CreateMap<RegionReferenceDto, Region>()
				.ConstructUsing(s => string.IsNullOrWhiteSpace(s.StringId) ? sl.GetInstance<IFactory<Region>>().Create() : sl.GetInstance<IStringConverter<Region>>().FromString(s.StringId))
				.ForAllMembers(o => o.Ignore());

			Mapper.CreateMap<ShipperDto, Shipper>()
				.ConstructUsing(s => string.IsNullOrWhiteSpace(s.StringId) ? sl.GetInstance<IFactory<Shipper>>().Create() : sl.GetInstance<IStringConverter<Shipper>>().FromString(s.StringId))
				.ForMember(d => d.ShipperId, o => o.Ignore())
				.ForMember(d => d.Orders, o => o.Ignore())
				;
			Mapper.CreateMap<ShipperReferenceDto, Shipper>()
				.ConstructUsing(s => string.IsNullOrWhiteSpace(s.StringId) ? sl.GetInstance<IFactory<Shipper>>().Create() : sl.GetInstance<IStringConverter<Shipper>>().FromString(s.StringId))
				.ForAllMembers(o => o.Ignore());

			Mapper.CreateMap<SupplierDto, Supplier>()
				.ConstructUsing(s => string.IsNullOrWhiteSpace(s.StringId) ? sl.GetInstance<IFactory<Supplier>>().Create() : sl.GetInstance<IStringConverter<Supplier>>().FromString(s.StringId))
				.ForMember(d => d.SupplierId, o => o.Ignore())
				.ForMember(d => d.Products, o => o.Ignore())
				;
			Mapper.CreateMap<SupplierReferenceDto, Supplier>()
				.ConstructUsing(s => string.IsNullOrWhiteSpace(s.StringId) ? sl.GetInstance<IFactory<Supplier>>().Create() : sl.GetInstance<IStringConverter<Supplier>>().FromString(s.StringId))
				.ForAllMembers(o => o.Ignore());

			Mapper.CreateMap<TerritoryDto, Territory>()
				.ConstructUsing(s => string.IsNullOrWhiteSpace(s.StringId) ? sl.GetInstance<IFactory<Territory>>().Create() : sl.GetInstance<IStringConverter<Territory>>().FromString(s.StringId))
				.ForMember(d => d.TerritoryId, o => o.Ignore())
				.ForMember(d => d.Employees, o => o.Ignore())
				;
			Mapper.CreateMap<TerritoryReferenceDto, Territory>()
				.ConstructUsing(s => string.IsNullOrWhiteSpace(s.StringId) ? sl.GetInstance<IFactory<Territory>>().Create() : sl.GetInstance<IStringConverter<Territory>>().FromString(s.StringId))
				.ForAllMembers(o => o.Ignore());
		}
	}
}