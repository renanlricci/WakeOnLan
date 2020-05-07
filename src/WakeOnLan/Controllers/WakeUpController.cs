using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Raspberry.Configuration;
using Serilog;
using System;
using System.Net;
using System.Net.NetworkInformation;

namespace Raspberry.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class WakeUpController : Controller
    {
        private readonly IOptions<Configs> _config;

        public WakeUpController(IOptions<Configs> config)
        {
            _config = config;
        }

        [HttpGet]
        public string Get()
        {
            try
            {
                Log.Information($"GET from IP: {Request.HttpContext.Connection.RemoteIpAddress}");
                return $"hey! {Request.HttpContext.Connection.RemoteIpAddress}, send a Post!";
            }
            catch (Exception ex)
            {
                Log.Error(JsonConvert.SerializeObject(ex));
                return $"Exception:{JsonConvert.SerializeObject(ex)}";
            }
        }

        [HttpGet("{ipAddress}")]
        public string GetByIpAddress(string ipAddress)
        {
            try
            {
                Log.Information($"GET from IP: {Request.HttpContext.Connection.RemoteIpAddress} testing Ip Address: {ipAddress}");
                Ping pingClient = new Ping();
                var ping = pingClient.Send(ipAddress);
                return $"ping to {ipAddress} status: {ping.Status.ToString()}";
            }
            catch (Exception ex)
            {
                Log.Error(JsonConvert.SerializeObject(ex));
                return $"Exception:{JsonConvert.SerializeObject(ex)}";
            }
        }

        [HttpPost]
        public object Post(string user, string pass)
        {
            try
            {
                if ("renanlricci" != user || "cagado9292" != pass)
                    return new { success = false, error = HttpStatusCode.Unauthorized };
                Log.Information($"WakeUpOnLan POST from IP: {Request.HttpContext.Connection.RemoteIpAddress}");
                //WakeOnLan.WakeUp("44-8A-5B-9D-A6-E1", "192.168.31.100", "255.255.255.0");
                WakeOnLan.WakeUp("44-8A-5B-9D-A6-E1", "192.168.31.255", "255.255.255.0");
                return new { success = true };
            }
            catch (Exception ex)
            {
                Log.Error(JsonConvert.SerializeObject(ex));
                return new
                {
                    HttpStatusCode.InternalServerError,
                    error = $"Exception:{JsonConvert.SerializeObject(ex)}"
                };
            }
        }


    }
}