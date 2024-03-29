﻿using Labs01.MediatR.Commons.Configurations.NotificationContext;
using Labs01.MediatR.WebApp.MediatrConfigurations.Pipelines;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Labs01.MediatR.Commons.Configuration.MediatrConfigurations.Pipelines
{
    public class NotificationPipeline : IAsyncResultFilter
	{
		private readonly NotificationContext _notificationContext;

		public NotificationPipeline(NotificationContext notificationContext)
		{
			_notificationContext = notificationContext;
		}

		public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			if (_notificationContext.HasNotifications)
			{
				context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
				context.HttpContext.Response.ContentType = "application/json";

				var notifications = JsonConvert.SerializeObject(_notificationContext.Notifications);
				await context.HttpContext.Response.WriteAsync(notifications);

				return;
			}

			await next();
		}
	}
}
