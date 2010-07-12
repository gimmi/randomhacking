using System.Web.Mvc;
using System.Collections.Generic;

namespace ExtMvc.Controllers.Ns
{
	public class NsCustomerController : Controller
	{
		private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ExtMvc.Controllers.Ns.NsCustomerController));
		private readonly ExtMvc.Data.Ns.CustomerRepository _repository;
		private readonly AutoMapper.IMappingEngine _mapper;
		private readonly Nexida.Infrastructure.IValidator _validator;
		private readonly Conversation.IConversation _conversation;
		private readonly Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Ns.Customer> _stringConverter;

		public NsCustomerController(Conversation.IConversation conversation, AutoMapper.IMappingEngine mapper, ExtMvc.Data.Ns.CustomerRepository repository, Nexida.Infrastructure.IValidator validator, Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Ns.Customer> stringConverter)
		{
			_conversation = conversation;
			_mapper = mapper;
			_repository = repository;
			_validator = validator;
			_stringConverter = stringConverter;
		}

		public ActionResult Save(ExtMvc.Dtos.Ns.CustomerDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				var itemMapped = _mapper.Map<ExtMvc.Dtos.Ns.CustomerDto, ExtMvc.Domain.Ns.Customer>(item);
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
				var itemDto = _mapper.Map<ExtMvc.Domain.Ns.Customer, ExtMvc.Dtos.Ns.CustomerDto>(item);
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

				public ActionResult SearchNormal(string contactName, int start, int limit, string sort, string dir)
				{
					Log.DebugFormat("SearchNormal called");
					using(_conversation.SetAsCurrent())
					{
										
						var set = _repository.SearchNormal(contactName);
						var items = set.Skip(start).Take(limit).Sort(sort, dir == "ASC").AsEnumerable();
						var dtos = _mapper.Map<IEnumerable<ExtMvc.Domain.Ns.Customer>, ExtMvc.Dtos.Ns.CustomerDto[]>(items);
						return Json(new{ items = dtos, count = set.Count() });
					}
				}
				


				public ActionResult ComboSearch(string query)
				{
					Log.DebugFormat("ComboSearch called");
					using(_conversation.SetAsCurrent())
					{
						var items = _repository.SearchNormal(query).AsEnumerable();
						var dtos = _mapper.Map<IEnumerable<ExtMvc.Domain.Ns.Customer>, ExtMvc.Dtos.Ns.CustomerReferenceDto[]>(items);
						return Json(new{ items = dtos });
					}
				}
				

	}
}