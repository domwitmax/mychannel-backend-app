using Application.Data.Entities;
using Application.Interfaces.Repository;
using Application.Models;
using Application.Models.Subscription;
using AutoMapper;

namespace Application.Services
{
    public class SubscriptionService : Interfaces.Services.ISubscriptionService
    {
        ISubscriptionRepository _subscriptionRepository;
        IMapper _mapper;
        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IMapper mappper)
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mappper;
        }
        public bool AddSubscription(SubscriptionDto subscriptionDto)
        {
            Subscription subscription = _mapper.Map<Subscription>(subscriptionDto);
            return _subscriptionRepository.AddSubscription(subscription);
        }

        public int GetSubscriptionCount(int userId)
        {
            return _subscriptionRepository.GetSubscriptionCount(userId);
        }

        public IEnumerable<SubscriptionDto> GetSubscriptions(int userId)
        {
            IEnumerable<Subscription> subscriptions = _subscriptionRepository.GetSubscriptions(userId);
            IEnumerable<SubscriptionDto> subscriptionDtos = subscriptions.Select(src => _mapper.Map<SubscriptionDto>(src));
            return subscriptionDtos;
        }

        public bool RemoveSubscription(SubscriptionDto subscriptionDto)
        {
            Subscription subscription = _mapper.Map<Subscription>(subscriptionDto);
            return _subscriptionRepository.RemoveSubscription(subscription);
        }
    }
}
