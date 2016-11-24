﻿using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using See4Me.Services;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.IO;
using See4Me.Extensions;
using Windows.UI.Popups;
using System;

namespace See4Me.Views
{
    public sealed partial class RecognizeTextPage : Page
    {
        public RecognizeTextPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.RegisterMessages();
            base.OnNavigatedTo(e);
        }

        private void RegisterMessages()
        {
            Messenger.Default.Register<NotificationMessage<byte[]>>(this, async (message) =>
            {
                switch (message.Notification)
                {
                    case Constants.PhotoTaken:
                        photo.Source = null;
                        await photo.SetSourceAsync(message.Content);

                        break;
                }
            });
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            Messenger.Default.Unregister(this);
            base.OnNavigatingFrom(e);
        }
    }
}
