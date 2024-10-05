using AutoMapper;
using Loymark.Back.Application.DTOs;
using Loymark.Back.Domain.Entities;

namespace Loymark.Back.Application.Profiles
{
    public class ActivityProfiles : Profile
    {
        public ActivityProfiles()
        {
            CreateMap<Activity, ActivityDto>()
                .ForMember(
                    activityDto => activityDto.UserName,
                    activity => activity.MapFrom(activity => $"{activity.User.Name} {activity.User.LastName}")
                ).ReverseMap();
        }
    }
}
