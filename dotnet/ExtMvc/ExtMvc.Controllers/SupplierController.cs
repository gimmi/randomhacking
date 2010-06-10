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
	public class SupplierController : Controller
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(SupplierController));
		private readonly SupplierRepository _repository;
		private readonly IMappingEngine _mapper;
		private readonly IValidator _validator;
		private readonly IConversation _conversation;
		private readonly IStringConverter<Supplier> _stringConverter;

		public SupplierController(IConversation conversation, IMappingEngine mapper, SupplierRepository repository, IValidator validator, IStringConverter<Supplier> stringConverter)
		{
			_conversation = conversation;
			_mapper = mapper;
			_repository = repository;
			_validator = validator;
			_stringConverter = stringConverter;
		}

		public ActionResult Save(SupplierDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				Supplier itemMapped = _mapper.Map<SupplierDto, Supplier>(item);
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
				Supplier item = _stringConverter.FromString(stringId);
				SupplierDto itemDto = _mapper.Map<Supplier, SupplierDto>(item);
				return Json(itemDto);
			}
		}

		public ActionResult Delete(SupplierDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				Supplier itemMapped = _mapper.Map<SupplierDto, Supplier>(item);
				_repository.Delete(itemMapped);
				_conversation.Flush();
				return Json(new{ success = true });
			}
		}

		public ActionResult SearchNormal(int? supplierId, string companyName, string contactName, string contactTitle, string address, string city, string region, string postalCode, string country, string phone, string fax, string homePage, int start, int limit, string sort, string dir)
		{
			Log.DebugFormat("SearchNormal called");
			using(_conversation.SetAsCurrent())
			{
				IPresentableSet<Supplier> set = _repository.SearchNormal(supplierId, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax, homePage);
				IEnumerable<Supplier> items = set.Skip(start).Take(limit).Sort(sort, dir == "ASC").AsEnumerable();
				SupplierDto[] dtos = _mapper.Map<IEnumerable<Supplier>, SupplierDto[]>(items);
				return Json(new{ items = dtos, count = set.Count() });
			}
		}
	}
}