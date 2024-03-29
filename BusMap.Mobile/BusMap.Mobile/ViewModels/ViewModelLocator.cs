﻿using System;
using System.Collections.Generic;
using System.Text;
using CommonServiceLocator;

namespace BusMap.Mobile.ViewModels
{
    public class ViewModelLocator
    {
        public NearestStopsMapPageViewModel NearestStopsMapPageViewModel
            => ServiceLocator.Current.GetInstance<NearestStopsMapPageViewModel>();

        public RoutesListPageViewModel RoutesListPageViewModel
            => ServiceLocator.Current.GetInstance<RoutesListPageViewModel>();

        public MainPageViewModel MainPageViewModel
            => ServiceLocator.Current.GetInstance<MainPageViewModel>();

        public TrackNewRouteViewModel TrackNewRouteViewModel
            => ServiceLocator.Current.GetInstance<TrackNewRouteViewModel>();

    }
}
