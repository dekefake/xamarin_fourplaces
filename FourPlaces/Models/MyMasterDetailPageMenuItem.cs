﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FourPlaces.Views;

namespace FourPlaces.Models
{
    public class MyMasterDetailPageMenuItem
    {
        public MyMasterDetailPageMenuItem()
        {
            TargetType = typeof(MainPage);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}