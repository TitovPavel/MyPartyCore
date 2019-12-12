using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.ViewModels
{
    public class ChatMessageViewModel
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public string AvatarPath { get; set; }
    }
}
