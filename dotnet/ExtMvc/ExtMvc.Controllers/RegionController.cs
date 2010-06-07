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
	public class RegionController : Controller
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(RegionController));
		private readonly RegionRepository _repository;
		private readonly IMappingEngine _mapper;
		private readonly IValidator _validator;
		private readonly IConversation _conversation;
		private readonly IStringConverter<Region> _stringConverter;

		public RegionController(IConversation conversation, IMappingEngine mapper, RegionRepository repository, IValidator validator, IStringConverter<Region> stringConverter)
		{
			_conversation = conversation;
			_mapper = mapper;
			_repository = repository;
			_validator = validator;
			_stringConverter = stringConverter;
		}

		public ActionResult Create(RegionDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				Region itemMapped = _mapper.Map<RegionDto, Region>(item);
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
				Region item = _stringConverter.FromString(stringId);
				RegionDto itemDto = _mapper.Map<Region, RegionDto>(item);
				return Json(itemDto);
			}
		}

		public ActionResult Update(RegionDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				Region itemMapped = _mapper.Map<RegionDto, Region>(item);
				ValidationHelpers.AddErrorsToModelState(ModelState, _validator.Validate(itemMapped), "item");
				if(ModelState.IsValid)
				{
					_repository.Update(itemMapped);
					_conversation.Flush();
				}
				return Json(new{ success = true });
			}
		}

		public ActionResult Delete(RegionDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				Region itemMapped = _mapper.Map<RegionDto, Region>(item);
				_repository.Delete(itemMapped);
				_conversation.Flush();
				return Json(new{ success = true });
			}
		}

		public ActionResult SearchNormal(int? regionId, string regionDescription, int start, int limit, string sort, string dir)
		{
			Log.DebugFormat("SearchNormal called");
			using(_conversation.SetAsCurrent())
			{
				IPresentableSet<Region> set = _repository.SearchNormal(regionId, regionDescription);
				IEnumerable<Region> items = set.Skip(start).Take(limit).Sort(sort, dir == "ASC").AsEnumerable();
				RegionDto[] dtos = _mapper.Map<IEnumerable<Region>, RegionDto[]>(items);
				return Json(new{ items = dtos, count = set.Count() });
			}
		}
	}
}