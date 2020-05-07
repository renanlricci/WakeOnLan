﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using WakeOnLan.Api.Models;
using WakeOnLan.CrossCutting.Exceptions;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WakeOnLan.Api.Controllers
{
    public abstract class BaseController<T> : Controller
    {
        protected readonly IMediator _mediator;

        protected BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<IActionResult> CreateResponse<T>(Func<Task<T>> function)
        {
            try
            {
                Log.Information("Request Func({0}) from:{1}", function?.Method?.Name, HttpContext.Request.Headers["X-Forwarded-For"]);
                var data = await function();
                return Response(data);
            }
            catch (CustomException ex)
            {
                Log.Warning("CUSTOMEXCEPTION StatusCode:{0} | Message:{1}", ex.StatusCode, ex.Message);
                return StatusCode(
                    (int)ex.StatusCode,
                    new ApiFailResponse(ex));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception");
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ApiFailResponse(ex));
            }
        }

        protected async Task<IActionResult> CreateResponse(Func<Task> action)
        {
            try
            {
                Log.Information("Request Func({0}) from:{1}", action?.Method?.Name, HttpContext.Request.Headers["X-Forwarded-For"]);
                await action();
                return Response();
            }
            catch (CustomException ex)
            {
                Log.Warning("CUSTOMEXCEPTION StatusCode:{0} | Message:{1}", ex.StatusCode, ex.Message);
                return StatusCode(
                    (int)ex.StatusCode,
                    new ApiFailResponse(ex));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception");
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ApiFailResponse(ex));
            }
        }

        private new IActionResult Response(object data = null) => Ok(new ApiResponse(data));
    }
}
