﻿using BillboardsProject.Model.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BillboardsProject.View
{
    /// <summary>
    /// Interaction logic for CreateSchedulePage.xaml
    /// </summary>
    public partial class CreateSchedulePage : Page
    {
        DatabaseContext database;
        public CreateSchedulePage()
        {
            InitializeComponent();
            database = new DatabaseContext();
            DynamicCheckBox();
           
        }

        private void DynamicCheckBox()
        {
            var itemList = database.Videos.ToList();
            var itemCorrectList = itemList.Where(c => c.OwnerId == AuthorizationPage.UserId);
            foreach(var item in itemCorrectList)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Name = "video_checkbox";
                checkBox.Content = item.NameOfVideo.ToString();
                scheduleCheckbox.Children.Add(checkBox);
            }
        }

        public void Button_Click_Save_and_Continue(object sender, RoutedEventArgs e)
        {
           var videos =  CheckVideos();
            this.NavigationService.Navigate(new AmountOfRepeatVideos(videos));
        }

        public void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SchedulePage());
        }

        private List<Video> CheckVideos()
        {
            List<Video> videos = database.Videos.ToList();
            List<Video> checkVideos = new List<Video>();

            List<CheckBox> checkBoxes = new List<CheckBox>();

            foreach (Object item in scheduleCheckbox.Children)
            {
                if (item is CheckBox)
                {
                    checkBoxes.Add((CheckBox)item);
                }
            }

            foreach (CheckBox item in checkBoxes)
            {
                if ((bool) item.IsChecked)
                {
                    var value = videos.Find(c => c.NameOfVideo == item.Content.ToString());
                    checkVideos.Add(value);
                }
            }
            return checkVideos;
        }
        

    }
}