using System.Web.Mvc;
using System.Collections.Generic;

namespace ExtMvc.Controllers.Ns
{
	public class NsCategoryController : Controller
	{
		private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ExtMvc.Controllers.Ns.NsCategoryController));
		private readonly ExtMvc.Data.Ns.CategoryRepository _repository;
		private readonly AutoMapper.IMappingEngine _mapper;
		private readonly Nexida.Infrastructure.IValidator _validator;
		private readonly Conversation.IConversation _conversation;
		private readonly Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Ns.Category> _stringConverter;

		public NsCategoryController(Conversation.IConversation conversation, AutoMapper.IMappingEngine mapper, ExtMvc.Data.Ns.CategoryRepository repository, Nexida.Infrastructure.IValidator validator, Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Ns.Category> stringConverter)
		{
			_conversation = conversation;
			_mapper = mapper;
			_repository = repository;
			_validator = validator;
			_stringConverter = stringConverter;
		}

		public ActionResult Save(ExtMvc.Dtos.Ns.CategoryDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				var itemMapped = _mapper.Map<ExtMvc.Dtos.Ns.CategoryDto, ExtMvc.Domain.Ns.Category>(item);
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
				var itemDto = _mapper.Map<ExtMvc.Domain.Ns.Category, ExtMvc.Dtos.Ns.CategoryDto>(item);
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

				public ActionResult SearchNormal(int start, int limit, string sort, string dir)
				{
					Log.DebugFormat("SearchNormal called");
					using(_conversation.SetAsCurrent())
					{
						var set = _repository.SearchNormal();
						var items = set.Skip(start).Take(limit).Sort(sort, dir == "ASC").AsEnumerable();
						var dtos = _mapper.Map<IEnumerable<ExtMvc.Domain.Ns.Category>, ExtMvc.Dtos.Ns.CategoryDto[]>(items);
						return Json(new{ items = dtos, count = set.Count() });
					}
				}
				



				public ActionResult ComboSearch()
				{
					Log.DebugFormat("ComboSearch called");
					using(_conversation.SetAsCurrent())
					{
						var items = _repository.SearchNormal().AsEnumerable();
						var dtos = _mapper.Map<IEnumerable<ExtMvc.Domain.Ns.Category>, ExtMvc.Dtos.Ns.CategoryReferenceDto[]>(items);
						return Json(new{ items = dtos });
					}
				}
				
	}
}