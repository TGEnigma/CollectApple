﻿using CollectApple.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace CollectApple.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}