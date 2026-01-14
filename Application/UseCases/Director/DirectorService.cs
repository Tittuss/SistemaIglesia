using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Director
{
    public class DirectorService : IDirectorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DirectorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<StudentDto> CreateStudentAsync(CreateStudentDto dto)
        {
            var existingStudent = await _unitOfWork.Students.GetByEmailAsync(dto.Email);
            if (existingStudent != null)
            {
                throw new ArgumentException($"El correo {dto.Email} ya está registrado en el sistema.");
            }

            var studentEntity = _mapper.Map<Student>(dto);

            studentEntity.Id = Guid.NewGuid();
            studentEntity.CreatedAt = DateTime.UtcNow;
            studentEntity.IsActive = true;

            await _unitOfWork.Students.AddAsync(studentEntity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<StudentDto>(studentEntity);
        }

        public async Task UpdateStudentAsync(UpdateStudentDto dto)
        {
            var studentEntity = await _unitOfWork.Students.GetByIdAsync(dto.Id);
            if (studentEntity == null)
            {
                throw new KeyNotFoundException($"No se encontró al estudiante con ID {dto.Id}");
            }

            studentEntity.FirstName = dto.FirstName;
            studentEntity.LastName = dto.LastName;
            studentEntity.IsActive = dto.IsActive;

            _unitOfWork.Students.Update(studentEntity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteStudentAsync(Guid id)
        {
            var studentEntity = await _unitOfWork.Students.GetByIdAsync(id);
            if (studentEntity == null)
            {
                throw new KeyNotFoundException($"No se encontró al estudiante con ID {id}");
            }

            _unitOfWork.Students.Delete(studentEntity);

            await _unitOfWork.SaveAsync();
        }
    }
}
