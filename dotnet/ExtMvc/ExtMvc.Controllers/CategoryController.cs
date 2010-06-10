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
	public class CategoryController : Controller
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(CategoryController));
		private readonly CategoryRepository _repository;
		private readonly IMappingEngine _mapper;
		private readonly IValidator _validator;
		private readonly IConversation _conversation;
		private readonly IStringConverter<Category> _stringConverter;

		public CategoryController(IConversation conversation, IMappingEngine mapper, CategoryRepository repository, IValidator validator, IStringConverter<Category> stringConverter)
		{
			_conversation = conversation;
			_mapper = mapper;
			_repository = repository;
			_validator = validator;
			_stringConverter = stringConverter;
		}

		public ActionResult Save(CategoryDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				Category itemMapped = _mapper.Map<CategoryDto, Category>(item);
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
				Category item = _stringConverter.FromString(stringId);
				CategoryDto itemDto = _mapper.Map<Category, CategoryDto>(item);
				return Json(itemDto);
			}
		}

		public void Delete(string stringId)
		{
			using(_conversation.SetAsCurrent())
			{
				Category item = _stringConverter.FromString(stringId);
				_repository.Delete(item);
				_conversation.Flush();
			}
		}

		public ActionResult SearchNormal(int start, int limit, string sort, string dir)
		{
			Log.DebugFormat("SearchNormal called");
			using(_conversation.SetAsCurrent())
			{
				IPresentableSet<Category> set = _repository.SearchNormal();
				IEnumerable<Category> items = set.Skip(start).Take(limit).Sort(sort, dir == "ASC").AsEnumerable();
				CategoryDto[] dtos = _mapper.Map<IEnumerable<Category>, CategoryDto[]>(items);
				return Json(new{ items = dtos, count = set.Count() });
			}
		}
	}
}