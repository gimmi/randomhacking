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
	public class EmployeeController : Controller
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(EmployeeController));
		private readonly EmployeeRepository _repository;
		private readonly IMappingEngine _mapper;
		private readonly IValidator _validator;
		private readonly IConversation _conversation;
		private readonly IStringConverter<Employee> _stringConverter;

		public EmployeeController(IConversation conversation, IMappingEngine mapper, EmployeeRepository repository, IValidator validator, IStringConverter<Employee> stringConverter)
		{
			_conversation = conversation;
			_mapper = mapper;
			_repository = repository;
			_validator = validator;
			_stringConverter = stringConverter;
		}

		public ActionResult Create(EmployeeDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				Employee itemMapped = _mapper.Map<EmployeeDto, Employee>(item);
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
				Employee item = _stringConverter.FromString(stringId);
				EmployeeDto itemDto = _mapper.Map<Employee, EmployeeDto>(item);
				return Json(itemDto);
			}
		}

		public ActionResult Update(EmployeeDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				Employee itemMapped = _mapper.Map<EmployeeDto, Employee>(item);
				ValidationHelpers.AddErrorsToModelState(ModelState, _validator.Validate(itemMapped), "item");
				if(ModelState.IsValid)
				{
					_repository.Update(itemMapped);
					_conversation.Flush();
				}
				return Json(new{ success = true });
			}
		}

		public ActionResult Delete(EmployeeDto item)
		{
			using(_conversation.SetAsCurrent())
			{
				Employee itemMapped = _mapper.Map<EmployeeDto, Employee>(item);
				_repository.Delete(itemMapped);
				_conversation.Flush();
				return Json(new{ success = true });
			}
		}

		public ActionResult SearchNormal(int start, int limit, string sort, string dir)
		{
			Log.DebugFormat("SearchNormal called");
			using(_conversation.SetAsCurrent())
			{
				IPresentableSet<Employee> set = _repository.SearchNormal();
				set = set.Skip(start).Take(limit).Sort(sort, dir == "ASC");
				EmployeeDto[] items = _mapper.Map<IEnumerable<Employee>, EmployeeDto[]>(set.AsEnumerable());
				return Json(new{ items, count = set.Count() });
			}
		}
	}
}