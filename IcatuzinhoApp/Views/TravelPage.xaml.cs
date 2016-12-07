using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
using Acr.UserDialogs;
using Microsoft.Practices.Unity;

namespace IcatuzinhoApp
{
    public partial class TravelPage : ContentPage
    {
        double _latitudeInicial = -22.9101457;
        double _longitudeInicial = -43.1707052;

        List<Station> _stations;

        protected async override void OnAppearing()
        {
            try
            {
                if (App.MapLoaded == false)
                {
                    App.MapLoaded = true;
                    CustomMap iosMap;

                    if (Device.OS == TargetPlatform.iOS)
                        iosMap = new CustomMap();

                    var model = BindingContext as TravelPageViewModel;
                    var routes = await model.Get();

                    _stations = await model.GetStations();

                    if (_stations != null)
                    {
                        UserDialogs.Instance.ShowLoading();

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

                        UserDialogs.Instance.HideLoading();

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
                LogExceptionHelper.SubmitToInsights(ex);
                UserDialogs.Instance.HideLoading();
            }
        }

        public TravelPage()
        {
            InitializeComponent();

            try
            {
                MapaTravel.MoveToRegion(MapSpan.FromCenterAndRadius(
                    new Position(_latitudeInicial, _longitudeInicial), new Distance(2.00)));
            }
            catch (Exception ex)
            {
                LogExceptionHelper.SubmitToInsights(ex);
            }
        }
    }
}

