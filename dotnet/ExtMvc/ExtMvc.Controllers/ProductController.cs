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
	public class ProductController : Controller
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(ProductController));
		private readonly ProductRepository _repository;
		private readonly IMappingEngine _mapper;
		private readonly IValidator _validator;
		private readonly IConversation _conversation;
		private readonly IStringConverter<Product> _stringConverter;

		public ProductController(IConversation conversation, IMappingEngine mapper, ProductRepository repository, IValidator validator, IStringConverter<Product> stringConverter)
		{
			_conversation = conversation;
			_mapper = mapper;
			_repository = repository;
			_validator = validator;
			_stringConverter = stringConverter;
		}

		public ActionResult Save(ProductDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				Product itemMapped = _mapper.Map<ProductDto, Product>(item);
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
				Product item = _stringConverter.FromString(stringId);
				ProductDto itemDto = _mapper.Map<Product, ProductDto>(item);
				return Json(itemDto);
			}
		}

		public void Delete(string stringId)
		{
			using(_conversation.SetAsCurrent())
			{
				Product item = _stringConverter.FromString(stringId);
				_repository.Delete(item);
				_conversation.Flush();
			}
		}

		public ActionResult SearchNormal(int? productId, string productName, bool? discontinued, CategoryReferenceDto category, SupplierReferenceDto supplier, int start, int limit, string sort, string dir)
		{
			Log.DebugFormat("SearchNormal called");
			using(_conversation.SetAsCurrent())
			{
				Category categoryMapped = _mapper.Map<CategoryReferenceDto, Category>(category);
				Supplier supplierMapped = _mapper.Map<SupplierReferenceDto, Supplier>(supplier);

				IPresentableSet<Product> set = _repository.SearchNormal(productId, productName, discontinued, categoryMapped, supplierMapped);
				IEnumerable<Product> items = set.Skip(start).Take(limit).Sort(sort, dir == "ASC").AsEnumerable();
				ProductDto[] dtos = _mapper.Map<IEnumerable<Product>, ProductDto[]>(items);
				return Json(new{ items = dtos, count = set.Count() });
			}
		}
	}
}