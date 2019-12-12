using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using MyPartyCore.DB.BL;
using MyPartyCore.DB.Models;
using MyPartyCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IPartyService _partyService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public ChatHub(IPartyService partyService,
            IMapper mapper,
            UserManager<User> userManager,
            IPhotoService photoService)
        {
            _partyService = partyService;
            _photoService = photoService;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task SendMessage(string message, int partyId)
        {
            string userName = Context.User.Identity.Name;
            User user = await _userManager.FindByNameAsync(userName);
            user.Avatar = _photoService.GetFileByID((int)user.AvatarId);

            ChatMessage chatMessage = new ChatMessage()
            {
                Date = DateTime.Now,
                Message = message,
                User = user,
                PartyId = partyId
            };

            _partyService.AddMessageChat(chatMessage);

            ChatMessageViewModel chatMessageViewModel = _mapper.Map<ChatMessageViewModel>(chatMessage);

            await Clients.Group(partyId.ToString()).SendAsync("ReceiveMessage", chatMessageViewModel);
        }
        public async Task ConnectUser(int partyId)
        {         
            await Groups.AddToGroupAsync(Context.ConnectionId, partyId.ToString());
         
            IQueryable<ChatMessage> chatMessages = _partyService.GetChatMessagesByPartyId(partyId);

            foreach (ChatMessage message in chatMessages)
            {
                ChatMessageViewModel chatMessageViewModel = _mapper.Map<ChatMessageViewModel>(message);

                await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", chatMessageViewModel);
            }         

        }

    }
}
