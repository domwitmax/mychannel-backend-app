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

        public int GetSubscriptionCount(string userName)
        {
            return _subscriptionRepository.GetSubscriptionCount(userName);
        }

        public IEnumerable<SubscriptionDto> GetSubscriptions(string userName)
        {
            IEnumerable<Subscription> subscriptions = _subscriptionRepository.GetSubscriptions(userName);
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
