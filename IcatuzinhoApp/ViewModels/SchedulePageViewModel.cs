using System;
using PropertyChanged;
using Acr.UserDialogs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;

namespace IcatuzinhoApp
{
	[ImplementPropertyChanged]
	public class SchedulePageViewModel : BasePageViewModel
	{
		readonly ITravelService _travelService;
		readonly IWeatherService _weatherService;

		IUserDialogs _userDialogs { get; set; }

		public List<Travel> Travels { get; set; }

		public bool IsRefreshing { get; set; }

		public bool IsOval2Setup { get; set; }

		public string Temp { get; set; }

		public string TempIco { get; set; }

		public SchedulePageViewModel(IScheduleService scheduleService,
									 ITravelService travelService,
									 IWeatherService weatherService)
		{
			_travelService = travelService;
			_weatherService = weatherService;
			Init();

			IsRefreshing = false;
			IsOval2Setup = false;
		}

		public void Init()
		{
			try
			{
				Travels = GetAll();
			}
			catch (Exception ex)
			{
				_userDialogs.HideLoading();
				base.SendToInsights(ex);
				UIFunctions.ShowErrorMessageToUI();
			}
		}

		public List<Travel> GetAll()
		{
			var w = GetWeather();
			TempIco = SetFontAwesomeForTemp(w.Ico);
			Temp = w.Temp;

			var collection = _travelService.GetAll();

			foreach (var item in collection.OrderBy(c => c.Schedule.StartSchedule.ToLocalTime()))
			{
				if (TimeSpan.Compare(DateTime.Now.ToLocalTime().TimeOfDay, item.Schedule.StartSchedule.ToLocalTime().TimeOfDay) <= 0)
					SetScheduleAvatar(true, item);
				else
					SetScheduleAvatar(false, item);

				if (TimeSpan.Compare(DateTime.Now.ToLocalTime().TimeOfDay, item.Schedule.StartSchedule.ToLocalTime().TimeOfDay) <= 0 &&
					item.Vehicle.SeatsAvailable > 0)
					SetScheduleStatusDescription(true, item);
				else
					SetScheduleStatusDescription(false, item);

				item.GetTemp = $"{TempIco} {Temp}º";
			}

			return collection;
		}

		public async Task GetUpdatedSeatsAvailableBySchedule()
		{
			if (Travels == null && !Travels.Any())
				return;

			foreach (var item in Travels)
			{
				try
				{
					item.Vehicle.SeatsAvailable = (int)await _travelService.GetSeatsAvailableByTravel(item.Schedule.Id);
				}
				catch (Exception ex)
				{
					SendToInsights(ex);
				}
			}
		}

		public void ScheduleGetInfoForUI()
		{
			Device.StartTimer(TimeSpan.FromSeconds(10), () =>
				{
					Task.Factory.StartNew(() =>
						{
							Device.BeginInvokeOnMainThread(async () =>
								{
									await GetUpdatedSeatsAvailableBySchedule();
									ScheduleGetInfoForUI();
								});
						});

					return false;
				});
		}

		public Command Refresh
		{
			get
			{
				return new Command(async () =>
					{
						try
						{
							IsRefreshing = true;
							var ids = Travels.Select(x => x.Id).ToList();

							foreach (var id in ids)
							{
								var availableSeats = await _travelService.GetAvailableSeats(id);
								try
								{
									Travels.First(x => x.Id == id).Vehicle.SeatsAvailable = availableSeats;
								}
								catch (Exception ex)
								{
									SendToInsights(ex);
								}

								if (TimeSpan.Compare(DateTime.Now.ToLocalTime().TimeOfDay, Travels.First(x => x.Id == id).Schedule.StartSchedule.ToLocalTime().TimeOfDay) <= 0)
									SetScheduleAvatar(true, Travels.First(x => x.Id == id));
								else
									SetScheduleAvatar(false, Travels.First(x => x.Id == id));

								if (TimeSpan.Compare(DateTime.Now.ToLocalTime().TimeOfDay, Travels.First(x => x.Id == id).Schedule.StartSchedule.ToLocalTime().TimeOfDay) <= 0 &&
									Travels.First(x => x.Id == id).Vehicle.SeatsAvailable > 0)
									SetScheduleStatusDescription(true, Travels.First(x => x.Id == id));
								else
									SetScheduleStatusDescription(false, Travels.First(x => x.Id == id));
							}
						}
						catch (Exception ex)
						{
							SendToInsights(ex);
							UIFunctions.ShowErrorMessageToUI("Erro ao atualizar as viagens, por favor tente novamente");
						}
						finally
						{
							IsRefreshing = false;
						}
					});
			}
		}

		void SetScheduleAvatar(bool available, Travel item)
		{
			if (available)
			{
				try
				{
					if (DateTime.Now.ToLocalTime().TimeOfDay <= item.Schedule.StartSchedule.ToLocalTime().TimeOfDay
						&& !IsOval2Setup)
					{
						item.Schedule.StatusAvatar = "oval2.png";
						item.Schedule.StatusDescription = "Embarque Liberado";
						IsOval2Setup = true;
					}
					else
					{
						item.Schedule.StatusAvatar = "oval3.png";
						item.Schedule.StatusDescription = "Embarque Fechado";
					}

					return;
				}
				catch (Exception ex)
				{
					base.SendToInsights(ex);
				}
			}

			try
			{
				item.Schedule.StatusDescription = "Embarque Encerrado";
				item.Schedule.StatusAvatar = "oval1.png";

				return;
			}
			catch (Exception ex)
			{
				base.SendToInsights(ex);
			}
		}

		void SetScheduleStatusDescription(bool available, Travel item)
		{
			/*
            var realm = Realm.GetInstance();

            if (available)
            {
                using (var tran = realm.BeginWrite())
                {
                    try
                    {
                        if (IsOval2Setup && DateTime.Now.ToLocalTime().TimeOfDay <= item.Schedule.StartSchedule.ToLocalTime().TimeOfDay)
                            item.Schedule.StatusDescription = "Embarque Liberado";
                        else
                            item.Schedule.StatusDescription = "Embarque fechado";

                        tran.Commit();
                        return;
                    }
                    catch (Exception ex)
                    {
                        base.SendToInsights(ex);
                        tran.Rollback();
                    }
                }
            }

            using (var tran = realm.BeginWrite())
            {
                try
                {
                    item.Schedule.StatusDescription = "Embarque encerrado";

                    tran.Commit();
                    return;
                }
                catch (Exception ex)
                {
                    base.SendToInsights(ex);
                    tran.Rollback();
                }
            }
            */
		}

		public Weather GetWeather()
		{
			return _weatherService.Get();
		}

		string SetFontAwesomeForTemp(string ico)
		{
			if (ico.Contains("rain") || ico.Contains("thunder"))
				return $"\uf0e9";

			if (ico.Contains("cloudy"))
				return $"\uf073";

			if (ico.Contains("sunny"))
				return $"\uf185";

			return $"\uf186";
		}
	}
}