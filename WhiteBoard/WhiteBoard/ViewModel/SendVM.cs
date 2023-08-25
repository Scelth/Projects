using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WhiteBoard.Context;
using WhiteBoard.Model;
using WhiteBoard.Services.Interfaces;
using WhiteBoard.View;

namespace WhiteBoard.ViewModel
{
    public class SendVM : ViewModelBase
    {
        public byte[] ImageBytes { get; set; }
        private readonly ISendService _sendService;
        private string _subject;
        public string Subject
        {
            get => _subject;
            set => Set(ref _subject, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => Set(ref _message, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        public SendVM(ISendService sendService)
        {
            _sendService = sendService;
        }

        public RelayCommand SendCommand
        {
            get => new(() =>
            {
                _sendService.SendToEmail(Email, Subject, Name, Message);
            });
        }
    }
}