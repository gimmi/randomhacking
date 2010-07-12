using System.Web.Mvc;
using System.Collections.Generic;

namespace ExtMvc.Controllers
{
	public class OrderController : Controller
	{
		private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ExtMvc.Controllers.OrderController));
		private readonly ExtMvc.Data.OrderRepository _repository;
		private readonly AutoMapper.IMappingEngine _mapper;
		private readonly Nexida.Infrastructure.IValidator _validator;
		private readonly Conversation.IConversation _conversation;
		private readonly Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Order> _stringConverter;

		public OrderController(Conversation.IConversation conversation, AutoMapper.IMappingEngine mapper, ExtMvc.Data.OrderRepository repository, Nexida.Infrastructure.IValidator validator, Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Order> stringConverter)
		{
			_conversation = conversation;
			_mapper = mapper;
			_repository = repository;
			_validator = validator;
			_stringConverter = stringConverter;
		}

		public ActionResult Save(ExtMvc.Dtos.OrderDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				var itemMapped = _mapper.Map<ExtMvc.Dtos.OrderDto, ExtMvc.Domain.Order>(item);
				Nexida.Infrastructure.Mvc.ValidationHelpers.AddErrorsToModelState(ModelState, _validator.Validate(itemMapped), "item");
				if (ModelState.IsValid)
				{
					var isNew = string.IsNullOrEmpty(item.StringId);
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
					errors = Nexida.Infrastructure.Mvc.ValidationHelpers.BuildErrorDictionary(ModelState),
				});
			}
		}

		public ActionResult Load(string stringId)
		{
			using(_conversation.SetAsCurrent())
			{
				var item = _stringConverter.FromString(stringId);
				var itemDto = _mapper.Map<ExtMvc.Domain.Order, ExtMvc.Dtos.OrderDto>(item);
				return Json(itemDto);
			}
		}

		public void Delete(string stringId)
		{
			using(_conversation.SetAsCurrent())
			{
				var item = _stringConverter.FromString(stringId);
				_repository.Delete(item);
				_conversation.Flush();
			}
		}

				public ActionResult SearchNormal(int? orderId, System.DateTime? orderDate, System.DateTime? requiredDate, System.DateTime? shippedDate, decimal? freight, ExtMvc.Dtos.AddressDto address, ExtMvc.Dtos.Ns.CustomerReferenceDto customer, ExtMvc.Dtos.Ns.EmployeeReferenceDto employee, ExtMvc.Dtos.ShipperReferenceDto shipper, int start, int limit, string sort, string dir)
				{
					Log.DebugFormat("SearchNormal called");
					using(_conversation.SetAsCurrent())
					{
																														var addressMapped = _mapper.Map<ExtMvc.Dtos.AddressDto, ExtMvc.Domain.Address>(address);
														var customerMapped = _mapper.Map<ExtMvc.Dtos.Ns.CustomerReferenceDto, ExtMvc.Domain.Ns.Customer>(customer);
														var employeeMapped = _mapper.Map<ExtMvc.Dtos.Ns.EmployeeReferenceDto, ExtMvc.Domain.Ns.Employee>(employee);
														var shipperMapped = _mapper.Map<ExtMvc.Dtos.ShipperReferenceDto, ExtMvc.Domain.Shipper>(shipper);
										
						var set = _repository.SearchNormal(orderId, orderDate, requiredDate, shippedDate, freight, addressMapped, customerMapped, employeeMapped, shipperMapped);
						var items = set.Skip(start).Take(limit).Sort(sort, dir == "ASC").AsEnumerable();
						var dtos = _mapper.Map<IEnumerable<ExtMvc.Domain.Order>, ExtMvc.Dtos.OrderDto[]>(items);
						return Json(new{ items = dtos, count = set.Count() });
					}
				}
				


	}
}