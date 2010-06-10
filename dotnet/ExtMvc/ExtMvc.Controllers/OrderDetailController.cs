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
	public class OrderDetailController : Controller
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(OrderDetailController));
		private readonly OrderDetailRepository _repository;
		private readonly IMappingEngine _mapper;
		private readonly IValidator _validator;
		private readonly IConversation _conversation;
		private readonly IStringConverter<OrderDetail> _stringConverter;

		public OrderDetailController(IConversation conversation, IMappingEngine mapper, OrderDetailRepository repository, IValidator validator, IStringConverter<OrderDetail> stringConverter)
		{
			_conversation = conversation;
			_mapper = mapper;
			_repository = repository;
			_validator = validator;
			_stringConverter = stringConverter;
		}

		public ActionResult Save(OrderDetailDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				OrderDetail itemMapped = _mapper.Map<OrderDetailDto, OrderDetail>(item);
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
				OrderDetail item = _stringConverter.FromString(stringId);
				OrderDetailDto itemDto = _mapper.Map<OrderDetail, OrderDetailDto>(item);
				return Json(itemDto);
			}
		}

		public ActionResult Delete(OrderDetailDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				OrderDetail itemMapped = _mapper.Map<OrderDetailDto, OrderDetail>(item);
				_repository.Delete(itemMapped);
				_conversation.Flush();
				return Json(new{ success = true });
			}
		}

		public ActionResult Search(int? orderId, int? productId, decimal? unitPrice, short? quantity, float? discount, int start, int limit, string sort, string dir)
		{
			Log.DebugFormat("Search called");
			using(_conversation.SetAsCurrent())
			{
				IPresentableSet<OrderDetail> set = _repository.Search(orderId, productId, unitPrice, quantity, discount);
				IEnumerable<OrderDetail> items = set.Skip(start).Take(limit).Sort(sort, dir == "ASC").AsEnumerable();
				OrderDetailDto[] dtos = _mapper.Map<IEnumerable<OrderDetail>, OrderDetailDto[]>(items);
				return Json(new{ items = dtos, count = set.Count() });
			}
		}
	}
}