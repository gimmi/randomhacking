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
	public class CustomerController : Controller
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(CustomerController));
		private readonly CustomerRepository _repository;
		private readonly IMappingEngine _mapper;
		private readonly IValidator _validator;
		private readonly IConversation _conversation;
		private readonly IStringConverter<Customer> _stringConverter;

		public CustomerController(IConversation conversation, IMappingEngine mapper, CustomerRepository repository, IValidator validator, IStringConverter<Customer> stringConverter)
		{
			_conversation = conversation;
			_mapper = mapper;
			_repository = repository;
			_validator = validator;
			_stringConverter = stringConverter;
		}

		public ActionResult Create(CustomerDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				Customer itemMapped = _mapper.Map<CustomerDto, Customer>(item);
				ValidationHelpers.AddErrorsToModelState(ModelState, _validator.Validate(itemMapped), "item");
				if(ModelState.IsValid)
				{
					_repository.Create(itemMapped);
					_conversation.Flush();
				}
				return Json(new{ success = true });
			}
		}

		public ActionResult Read(string stringId)
		{
			using(_conversation.SetAsCurrent())
			{
				Customer item = _stringConverter.FromString(stringId);
				CustomerDto itemDto = _mapper.Map<Customer, CustomerDto>(item);
				return Json(itemDto);
			}
		}

		public ActionResult Update(CustomerDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				Customer itemMapped = _mapper.Map<CustomerDto, Customer>(item);
				ValidationHelpers.AddErrorsToModelState(ModelState, _validator.Validate(itemMapped), "item");
				if(ModelState.IsValid)
				{
					_repository.Update(itemMapped);
					_conversation.Flush();
				}
				return Json(new{ success = true });
			}
		}

		public ActionResult Delete(CustomerDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				Customer itemMapped = _mapper.Map<CustomerDto, Customer>(item);
				_repository.Delete(itemMapped);
				_conversation.Flush();
				return Json(new{ success = true });
			}
		}

		public ActionResult SearchNormal(string contactName, int start, int limit, string sort, string dir)
		{
			Log.DebugFormat("SearchNormal called");
			using(_conversation.SetAsCurrent())
			{
				IPresentableSet<Customer> set = _repository.SearchNormal(contactName);
				set = set.Skip(start).Take(limit).Sort(sort, dir == "ASC");
				CustomerDto[] items = _mapper.Map<IEnumerable<Customer>, CustomerDto[]>(set.AsEnumerable());
				return Json(new{ items, count = set.Count() });
			}
		}
	}
}