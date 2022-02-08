using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SignalR.DAL;
using SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiggnalR.Hubs
{
    public class ChatHub:Hub
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly Context _context;

        public ChatHub(UserManager<AppUser> userManager, SignalR.DAL.Context context)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task SendMessage(string userValue,string message)
        {
            await Clients.All.SendAsync("RecieveMessage", userValue, message);
        }

        public override Task OnConnectedAsync()
        {
            AppUser user = _userManager.FindByNameAsync(Context.User.Identity.Name).Result;
            user.ConnectionId = Context.ConnectionId;
            //await _userManager.UpdateAsync(user);
            Clients.All.SendAsync("Connected", user.Id);
            return base.OnConnectedAsync();



        }
    }
}
