using Application.Data.Entities;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Application.Models.Subscription;
using AutoMapper;

namespace Application.Services
{
    public class SubscriptionService: ISubscriptionService
    {
        private readonly IAccountService _accountService;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMapper _mapper;
        public SubscriptionService(IAccountService accountService, ISubscriptionRepository subscriptionRepository, IMapper mappper)
        {
            _accountService = accountService;
            _subscriptionRepository = subscriptionRepository;
            _mapper = mappper;
        }
        private bool subscriptionUserExist(SubscriptionDto subscriptionDto)
        {
            return _accountService.Exist(subscriptionDto.SubscriptionUserName) && _accountService.Exist(subscriptionDto.UserName);
        }
        public bool AddSubscription(SubscriptionDto subscriptionDto)
        {
            if (!subscriptionUserExist(subscriptionDto))
                return false;
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
            if (!subscriptionUserExist(subscriptionDto))
                return false;
            Subscription subscription = _mapper.Map<Subscription>(subscriptionDto);
            return _subscriptionRepository.RemoveSubscription(subscription);
        }
    }
}
