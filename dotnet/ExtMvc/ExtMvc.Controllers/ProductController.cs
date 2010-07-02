using System.Web.Mvc;
using System.Collections.Generic;

namespace ExtMvc.Controllers
{
	public class ProductController : Controller
	{
		private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ExtMvc.Controllers.ProductController));
		private readonly ExtMvc.Data.ProductRepository _repository;
		private readonly AutoMapper.IMappingEngine _mapper;
		private readonly Nexida.Infrastructure.IValidator _validator;
		private readonly Conversation.IConversation _conversation;
		private readonly Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Product> _stringConverter;

		public ProductController(Conversation.IConversation conversation, AutoMapper.IMappingEngine mapper, ExtMvc.Data.ProductRepository repository, Nexida.Infrastructure.IValidator validator, Nexida.Infrastructure.IStringConverter<ExtMvc.Domain.Product> stringConverter)
		{
			_conversation = conversation;
			_mapper = mapper;
			_repository = repository;
			_validator = validator;
			_stringConverter = stringConverter;
		}

		public ActionResult Save(ExtMvc.Dtos.ProductDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				var itemMapped = _mapper.Map<ExtMvc.Dtos.ProductDto, ExtMvc.Domain.Product>(item);
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
				var itemDto = _mapper.Map<ExtMvc.Domain.Product, ExtMvc.Dtos.ProductDto>(item);
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

				public ActionResult SearchNormal(int? productId, string productName, bool? discontinued, ExtMvc.Dtos.CategoryReferenceDto category, ExtMvc.Dtos.SupplierReferenceDto supplier, int start, int limit, string sort, string dir)
				{
					Log.DebugFormat("SearchNormal called");
					using(_conversation.SetAsCurrent())
					{
																						var categoryMapped = _mapper.Map<ExtMvc.Dtos.CategoryReferenceDto, ExtMvc.Domain.Category>(category);
														var supplierMapped = _mapper.Map<ExtMvc.Dtos.SupplierReferenceDto, ExtMvc.Domain.Supplier>(supplier);
										
						var set = _repository.SearchNormal(productId, productName, discontinued, categoryMapped, supplierMapped);
						var items = set.Skip(start).Take(limit).Sort(sort, dir == "ASC").AsEnumerable();
						var dtos = _mapper.Map<IEnumerable<ExtMvc.Domain.Product>, ExtMvc.Dtos.ProductDto[]>(items);
						return Json(new{ items = dtos, count = set.Count() });
					}
				}
				
	}
}