using Hospital.Core.IRepositories;
using Hospital.Core.IServices;
using Hospital.Repository.Repositories;
using Hospital.Service.Services;
using HospitalManagementSystem.Errors;
using HospitalManagementSystem.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Extenstion
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddScoped(typeof(IPatientService), typeof(PatientService));
            services.AddScoped(typeof(IDoctorService), typeof(DoctorService));
            services.AddScoped(typeof(IDepartmentService), typeof(DepartmentService));
            services.AddAutoMapper(m => m.AddProfile(typeof(MappingProfiles)));
            services.AddScoped(typeof(IMedicalRecordService), typeof(MedicalRecordService));
            services.AddScoped(typeof(IAppointmentService), typeof(AppointmentService));
            services.AddScoped(typeof(IMedicationService), typeof(MedicationService));
            services.AddScoped(typeof(IPrescriptionDetailService), typeof(PrescriptionDetailService));


            services.Configure<ApiBehaviorOptions>(options =>
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count > 0)
                                              .SelectMany(p => p.Value.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                    var validatonErrorResponsse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(validatonErrorResponsse);
                }
            );
            return services;
        }
    }
}
