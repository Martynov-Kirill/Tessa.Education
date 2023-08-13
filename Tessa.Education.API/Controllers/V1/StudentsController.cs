using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Tessa.Education.API.Models.Dto;
using Tessa.Education.BLL.Models;
using Tessa.Education.BLL.Services;
using Tessa.Education.BLL.Services.Interfaces;
using Tessa.Education.Entites.Entities;
using Tessa.Education.Entites.Models.Errors.Enums;

namespace Tessa.Education.API.Controllers.V1
{
    /// <summary>
    /// Студенты
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentsController : AbstractController
    {
        private readonly IStudentService _service;
        private readonly IMapper _mapper;
        public StudentsController(IStudentService service, IMapper mapper)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper;
        }

        /// <summary>
        /// Вывод списка всех студентов
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Student>>> GetAll()
        {
            return Ok(await _service.GetAll().ConfigureAwait(false));
        }

        /// <summary>
        /// Найти студента по его ID
        /// </summary>
        /// <param name="id">Уникальный индентификатор студента</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Student>> GetBy(int id)
        {
            return Ok(await _service.Get(id).ConfigureAwait(false));
        }

        /// <summary>
        /// Добавить в базу нового студента
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Student>> Create([FromBody] StudentDto dto)
        {
            if (dto is null)
                throw new ErrorHandler(Enum.GetName(ErrorType.FIELD_IS_EMPTY));

            var model = _mapper.Map<Student>(dto);
            return Ok(await _service.Create(model).ConfigureAwait(false));
        }

        /// <summary>
        /// Добавить в базу несколько новых студентах
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        [HttpPost("many")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Student>>> CreateMany([FromBody] List<StudentDto> dtos)
        {
            if (dtos.Any() == false)
                throw new ErrorHandler(Enum.GetName(ErrorType.FIELD_IS_EMPTY));

            var models = _mapper.Map<List<Student>>(dtos);
            return Ok(await _service.CreateMany(models).ConfigureAwait(false));
        }

        /// <summary>
        /// Обновить данные о студенте
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Student>> Update(int id, [FromBody] StudentDto dto)
        {
            if (dto is null)
                throw new ErrorHandler(Enum.GetName(ErrorType.FIELD_IS_EMPTY));

            var model = _mapper.Map<Student>(dto);
            return Ok(await _service.Update(model, id));
        }

        /// <summary>
        /// Обновить данные о нескольких студентах
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        [HttpPut("many")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Student>>> UpdateMany(int id, [FromBody] List<StudentDto> dtos)
        {
            if (dtos is null)
                throw new ErrorHandler(Enum.GetName(ErrorType.FIELD_IS_EMPTY));

            var model = _mapper.Map<List<Student>>(dtos);
            return Ok(await _service.Update(model).ConfigureAwait(false));
        }

        /// <summary>
        /// Удалить данные о студенте
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Student>> Delete(List<int> ids)
        {
            return Ok(await _service.DeleteMany(ids));
        }
    }
}