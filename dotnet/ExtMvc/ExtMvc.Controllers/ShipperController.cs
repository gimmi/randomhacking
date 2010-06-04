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
	public class ShipperController : Controller
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(ShipperController));
		private readonly ShipperRepository _repository;
		private readonly IMappingEngine _mapper;
		private readonly IValidator _validator;
		private readonly IConversation _conversation;
		private readonly IStringConverter<Shipper> _stringConverter;

		public ShipperController(IConversation conversation, IMappingEngine mapper, ShipperRepository repository, IValidator validator, IStringConverter<Shipper> stringConverter)
		{
			_conversation = conversation;
			_mapper = mapper;
			_repository = repository;
			_validator = validator;
			_stringConverter = stringConverter;
		}

		public ActionResult Create(ShipperDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				Shipper itemMapped = _mapper.Map<ShipperDto, Shipper>(item);
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
				Shipper item = _stringConverter.FromString(stringId);
				ShipperDto itemDto = _mapper.Map<Shipper, ShipperDto>(item);
				return Json(itemDto);
			}
		}

		public ActionResult Update(ShipperDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				Shipper itemMapped = _mapper.Map<ShipperDto, Shipper>(item);
				ValidationHelpers.AddErrorsToModelState(ModelState, _validator.Validate(itemMapped), "item");
				if(ModelState.IsValid)
				{
					_repository.Update(itemMapped);
					_conversation.Flush();
				}
				return Json(new{ success = true });
			}
		}

		public ActionResult Delete(ShipperDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				Shipper itemMapped = _mapper.Map<ShipperDto, Shipper>(item);
				_repository.Delete(itemMapped);
				_conversation.Flush();
				return Json(new{ success = true });
			}
		}

		public ActionResult SearchNormal(int? shipperId, string companyName, string phone, int start, int limit, string sort, string dir)
		{
			Log.DebugFormat("SearchNormal called");
			using(_conversation.SetAsCurrent())
			{
				IPresentableSet<Shipper> set = _repository.SearchNormal(shipperId, companyName, phone);
				set = set.Skip(start).Take(limit).Sort(sort, dir == "ASC");
				ShipperDto[] items = _mapper.Map<IEnumerable<Shipper>, ShipperDto[]>(set.AsEnumerable());
				return Json(new{ items, count = set.Count() });
			}
		}
	}
}