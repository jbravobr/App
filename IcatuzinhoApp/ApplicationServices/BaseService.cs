using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using Acr.Settings;
using Newtonsoft.Json;

namespace IcatuzinhoApp
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        readonly IBaseRepository<T> _repo;

        public BaseService(IBaseRepository<T> repo)
        {
            _repo = repo;

            if (App.httpClient == null)
                App.httpClient = HttpAccessInstance.GetClient;
        }

        string GetRightServicePath
        {
            get
            {
                var path = string.Empty;

                if (typeof(T) == typeof(User))
                    path = Constants.UserServiceAddress;

                if (typeof(T) == typeof(Schedule))
                    path = Constants.ScheduleServiceAddress;

                if (typeof(T) == typeof(Station))
                    path = Constants.StationServiceAddress;

                if (typeof(T) == typeof(Itinerary))
                    path = Constants.ItineraryServiceAddress;

                if (typeof(T) == typeof(Travel))
                    path = Constants.TravelServiceAddress;
				if (typeof(T) == typeof(Weather))
					path = Constants.WeatherServiceAddress;


                return path;
            }
        }

        /// <summary>
        /// Insere os novos registros quando estes são lista.
        /// </summary>
        /// <returns>The or replace all with children .</returns>
        /// <param name="list">List.</param>
        public async Task InsertOrReplaceAllWithChildren(List<T> list)
        {
            await Task.Run(() => _repo.InsertOrReplaceAllWithChildren(list));
        }

        /// <summary>
        /// Deleta o registro informado baseado na entidade.
        /// </summary>
        /// <returns>The .</returns>
        /// <param name="entidade">Entidade.</param>
        public async Task Delete(T entidade)
        {
            await Task.Run(() => _repo.Delete(entidade));
        }

        /// <summary>
        /// Retorna os dados da entidade e todos os relacionados base numa expressão filtro.
        /// </summary>
        /// <returns>The all with children .</returns>
        /// <param name="predicate">Predicate.</param>
        public async Task<List<T>> GetAllWithChildren(Expression<Func<T, bool>> predicate, string optionalRoute = "")
        {
            if (await Connectivity.IsNetworkingOK())
            {
                try
                {
                    var data = await App.httpClient.GetAsync(string.IsNullOrEmpty(optionalRoute) ? GetRightServicePath : optionalRoute);

                    if (data != null && data.IsSuccessStatusCode)
                    {
                        var dto = new DTO<T>();
                        var entities = await dto.ConvertCollectionObjectFromJson(data.Content);

                        if (entities != null && entities.Any())
                            await InsertOrReplaceAllWithChildren(entities);
                    }

                    if (data != null && data.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        UIFunctions.ShowErrorMessageToUI(Constants.MessageErroAuthentication);
                        return null;
                    }

                    if (data == null)
                    {
                        UIFunctions.ShowErrorMessageToUI();
                        return null;
                    }

                }
                catch (Exception ex)
                {
                    LogExceptionHelper.SubmitToInsights(ex);
                    UIFunctions.ShowErrorMessageToUI();
                    return null;
                }
            }

            return _repo.GetAllWithChildren(predicate);
        }

        /// <summary>
        /// Retorna todos os registros em lista do predicado informado.
        /// </summary>
        /// <returns>The .</returns>
        /// <param name="predicate">Predicate.</param>
        public async Task<T> Get(Expression<Func<T, bool>> predicate, string optionalRoute = "")
        {
            if (await Connectivity.IsNetworkingOK())
            {
                try
                {
                    var data = await App.httpClient.GetAsync(string.IsNullOrEmpty(optionalRoute) ? GetRightServicePath : optionalRoute);

                    if (data != null && data.IsSuccessStatusCode)
                    {
                        var dto = new DTO<T>();
                        var entity = await dto.ConvertSingleObjectFromJson(data.Content);

                        if (entity != null)
                            await InsertOrReplaceWithChildren(entity);
                    }

                    if (data != null && data.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        UIFunctions.ShowErrorMessageToUI(Constants.MessageErroAuthentication);
                        return null;
                    }

                    if (data == null)
                    {
                        UIFunctions.ShowErrorMessageToUI();
                        return null;
                    }

                }
                catch (Exception ex)
                {
                    LogExceptionHelper.SubmitToInsights(ex);
                    UIFunctions.ShowErrorMessageToUI();
                    return null;
                }
            }
            return _repo.GetWithChildren(predicate);
        }

        /// <summary>
        /// Retorna todos os registros e seus filhos, sem filtro.
        /// </summary>
        /// <returns>The all with children .</returns>
        public async Task<List<T>> GetAllWithChildren(string optionalRoute = "")
        {
            if (await Connectivity.IsNetworkingOK())
            {
                try
                {
                    var data = await App.httpClient.GetAsync(string.IsNullOrEmpty(optionalRoute) ? GetRightServicePath : optionalRoute);

                    if (data != null && data.IsSuccessStatusCode)
                    {
                        var dto = new DTO<T>();
                        var entities = await dto.ConvertCollectionObjectFromJson(data.Content);

                        if (entities != null && entities.Any())
                            await InsertOrReplaceAllWithChildren(entities);
                    }

                    if (data != null && data.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        UIFunctions.ShowErrorMessageToUI(Constants.MessageErroAuthentication);
                        return null;
                    }

                    if (data == null)
                    {
                        UIFunctions.ShowErrorMessageToUI();
                        return null;
                    }

                }
                catch (Exception ex)
                {
                    LogExceptionHelper.SubmitToInsights(ex);
                    UIFunctions.ShowErrorMessageToUI();
                    return null;
                }
            }

            return _repo.GetAllWithChildren();
        }

        /// <summary>
        /// Retornar uma  entidade com filtro em seu ID.
        /// </summary>
        /// <returns>The with children by identifier .</returns>
        /// <param name="pkId">Pk identifier.</param>
        public async Task<T> GetWithChildrenById(int pkId, string optionalRoute = "")
        {
            if (await Connectivity.IsNetworkingOK())
            {
                try
                {
					var url = string.IsNullOrEmpty(optionalRoute) ? GetRightServicePath : optionalRoute;
					var data = await App.httpClient.GetAsync($"{(url)}{pkId}");

                    if (data != null && data.IsSuccessStatusCode)
                    {
                        var dto = new DTO<T>();
                        var entity = await dto.ConvertSingleObjectFromJson(data.Content);

                        if (entity != null)
                            await InsertOrReplaceWithChildren(entity);
                    }

                    if (data != null && data.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        UIFunctions.ShowErrorMessageToUI(Constants.MessageErroAuthentication);
                        return null;
                    }

                    if (data == null)
                    {
                        UIFunctions.ShowErrorMessageToUI();
                        return null;
                    }

                }
                catch (Exception ex)
                {
                    LogExceptionHelper.SubmitToInsights(ex);
                    UIFunctions.ShowErrorMessageToUI();
                    return null;
                }
            }
            return _repo.GetWithChildrenById(pkId);
        }

        /// <summary>
        /// Efetua a atualiação de uma entidade.
        /// </summary>
        /// <returns>The with children .</returns>
        /// <param name="entity">Entity.</param>
        public async Task UpdateWithChildren(T entity)
        {
            await Task.Run(() => _repo.UpdateWithChildren(entity));
        }

        /// <summary>
        /// Efetua a inserção de um registro e dos seus filhos.
        /// </summary>
        /// <returns>The or replace with children .</returns>
        /// <param name="entity">Entity.</param>
        public async Task InsertOrReplaceWithChildren(T entity)
        {
			try
			{
				await Task.Run(() => _repo.InsertOrReplaceWithChildren(entity));
			}
			catch (Exception ex)
			{
				throw ex;
			}

        }

        /// <summary>
        /// Retorna uma entidade e seus filhos.
        /// </summary>
        public async Task<T> GetWithChildren(Expression<Func<T, bool>> predicate, string optionalRoute = "")
        {
            if (await Connectivity.IsNetworkingOK())
            {
                try
                {
                    var data = await App.httpClient.GetAsync(string.IsNullOrEmpty(optionalRoute) ? GetRightServicePath : optionalRoute);

                    if (data != null && data.IsSuccessStatusCode)
                    {
                        var dto = new DTO<T>();
                        var entity = await dto.ConvertSingleObjectFromJson(data.Content);

                        if (entity != null)
                            await InsertOrReplaceWithChildren(entity);
                    }

                    if (data != null && data.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        UIFunctions.ShowErrorMessageToUI(Constants.MessageErroAuthentication);
                        return null;
                    }

                    if (data == null)
                    {
                        UIFunctions.ShowErrorMessageToUI();
                        return null;
                    }

                }
                catch (Exception ex)
                {
                    LogExceptionHelper.SubmitToInsights(ex);
                    UIFunctions.ShowErrorMessageToUI();
                    return null;
                }
            }

            return _repo.GetWithChildren(predicate);
        }

        /// <summary>
        /// Retorna uma lista de uma entidade.
        /// </summary>
        public async Task<List<T>> GetAll(string optionalRoute = "")
        {
            if (await Connectivity.IsNetworkingOK())
            {
                try
                {
                    var data = await App.httpClient.GetAsync(GetRightServicePath);

                    if (data != null && data.IsSuccessStatusCode)
                    {
                        var dto = new DTO<T>();
                        var entities = await dto.ConvertCollectionObjectFromJson(data.Content);

                        if (entities != null && entities.Any())
                            await InsertOrReplaceAllWithChildren(entities);
                    }

                    if (data != null && data.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        UIFunctions.ShowErrorMessageToUI(Constants.MessageErroAuthentication);
                        return null;
                    }

                    if (data == null)
                    {
                        UIFunctions.ShowErrorMessageToUI();
                        return null;
                    }

                }
                catch (Exception ex)
                {
                    LogExceptionHelper.SubmitToInsights(ex);
                    UIFunctions.ShowErrorMessageToUI();
                    return null;
                }
            }

            return _repo.GetAll();
        }

        /// <summary>
        /// Retorna T.
        /// </summary>
        public async Task<T> Get(string optionalRoute = "")
        {
            if (await Connectivity.IsNetworkingOK())
            {
                try
                {
                    var data = await App.httpClient.GetAsync(GetRightServicePath);

                    if (data != null && data.IsSuccessStatusCode)
                    {
                        var dto = new DTO<T>();
                        var entity = await dto.ConvertSingleObjectFromJson(data.Content);

                        if (entity != null)
                            await InsertOrReplaceWithChildren(entity);
                    }

                    if (data != null && data.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        UIFunctions.ShowErrorMessageToUI(Constants.MessageErroAuthentication);
                        return null;
                    }

                    if (data == null)
                    {
                        UIFunctions.ShowErrorMessageToUI();
                        return null;
                    }

                }
                catch (Exception ex)
                {
                    LogExceptionHelper.SubmitToInsights(ex);
                    UIFunctions.ShowErrorMessageToUI();
                    return null;
                }
            }
            return _repo.Get();
        }

        /// <summary>
        /// Verifica se existe a entidade informada
        /// </summary>
        public async Task<bool> Any()
        {
            return await Task.Run(() => _repo.Any());
        }

        /// <summary>
        /// Faz Checkin para a viagem
        /// </summary>
        /// <returns>The checkin.</returns>
        /// <param name="scheduleId">Schedule identifier.</param>
        /// <param name="userId">User identifier.</param>
        public async Task<bool> DoCheckin(int scheduleId, int userId)
        {
            try
            {
                var data = await App.httpClient.GetAsync($"{Constants.TravelServiceCheckInAddress}{scheduleId}/{userId}");

                if (data != null && data.IsSuccessStatusCode)
                {
                    var dto = new DTO<Travel>();
                    var entity = await dto.ConvertSingleObjectFromJsonToBolean(data.Content);

                    return await Task.FromResult(entity);
                }

                if (data != null && data.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    UIFunctions.ShowErrorMessageToUI(Constants.MessageErroAuthentication);

                if (data == null)
                    UIFunctions.ShowErrorMessageToUI();

                return await Task.FromResult(false);
            }
            catch (Exception ex)
            {
                LogExceptionHelper.SubmitToInsights(ex);
                UIFunctions.ShowErrorMessageToUI();
                return await Task.FromResult(false);
            }
        }

        /// <summary>
        /// Faz checkout da viagem
        /// </summary>
        /// <returns>The checkout.</returns>
        /// <param name="scheduleId">Schedule identifier.</param>
        /// <param name="userId">User identifier.</param>
        public async Task<bool> DoCheckout(int scheduleId, int userId)
        {
            try
            {
                var data = await App.httpClient.GetAsync($"{Constants.TravelServiceCheckOutAddress}{scheduleId}/{userId}");

                if (data != null && data.IsSuccessStatusCode)
                {
                    var dto = new DTO<Travel>();
                    var entity = await dto.ConvertSingleObjectFromJsonToBolean(data.Content);

                    return await Task.FromResult(entity);
                }

                if (data != null && data.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    UIFunctions.ShowErrorMessageToUI(Constants.MessageErroAuthentication);

                if (data == null)
                    UIFunctions.ShowErrorMessageToUI();

                return await Task.FromResult(false);
            }
            catch (Exception ex)
            {
                LogExceptionHelper.SubmitToInsights(ex);
                UIFunctions.ShowErrorMessageToUI();
                return await Task.FromResult(false);
            }
        }

        /// <summary>
        /// Faz o login
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password.</param>
        public async Task<bool> Login(string email, string password)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"{Constants.FormsAuthentication}");
                request.Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username",email),
                    new KeyValuePair<string, string>("password",password)
                });

                var response = await App.httpClient.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var authenticationToken = JsonConvert.DeserializeObject<AuthenticationToken>(jsonString);
                    Settings.Local.Set<string>("AccessToken", authenticationToken.AccessToken);

					App.httpClient = HttpAccessInstance.GetClientWithNewToken;

                    return true;
                }

                if (response != null && (response.StatusCode == System.Net.HttpStatusCode.Forbidden ||
                                         response.StatusCode == System.Net.HttpStatusCode.BadRequest))
                {
                    UIFunctions.ShowErrorMessageToUI(Constants.MessageErroAuthentication);
                    LogExceptionHelper.SubmitToInsights(new ArgumentException($"Erro na autorização para o email: {Settings.Local.Get<string>("UsernName")}"));
                }

                if (response == null)
                    throw new ArgumentNullException(nameof(response), "Response vazio");

                return false;
            }
            catch (Exception ex)
            {
                LogExceptionHelper.SubmitToInsights(ex);
                return false;
            }
        }

		public async Task<User> GetUserByLogin(string login, string password)
		{
			if (await Connectivity.IsNetworkingOK())
			{
				try
				{
					var data = await App.httpClient.GetAsync($"{GetRightServicePath}{login}");

					if (data != null && data.IsSuccessStatusCode)
					{
						var dto = new DTO<T>();
						var entity = await dto.ConvertSingleObjectFromJson(data.Content);

						if (entity != null)
						{
							(entity as User).Password = password;
							await InsertOrReplaceWithChildren(entity);
						}
					}

					if (data != null && data.StatusCode == System.Net.HttpStatusCode.Forbidden)
					{
						UIFunctions.ShowErrorMessageToUI(Constants.MessageErroAuthentication);
						return null;
					}

					if (data == null)
					{
						UIFunctions.ShowErrorMessageToUI();
						return null;
					}

				}
				catch (Exception ex)
				{
					LogExceptionHelper.SubmitToInsights(ex);
					UIFunctions.ShowErrorMessageToUI();
					return null;
				}
			}

			return null;
		}

		public async Task<T> GetNextSchedule()
		{
			try
			{
				var schedules = _repo.GetAll();
				if (schedules == null || !schedules.Any())
				{
					schedules = await GetAll();
				}
				return schedules.FirstOrDefault(x => (x as Schedule).StartSchedule.TimeOfDay >= DateTime.Now.TimeOfDay);
			}
			catch (Exception ex)
			{
				LogExceptionHelper.SubmitToInsights(ex);
				UIFunctions.ShowErrorMessageToUI();
				return null;
			}
		}

		public async Task<Travel> GetTravelByScheduleId(int scheduleId)
		{
			
				try
				{
					
				var data = await App.httpClient.GetAsync($"{GetRightServicePath}{scheduleId}");

					if (data != null && data.IsSuccessStatusCode)
					{
						var dto = new DTO<T>();
						var entity =  await dto.ConvertSingleObjectFromJson(data.Content);

						(entity as Travel).ScheduleId = scheduleId;

						if (entity != null)
							await InsertOrReplaceWithChildren(entity);
					}

					if (data != null && data.StatusCode == System.Net.HttpStatusCode.Forbidden)
					{
						UIFunctions.ShowErrorMessageToUI(Constants.MessageErroAuthentication);
						return null;
					}

					if (data == null)
					{
						UIFunctions.ShowErrorMessageToUI();
						return null;
					}

					var all = _repo.GetAllWithChildren();

				var trav =(all as List<Travel>).FirstOrDefault(x => x.ScheduleId == scheduleId);
				return trav;

				}
				catch (Exception ex)
				{
					LogExceptionHelper.SubmitToInsights(ex);
					UIFunctions.ShowErrorMessageToUI();
					return null;
				}

		}
	}
}