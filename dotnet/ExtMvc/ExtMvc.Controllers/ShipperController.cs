using System.Web.Mvc;
using System.Collections.Generic;

namespace ExtMvc.Controllers
{
	public class ShipperController : Controller
	{
		private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ExtMvc.Controllers.ShipperController));
		private readonly ExtMvc.Data.ShipperRepository _repository;
		private readonly AutoMapper.IMappingEngine _mapper;
		private readonly Nexida.Infrastructure.IValidator _validator;
		private readonly Conversation.IConversation _conversation;
		private readonly Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Shipper> _stringConverter;

		public ShipperController(Conversation.IConversation conversation, AutoMapper.IMappingEngine mapper, ExtMvc.Data.ShipperRepository repository, Nexida.Infrastructure.IValidator validator, Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Shipper> stringConverter)
		{
			_conversation = conversation;
			_mapper = mapper;
			_repository = repository;
			_validator = validator;
			_stringConverter = stringConverter;
		}

		public ActionResult Save(ExtMvc.Dtos.ShipperDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				var itemMapped = _mapper.Map<ExtMvc.Dtos.ShipperDto, ExtMvc.Domain.Shipper>(item);
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
				var itemDto = _mapper.Map<ExtMvc.Domain.Shipper, ExtMvc.Dtos.ShipperDto>(item);
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

				public ActionResult SearchNormal(int? shipperId, string companyName, string phone, int start, int limit, string sort, string dir)
				{
					Log.DebugFormat("SearchNormal called");
					using(_conversation.SetAsCurrent())
					{
																		
						var set = _repository.SearchNormal(shipperId, companyName, phone);
						var items = set.Skip(start).Take(limit).Sort(sort, dir == "ASC").AsEnumerable();
						var dtos = _mapper.Map<IEnumerable<ExtMvc.Domain.Shipper>, ExtMvc.Dtos.ShipperDto[]>(items);
						return Json(new{ items = dtos, count = set.Count() });
					}
				}
				


	}
}