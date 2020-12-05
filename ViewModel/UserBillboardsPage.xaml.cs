﻿using Billbort.Presents;
using DAL.Repositories.Implementations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Billbort
{
    public partial class UserBillboardsPage : Page
    {
        private UserViewBillboardService _userViewBillboardService;
        public UserBillboardsPage()
        {
            InitializeComponent();
            var createNewBillboardRepository = new CreateNewBillboardRepository();
            var billboards = createNewBillboardRepository.GetAll();
            var newBillboards = billboards.Where(c => c.Owner != string.Empty);
            billsGrid.ItemsSource = newBillboards;
            _userViewBillboardService = new UserViewBillboardService();
        }

        private void Button_Click_View_Billboard(object sender, RoutedEventArgs e)
        {
            _userViewBillboardService.ViewBillboard(sender, out int id, out string address, out string owner);
            UserViewBillboardPage.BillboardAddress = address;
            UserViewBillboardPage.BillboardId = id;
            UserViewBillboardPage.BillboardOwner = owner;
            this.NavigationService.Navigate(new UserViewBillboardPage());
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new UserRoomPage());
        }

        private void Button_Click_View_Registration(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegisterBillboardPage());
        }

    }
}
