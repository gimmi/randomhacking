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
	public class TerritoryController : Controller
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(TerritoryController));
		private readonly TerritoryRepository _repository;
		private readonly IMappingEngine _mapper;
		private readonly IValidator _validator;
		private readonly IConversation _conversation;
		private readonly IStringConverter<Territory> _stringConverter;

		public TerritoryController(IConversation conversation, IMappingEngine mapper, TerritoryRepository repository, IValidator validator, IStringConverter<Territory> stringConverter)
		{
			_conversation = conversation;
			_mapper = mapper;
			_repository = repository;
			_validator = validator;
			_stringConverter = stringConverter;
		}

		public ActionResult Save(TerritoryDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				Territory itemMapped = _mapper.Map<TerritoryDto, Territory>(item);
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
				Territory item = _stringConverter.FromString(stringId);
				TerritoryDto itemDto = _mapper.Map<Territory, TerritoryDto>(item);
				return Json(itemDto);
			}
		}

		public void Delete(string stringId)
		{
			using(_conversation.SetAsCurrent())
			{
				Territory item = _stringConverter.FromString(stringId);
				_repository.Delete(item);
				_conversation.Flush();
			}
		}

		public ActionResult SearchNormal(string territoryDescription, int start, int limit, string sort, string dir)
		{
			Log.DebugFormat("SearchNormal called");
			using(_conversation.SetAsCurrent())
			{
				IPresentableSet<Territory> set = _repository.SearchNormal(territoryDescription);
				IEnumerable<Territory> items = set.Skip(start).Take(limit).Sort(sort, dir == "ASC").AsEnumerable();
				TerritoryDto[] dtos = _mapper.Map<IEnumerable<Territory>, TerritoryDto[]>(items);
				return Json(new{ items = dtos, count = set.Count() });
			}
		}


		public ActionResult ComboSearch(string query)
		{
			Log.DebugFormat("ComboSearch called");
			using(_conversation.SetAsCurrent())
			{
				IEnumerable<Territory> items = _repository.SearchNormal(query).AsEnumerable();
				TerritoryReferenceDto[] dtos = _mapper.Map<IEnumerable<Territory>, TerritoryReferenceDto[]>(items);
				return Json(new{ items = dtos });
			}
		}
	}
}