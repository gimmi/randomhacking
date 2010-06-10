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

		public ActionResult Save(CustomerDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				Customer itemMapped = _mapper.Map<CustomerDto, Customer>(item);
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
				Customer item = _stringConverter.FromString(stringId);
				CustomerDto itemDto = _mapper.Map<Customer, CustomerDto>(item);
				return Json(itemDto);
			}
		}

		public void Delete(string stringId)
		{
			using(_conversation.SetAsCurrent())
			{
				Customer item = _stringConverter.FromString(stringId);
				_repository.Delete(item);
				_conversation.Flush();
			}
		}

		public ActionResult SearchNormal(string contactName, int start, int limit, string sort, string dir)
		{
			Log.DebugFormat("SearchNormal called");
			using(_conversation.SetAsCurrent())
			{
				IPresentableSet<Customer> set = _repository.SearchNormal(contactName);
				IEnumerable<Customer> items = set.Skip(start).Take(limit).Sort(sort, dir == "ASC").AsEnumerable();
				CustomerDto[] dtos = _mapper.Map<IEnumerable<Customer>, CustomerDto[]>(items);
				return Json(new{ items = dtos, count = set.Count() });
			}
		}
	}
}