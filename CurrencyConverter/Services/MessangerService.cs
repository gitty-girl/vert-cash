using System;
using System.Threading.Tasks;
using CurrencyConverter.ExecutionControl;
using CurrencyConverter.Models;
using CurrencyConverter.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Services
{
    public class MessangerService
    {
        public async Task<ExecutionResult<Message>> GetMessageAsync(int id)
        {
            //get message from database
            var message = new Message();

            if (message == null)
                throw new Exception("Message not found");

            return ExecutionResult<Message>.Success(message);
        }

        public async Task<ExecutionResult> PostMessageAsync(Message message)
        {
            //post message to database

            return ExecutionResult.Success();
        }

        public async Task<ExecutionResult> EditMessageAsync(int id, Message message)
        {
            //get
            var oldMessage = new Message();

            //edit
            oldMessage = message;

            //save 

            return ExecutionResult.Success();
        }

        public async Task<ExecutionResult> DeleteMessageAsync(int id)
        {
            //get

            //remove

            return ExecutionResult.Success();
        }
    }
}