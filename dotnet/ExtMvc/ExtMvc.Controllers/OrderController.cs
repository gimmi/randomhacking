using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Conversation;
using ExtMvc.Data;
using ExtMvc.Domain;
using ExtMvc.Dtos;
using log4net;
using Nexida.Infrastructure;
using Nexida.Infrastructure.Mvc;

namespace ExtMvc.Controllers
{
	public class OrderController : Controller
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(OrderController));
		private readonly OrderRepository _repository;
		private readonly IMappingEngine _mapper;
		private readonly IValidator _validator;
		private readonly IConversation _conversation;
		private readonly IStringConverter<Order> _stringConverter;

		public OrderController(IConversation conversation, IMappingEngine mapper, OrderRepository repository, IValidator validator, IStringConverter<Order> stringConverter)
		{
			_conversation = conversation;
			_mapper = mapper;
			_repository = repository;
			_validator = validator;
			_stringConverter = stringConverter;
		}

		public ActionResult Save(OrderDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				Order itemMapped = _mapper.Map<OrderDto, Order>(item);
				ValidationHelpers.AddErrorsToModelState(ModelState, _validator.Validate(itemMapped), "item");
				if(ModelState.IsValid)
				{
					bool isNew = string.IsNullOrEmpty(item.StringId);
					if(isNew)
					{
						_repository.Create(itemMapped);
					}
					if(!isNew)
					{
						_repository.Update(itemMapped);
					}
					_conversation.Flush();
				}
				return Json(new{
					success = ModelState.IsValid,
					errors = ValidationHelpers.BuildErrorDictionary(ModelState),
				});
			}
		}

		public ActionResult Load(string stringId)
		{
			using(_conversation.SetAsCurrent())
			{
				Order item = _stringConverter.FromString(stringId);
				OrderDto itemDto = _mapper.Map<Order, OrderDto>(item);
				return Json(itemDto);
			}
		}

		public void Delete(string stringId)
		{
			using(_conversation.SetAsCurrent())
			{
				Order item = _stringConverter.FromString(stringId);
				_repository.Delete(item);
				_conversation.Flush();
			}
		}

		public ActionResult SearchNormal(int? orderId, DateTime? orderDate, DateTime? requiredDate, DateTime? shippedDate, decimal? freight, string shipName, string shipAddress, string shipCity, string shipRegion, string shipPostalCode, string shipCountry, CustomerReferenceDto customer, EmployeeReferenceDto employee, ShipperReferenceDto shipper, int start, int limit, string sort, string dir)
		{
			Log.DebugFormat("SearchNormal called");
			using(_conversation.SetAsCurrent())
			{
				Customer customerMapped = _mapper.Map<CustomerReferenceDto, Customer>(customer);
				Employee employeeMapped = _mapper.Map<EmployeeReferenceDto, Employee>(employee);
				Shipper shipperMapped = _mapper.Map<ShipperReferenceDto, Shipper>(shipper);

				IPresentableSet<Order> set = _repository.SearchNormal(orderId, orderDate, requiredDate, shippedDate, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, customerMapped, employeeMapped, shipperMapped);
				IEnumerable<Order> items = set.Skip(start).Take(limit).Sort(sort, dir == "ASC").AsEnumerable();
				OrderDto[] dtos = _mapper.Map<IEnumerable<Order>, OrderDto[]>(items);
				return Json(new{ items = dtos, count = set.Count() });
			}
		}
	}
}