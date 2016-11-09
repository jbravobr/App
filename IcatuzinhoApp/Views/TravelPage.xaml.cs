using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
using Acr.UserDialogs;
using Microsoft.Practices.Unity;
using Xamarin.Forms.Xaml;

namespace IcatuzinhoApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TravelPage : ContentPage
    {
        List<Station> _stations;
        ILogExceptionService _logExceptionService;

        protected override void OnAppearing()
        {
            var _userDialogsService = App._container.Resolve<IUserDialogs>();

            try
            {
                if (App.MapLoaded == false)
                {
                    App.MapLoaded = true;
                    CustomMap iosMap;

                    if (Device.OS == TargetPlatform.iOS)
                        iosMap = new CustomMap();

                    var model = BindingContext as TravelPageViewModel;
                    var routes = model.Get();

                    _stations = model.GetStations();

                    if (_logExceptionService == null)
                        _logExceptionService = App._container.Resolve<ILogExceptionService>();

                    if (_stations != null)
                    {
                        _userDialogsService.ShowLoading();

                        if (_stations != null && _stations.Any())
                        {
                            foreach (var s in _stations)
                            {
                                var p = new Pin
                                {
                                    Label = s.Name,
                                    Position = new Position(s.Latitude, s.Longitude),
                                    Type = PinType.SearchResult
                                };

                                MapaTravel.Pins.Add(p);
                            }

                            if (Device.OS == TargetPlatform.iOS)
                            {
                                if (MapaTravel.RouteCoordinates != null && MapaTravel.RouteCoordinates.Any())
                                    MapaTravel.RouteCoordinates = new List<Position>();
                            }

                            if (routes != null && routes.Any())
                            {
                                foreach (var route in routes.OrderBy(c => c.Order))
                                {
                                    MapaTravel.RouteCoordinates.Add(new Position(route.Latitude, route.Longitude));
                                }
                            }
                        }

                        MapaTravel.MoveToRegion(MapSpan.FromCenterAndRadius(
                            new Position(_stations.First().Latitude,
                                         _stations.First().Longitude),
                                    Distance.FromMeters(1000)));

                        _userDialogsService.HideLoading();

                        if (Device.OS == TargetPlatform.iOS)
                        {
                            iosMap = MapaTravel;
                            Content = new StackLayout
                            {
                                VerticalOptions = LayoutOptions.FillAndExpand,
                                Padding = 0,
                                Children = {
                                                iosMap
                                    }
                            };
                        }
                    }
                }

                base.OnAppearing();
            }
            catch (Exception ex)
            {
                _logExceptionService.SubmitToInsights(ex);
                _userDialogsService.HideLoading();
            }
        }

        public TravelPage()
        {
            InitializeComponent();
        }
    }
}

