using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using WebLog.API.ViewModels;
using WebLog.Core;
using WebLog.Core.Common;
using WebLog.Core.Models;

namespace WebLog.API.Controllers
{
    public class MessageController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        [Authorize]
        public IHttpActionResult GetMessages()
        {
            var messages = _unitOfWork.Messages.GetMessages(User.Identity.GetUserId()).ToList();
            var convMessages = ConvertHelper.GetConvertedMessages(messages, User.Identity.GetUserId());
            return Ok(convMessages);
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult GetEmail()
        {
            return Ok(User.Identity.GetUserId());
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult SendMessage([FromBody] NewMessage message)
        {
            var userFrom = _unitOfWork.Users.GetUser(User.Identity.GetUserId());
            var userTo = _unitOfWork.Users.GetUser(message.Email);
            _unitOfWork.Messages.Add(new Message(userFrom, userTo, message.Text));
            _unitOfWork.Complete();
            return Ok();
        }
    }
}
